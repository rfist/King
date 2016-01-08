using UnityEngine;
using System.Collections;

public class CardMediator : MonoBehaviour {

    public CardVO card;
    bool Disabled = false;
    void OnMouseDown()
    {
        if (Disabled)
        {
            return;
        }

        if (GameController.inst.checkIfTurnValid(card))
        {
            AnimationHelper.makeTurn(card);
        }
        else
        {
            Debug.LogError("You can't make turn with this card!");
        }   
    }

    // http://answers.unity3d.com/questions/45782/itween-oncomplete-not-firing.html
    // Must be in object, which animated(((
    void endTurn()
    {
        Debug.Log("endTurn");
        Disabled = true;
        GameModel.inst.cardsOnDeck.Add(card);
        EventManager.TriggerEvent(Config.ON_TURN_ANIMATION_FINISHED);
    }

    void endGrab()
    {
        Debug.Log("endGrab");
        Destroy(card.gameObject);
        card.Owner.Deck.Remove(card);
        GameModel.inst.cardsOnDeck.Remove(card);     
        EventManager.TriggerEvent(Config.ON_GRAB_ANIMATION_FINISHED);
    }
}

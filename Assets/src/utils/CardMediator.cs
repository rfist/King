using UnityEngine;
using System.Collections;

public class CardMediator : MonoBehaviour {

    public CardVO card;
    public bool IsLast = false;
    bool Disabled = false;

    void OnMouseDown()
    {
        for (int i = 0; i < GameModel.inst.cardsOnDeck.Count; i++)
        {
            CardVO cardInDeck = GameModel.inst.cardsOnDeck[i] as CardVO;
            if (cardInDeck.Owner.Id == Config.PLAYER_ME)
            {
                return; // мы уже ходили в этом ходу
            }
        }

        if (Disabled || 
            GameModel.inst.currentPlayer.Id != Config.PLAYER_ME || 
            AnimationHelper.isAnimated)
        {
            return;
        }

        if (GameController.inst.checkIfTurnValid(card))
        {
            AnimationHelper.makeTurn(card);
        }
        else
        {
            AudioManager.inst.playError();
            Debug.LogError("You can't make turn with this card!");
        }   
    }

    // http://answers.unity3d.com/questions/45782/itween-oncomplete-not-firing.html
    // Must be in object, which animated(((
    void endTurn()
    {   
        Debug.Log("endTurn by player " + card.Owner.Id);
        GameModel.inst.cardsOnDeck.Add(card);
        GameModel.inst.level.History.Add(card);
        Disabled = true;
        EventManager.TriggerEvent(Config.ON_TURN_ANIMATION_FINISHED);
        AnimationHelper.isAnimated = false;
    }

    void endGrab()
    {
        Debug.Log("endGrab");
        Destroy(card.gameObject);
        card.Owner.Deck.Remove(card);
        GameModel.inst.cardsOnDeck.Remove(card);     
        EventManager.TriggerEvent(Config.ON_GRAB_ANIMATION_FINISHED);
    }

    void endDistribution()
    {
        AudioManager.inst.playEndTurn();
        if (card.Owner.Id == Config.PLAYER_ME)
        {
            SpriteRenderer renderer = card.gameObject.GetComponent<SpriteRenderer>();
            renderer.sprite = Resources.Load<Sprite>("Cards/" + card.ImageName);
        }

        if (IsLast)
        {
            IsLast = false;
            EventManager.TriggerEvent(Config.ON_START_ANIMATION_FINISHED);
        }
    }
}

using UnityEngine;
using System.Collections;

public class AnimationHelper
{
    // http://habrahabr.ru/post/220837/ using iTween

    public static void makeTurn(CardVO card)
    {
        string namePositionPoint = Config.NameOfPlayersPointsOnTable[card.Owner.Id];
        Vector3 position = GameObject.Find(namePositionPoint).transform.position;
        iTween.MoveTo(card.gameObject, iTween.Hash("x", position.x, "y", position.y, "time", 0.2, "oncomplete", "endTurn")); // callback is in CardMediator
        Debug.Log("select card # " + card.Id);
    }

    public static void grabCards(PlayerVO grabber)
    {
        string namePositionPoint = grabber.Container;
        Vector3 position = GameObject.Find(namePositionPoint).transform.position;

        for (int i = 0; i < GameModel.inst.cardsOnDeck.Count; i++)
        {
            CardVO card = GameModel.inst.cardsOnDeck[i] as CardVO;
            iTween.MoveTo(card.gameObject, iTween.Hash("x", position.x, "y", position.y, "time", 0.7, "oncomplete", "endGrab")); // callback is in CardMediator
            Debug.Log("grab card # " + card.Id);
        }      
    }
}

using UnityEngine;
using System.Collections;

public class AnimationHelper
{
    // http://habrahabr.ru/post/220837/ using iTween

    public static bool isAnimated = false;
    public static void makeTurn(CardVO card)
    {
        SpriteRenderer renderer = card.gameObject.GetComponent<SpriteRenderer>(); 
        renderer.sprite = Resources.Load<Sprite>("Cards/" + card.ImageName);
        isAnimated = true;
        string namePositionPoint = Config.NameOfPlayersPointsOnTable[card.Owner.Id];
        Vector3 position = GameObject.Find(namePositionPoint).transform.position;
        AudioManager.inst.playEndTurn();
        iTween.MoveTo(card.gameObject, iTween.Hash("x", position.x, "y", position.y, "time", 0.5, "oncomplete", "endTurn")); // callback is in CardMediator
  //      Debug.Log("select card # " + card.Id);
    }

    public static void grabCards(PlayerVO grabber)
    {
        string namePositionPoint = grabber.Container;
        Vector3 position = GameObject.Find(namePositionPoint).transform.position;
        AudioManager.inst.playGrab();
        for (int i = 0; i < GameModel.inst.cardsOnDeck.Count; i++)
        {
            CardVO card = GameModel.inst.cardsOnDeck[i] as CardVO;
            iTween.MoveTo(card.gameObject, iTween.Hash("x", position.x, "y", position.y, "time", 0.7, "oncomplete", "endGrab")); // callback is in CardMediator
            Debug.Log("grab card # " + card.Id);
        }      
    }

    public static void makeDistribution(CardVO card, float delay)
    {
        Vector3 startPosotion = GameObject.Find(Config.CREATE_CARD_POSITION).transform.position;
        //SpriteRenderer renderer = card.gameObject.GetComponent<SpriteRenderer>();
        //renderer.sprite = Resources.Load<Sprite>("Cards/" + card.ImageName);
        //isAnimated = true;
        Vector3 position = card.gameObject.transform.position;
        card.gameObject.transform.position = new Vector3(startPosotion.x, startPosotion.y, 1);
        iTween.MoveTo(card.gameObject, iTween.Hash("x", position.x, "y", position.y, "delay", delay, "time", Config.SPEED_DISTRIBUTION, "oncomplete", "endDistribution")); // callback is in CardMediator
//        Debug.Log("select card # " + card.Id);
    }
}

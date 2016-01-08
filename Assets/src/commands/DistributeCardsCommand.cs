using UnityEngine;
using System.Collections;

public class DistributeCardsCommand : ICommand
{

    public DistributeCardsCommand() { }


    public void execute()
    {
        for (int i = 0; i < GameModel.inst.Players.Length; i++)
        {
            for (int number = 0; number < GameModel.inst.Players[i].Deck.Count; number++)
            {
                createCard(number, GameModel.inst.Players[i].Deck[number] as CardVO);
            }
        }
    }

    void createCard(int number, CardVO card)
    {
        GameObject go = new GameObject("card");
        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();

        GameObject container = GameObject.Find(card.Owner.Container);
        Vector3 position = container.transform.Find("CardPlace").gameObject.transform.position;
        if (card.Owner.Id == Config.PLAYER_ME)
        {
            go.transform.position = new Vector3(position.x + number * Config.DISTANCE_BETWEEN_CARDS_FOR_PLAYER, position.y, 1);
            renderer.sprite = Resources.Load<Sprite>("Cards/" + card.ImageName);
            go.AddComponent<BoxCollider>();
            CardMediator cardMediator = go.AddComponent<CardMediator>();
            cardMediator.card = card;
        }
        else {
            //renderer.sprite = Resources.Load<Sprite>("Cards/CardBack");
            renderer.sprite = Resources.Load<Sprite>("Cards/" + card.ImageName);
            go.transform.position = new Vector3(position.x + number * Config.DISTANCE_BETWEEN_CARDS, position.y, 1);
            CardMediator cardMediator = go.AddComponent<CardMediator>();
            cardMediator.card = card;
        }
        card.gameObject = go;
    }
}

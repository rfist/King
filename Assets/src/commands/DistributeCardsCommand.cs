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
        CardMediator cardMediator;
        if (card.Owner.Id == Config.PLAYER_ME)
        {
            go.transform.position = new Vector3(position.x + number * Config.DISTANCE_BETWEEN_CARDS_FOR_PLAYER, position.y, 1);
            //renderer.sprite = Resources.Load<Sprite>("Cards/" + card.ImageName);
            renderer.sprite = Resources.Load<Sprite>("Cards/CardBack");
            go.AddComponent<BoxCollider>();
            cardMediator = go.AddComponent<CardMediator>();
            cardMediator.card = card;
        }
        else {
            if (Config.IS_AI_CARDS_OPEN)
            {
                renderer.sprite = Resources.Load<Sprite>("Cards/" + card.ImageName);
            }
            else
            {
                renderer.sprite = Resources.Load<Sprite>("Cards/CardBack");
            }
            go.transform.position = new Vector3(position.x + number * Config.DISTANCE_BETWEEN_CARDS, position.y, 1);
            cardMediator = go.AddComponent<CardMediator>();
            cardMediator.card = card;
        }
        card.gameObject = go;
        float delay = number * 4 * Config.SPEED_DISTRIBUTION;
        int playerNumber = card.Owner.Id;
        if (playerNumber == Config.PLAYER_C && number == 7) // last card
        {
            cardMediator.IsLast = true;
        }

        AnimationHelper.makeDistribution(card, (Config.SPEED_DISTRIBUTION * playerNumber + delay) / 2);
    }
}

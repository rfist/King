using UnityEngine;
using System.Collections;

public class EndRoundCommand : MonoBehaviour, ICommand
{

    public EndRoundCommand() { }


    public void execute()
    {

        Debug.Log("EndRoundCommand");

        // remove all cards
        for (int i = 0; i < GameModel.inst.Players.Length; i++)
        {
            int len = GameModel.inst.Players[i].Deck.Count;

            for (int number = len - 1; number >= 0; number--)
            {
                removeCard(GameModel.inst.Players[i].Deck[number] as CardVO);
            }
        }

        // count score, remove goals
        for (int j = 0; j < GameModel.inst.Players.Length; j++)
        {
            GameModel.inst.Players[j].Goals = new ArrayList();
        }

        CommandManager.inst.addCommand(typeof(ShowScoreCommand));

    }

    void removeCard(CardVO card)
    {
        if (card.gameObject)
        {
            Destroy(card.gameObject);
        }
        card.Owner.Deck.Remove(card);
        GameModel.inst.cardsOnDeck.Remove(card);
    }
}

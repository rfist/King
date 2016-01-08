using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class ShuffleDeckCommand : ICommand
{
    public ShuffleDeckCommand() {}


    public void execute () {
        int[] emptySlots = new int[GameModel.inst.Deck.Length];

        for (int i = 0; i < GameModel.inst.Deck.Length; i++)
        {
           emptySlots[i] = i;
        }

        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < emptySlots.Length; t++)
        {
            int tmp = emptySlots[t];
            int r = Random.Range(t, emptySlots.Length);
            emptySlots[t] = emptySlots[r];
            emptySlots[r] = tmp;
        }

        CardVO card;
        for (int i = 0; i < emptySlots.Length; i++)
        {
            card = GameModel.inst.Deck[emptySlots[i]];
            if (i < 8)
            {
                card.Owner = GameModel.inst.Players[Config.PLAYER_A];
                GameModel.inst.Players[Config.PLAYER_A].Deck.Add(card);
            }
            if (i >= 8 && i < 16)
            {
                card.Owner = GameModel.inst.Players[Config.PLAYER_B];
                GameModel.inst.Players[Config.PLAYER_B].Deck.Add(card);
            }
            if (i >= 16 && i < 24)
            {
                card.Owner = GameModel.inst.Players[Config.PLAYER_C];
                GameModel.inst.Players[Config.PLAYER_C].Deck.Add(card);
            }
            if (i >= 24)
            {
                card.Owner = GameModel.inst.Players[Config.PLAYER_ME];
                GameModel.inst.Players[Config.PLAYER_ME].Deck.Add(card);
            }
        }

        // sort player cards
        CardVO[] array = GameModel.inst.Players[Config.PLAYER_ME].Deck.ToArray(typeof(CardVO)) as CardVO[];
        array = array.OrderBy(o => o.Suit).ThenBy(o => o.Rank).ToArray();
        GameModel.inst.Players[Config.PLAYER_ME].Deck = new ArrayList();
        GameModel.inst.Players[Config.PLAYER_ME].Deck.AddRange(array);

        Debug.Log("Shuffle Complete");
        EventManager.TriggerEvent(Config.ON_LEVEL_CHANGED);
    }
}

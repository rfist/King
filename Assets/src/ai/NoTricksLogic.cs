using UnityEngine;
using System.Collections;

public class NoTricksLogic
{
    public NoTricksLogic() { }

    public static CardVO getCardForPlay(ArrayList deck)
    {
        CardVO playingCard = null;
        if (GameModel.inst.cardsOnDeck.Count == 0)
        {
            for (int k = 0; k < deck.Count; k++)
            {
                CardVO card = deck[k] as CardVO;
                if (playingCard == null)
                {
                    playingCard = card;
                }
                else
                {
                    if (card.Rank < playingCard.Rank) // отдаем самую меншьую карту
                    {
                        playingCard = card;
                    }
                }

            }
        }
        else
        {
            CardVO maxCard = GameModel.inst.cardsOnDeck[0] as CardVO;

            for (int m = 0; m < GameModel.inst.cardsOnDeck.Count; m++)  // находим самую сильную играющую карту
            {
                CardVO card = GameModel.inst.cardsOnDeck[m] as CardVO;
                if (card.Suit == maxCard.Suit && card.Rank > maxCard.Rank)
                {
                    maxCard = card;
                }
            }

            for (int i = 0; i < deck.Count; i++)
            {
                CardVO card = deck[i] as CardVO;
                if (card.Suit == maxCard.Suit)
                {
                    if (playingCard == null)
                    {
                        playingCard = card;
                    }
                    else
                    {
                        if (card.Rank < maxCard.Rank) 
                        {
                            if (playingCard.Rank > maxCard.Rank && card.Rank > playingCard.Rank) // если меньших нет, то отдаем самую крупную карту
                            {
                                playingCard = card;
                            }

                            if (card.Rank > playingCard.Rank) // если больше текущей, то отдаем ее
                            {
                                playingCard = card;
                            }

                            if (playingCard.Rank > maxCard.Rank) // если текущая карта той же масти, но больше максимальной, то берем ту что помешье
                            {
                                playingCard = card;
                            }                         
                        }
                        else
                        {
                            if (card.Rank < playingCard.Rank) // если больше максимальной, но меньше текущей
                            {
                                playingCard = card;
                            }
                        }
                    }
                }
            }

            if (playingCard == null) // карт той же масти не нашлось
            {
                for (int j = 0; j < deck.Count; j++)
                {
                    CardVO card = deck[j] as CardVO;
                    if (playingCard == null)
                    {
                        playingCard = card;
                    }
                    else
                    {
                        if (card.Rank > playingCard.Rank) // отдаем самую крупную карту
                        {
                            playingCard = card;
                        }
                    }
                }
            }
        }

        return playingCard;
    }
}

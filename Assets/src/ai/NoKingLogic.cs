using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class NoKingLogic : BasicLogic
{
    public static CardVO getCardForPlay(ArrayList deck)
    {
        CardVO playingCard = null;
        CardVO card;
        if (GameModel.inst.cardsOnDeck.Count == 0)
        {
            int i;
            ArrayList sortedDeck = sortCardsByDecrease(deck);
            for (i = 0; i < sortedDeck.Count; i++)
            {
                card = sortedDeck[i] as CardVO;
                if (card.Suit != Config.HEARTS_SUIT)
                {
                    playingCard = card;
                    break;
                }
            }

            if (playingCard == null)
            {
                playingCard = sortedDeck[0] as CardVO;
            }
        }
        else
        {
            CardVO maxCard = getMaxCard();

            for (int a = 0; a < deck.Count; a++)
            {
                card = deck[a] as CardVO;
                if (card.Suit == maxCard.Suit)
                {
                    if (playingCard == null)
                    {
                        playingCard = card; // первая карта такой масти
                    }
                    else
                    {
                        if (card.Rank < maxCard.Rank)
                        {
                            if (card.Rank > playingCard.Rank) // если больше текущей, то отдаем ее
                            {
                                playingCard = card;
                            }

                            if (playingCard.Rank > maxCard.Rank) // если текущая карта той же масти, но больше максимальной, то берем ту что помешье
                            {
                                playingCard = card;
                            }
                        }
                    }

                }
            }

            if (playingCard == null)
            {
                for (int j = 0; j < deck.Count; j++)
                {
                    card = deck[j] as CardVO;
                    if (card.Rank == Config.KING_RANK && card.Suit == Config.HEARTS_SUIT) // если в колоде есть Кинг, то обязательно сбрасываем его
                    {
                        playingCard = card;
                        break;
                    }
                }
            }

            if (playingCard == null)
            {
                playingCard = sortCardsByDecrease(deck)[deck.Count - 1] as CardVO; // если нужной масти не найдено, ходим самой большой из тех, что есть
            }
        }

        return playingCard;
    }
    
}


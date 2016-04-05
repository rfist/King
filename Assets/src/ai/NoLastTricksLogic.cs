using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class NoLastTricksLogic: BasicLogic
{
    public static CardVO getCardForPlay(ArrayList deck)
    {
        CardVO playingCard = null;
        if (GameModel.inst.cardsOnDeck.Count == 0)
        {
            if (isLastTricks(deck)) // если это не последний ход, то ходим самой большой картой
            {
                playingCard = sortCardsByDecrease(deck)[0] as CardVO;
            }
            else
            {
                playingCard = sortCardsByDecrease(deck)[deck.Count - 1] as CardVO;           
            }            
        }
        else
        {
            CardVO maxCard = getMaxCard();
            CardVO card;

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
                        if (isLastTricks(deck))
                        {
                            if (card.Rank < maxCard.Rank && playingCard.Rank < card.Rank)
                            {
                                playingCard = card;
                            }
                        }
                        else
                        {
                            if (card.Rank > playingCard.Rank)
                            {
                                playingCard = card;
                            }
                        }

                        if (GameModel.inst.cardsOnDeck.Count == 3 && playingCard.Rank > maxCard.Rank && card.Rank > playingCard.Rank)
                        {
                            playingCard = card; // если ходим последними и забираем, то сбрасываем самую большую карту
                        }

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

    static bool isLastTricks(ArrayList deck)
    {
        return deck.Count <= 2;
    }
}


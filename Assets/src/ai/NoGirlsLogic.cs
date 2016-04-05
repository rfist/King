using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class NoGirlsLogic : BasicLogic
{
    public static CardVO getCardForPlay(ArrayList deck)
    {
        CardVO playingCard = null;
        if (GameModel.inst.cardsOnDeck.Count == 0)
        {
            playingCard = sortCardsByDecrease(deck)[0] as CardVO;
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
                        if (playingCard.Rank > maxCard.Rank && isQueenCard(playingCard) && !isQueenCard(card))
                        {
                            playingCard = card; // заменяем даму, если это возможно
                        }

                        if (card.Rank < maxCard.Rank)
                        {
                            if (!isQueenCard(playingCard))
                            {
                                playingCard = card;
                            }
                            else
                            {
                                if (isQueenCard(card))
                                {
                                    playingCard = card;
                                }
                            }
                        }

                        if (GameModel.inst.cardsOnDeck.Count == 3 && playingCard.Rank > maxCard.Rank && !isQueenCard(card) && card.Rank > maxCard.Rank) 
                        {
                            playingCard = card; // если ходим последними и забираем, то сбрасываем самую большую карту
                        }

                    }

                }
            }


            if (playingCard == null) // если нужной масти нет, то в первую очередь сбрасываем даму
            {
                for (int i = 0; i < deck.Count; i++)
                {
                    card = deck[i] as CardVO;
                    if (isQueenCard(card))
                    {
                        playingCard = card;
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


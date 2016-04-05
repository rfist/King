using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class NoBoysLogic : BasicLogic
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
            CardVO maxCard = GameModel.inst.cardsOnDeck[0] as CardVO;
            CardVO card;

            for (int m = 0; m < GameModel.inst.cardsOnDeck.Count; m++)  // находим самую сильную играющую карту
            {
                card = GameModel.inst.cardsOnDeck[m] as CardVO;
                if (card.Suit == maxCard.Suit && card.Rank > maxCard.Rank)
                {
                    maxCard = card;
                }
            }

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
                        if (playingCard.Rank > maxCard.Rank && isJackOrKing(playingCard) && !isJackOrKing(card))
                        {
                            playingCard = card; // заменяем короля или вальта, если это возможно
                        }

                        if (card.Rank < maxCard.Rank)
                        {
                            if (!isJackOrKing(playingCard))
                            {
                                playingCard = card;
                            }
                            else
                            {
                                if (card.Rank == Config.KING_RANK)
                                {
                                    playingCard = card;
                                }
                            }
                        }

                        if (GameModel.inst.cardsOnDeck.Count == 3 && playingCard.Rank > maxCard.Rank && !isJackOrKing(card) && card.Rank > maxCard.Rank)
                        {
                            playingCard = card;
                        }

                    }

                }
            }


            if (playingCard == null) // если нужной масти нет, то в первую очередь сбрасываем короля или валета
            {
                for (int i = 0; i < deck.Count; i++)
                {
                    card = deck[i] as CardVO;
                    if (card.Rank == Config.JACK_RANK)
                    {
                        playingCard = card;
                    }
                    if (card.Rank == Config.KING_RANK) // короля сбрасываем в первую очередь
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


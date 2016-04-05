using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class NoAnyLogic : BasicLogic
{
    public static CardVO getCardForPlay(ArrayList deck)
    {
        CardVO playingCard = null;
        CardVO playingHeartCard = null;
        if (GameModel.inst.cardsOnDeck.Count == 0)
        {
            for (int k = 0; k < deck.Count; k++)
            {
                CardVO card = deck[k] as CardVO;
                if (card.Suit != Config.HEARTS_SUIT)
                {
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
                else
                {
                    if (playingHeartCard == null)
                    {
                        playingHeartCard = card;
                    }
                    else
                    {
                        if (card.Rank < playingHeartCard.Rank) // отдаем самую меншьую карту
                        {
                            playingHeartCard = card;
                        }
                    }
                }
            }
            if (playingCard == null)
            {
                playingCard = playingHeartCard; // если нет никаких других карт, то ходим с чирвы
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
                        else
                        {
                            if (card.Rank < playingCard.Rank) // если больше максимальной, но меньше текущей
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

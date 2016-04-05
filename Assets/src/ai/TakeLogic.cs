using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class TakeLogic : BasicLogic
{
    public static CardVO getCardByTricksLogic(ArrayList deck)
    {
        CardVO playingCard = null;
        if (isFirstTurn)
        {
            ArrayList sorted = sortCardsByDecrease(deck);
            playingCard = sorted[sorted.Count - 1] as CardVO;
            if (!checkNoMoreBigger(playingCard)) // если в игре есть карты больше, то ходим с самой маленькой
            {
                playingCard = sorted[0] as CardVO;
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
                        if (card.Rank > maxCard.Rank && checkNoMoreBigger(card))
                        {
                            playingCard = card;
                        }
                        else
                        {
                            if (card.Rank < playingCard.Rank) // если больше максимальной, но меньше текущей
                            {
                                playingCard = card;
                            }
                        }

                        if (isLastTurn && card.Rank > maxCard.Rank)
                        {
                            if (card.Rank < playingCard.Rank || playingCard.Rank < maxCard.Rank)
                            {
                                playingCard = card; // если ходим последними, то все равно забираем
                            }                            
                        }
                    }

                }
            }

            if (playingCard == null)
            {
                playingCard = sortCardsByDecrease(deck)[0] as CardVO; // если нужной масти не найдено, ходим самой меньшей
            }
        }
        return playingCard;
    }

    public static CardVO getCardByHeartsLogic(ArrayList deck)
    {
        CardVO playingCard = null;
        CardVO smallestPlayingCard = null;
        CardVO card;
        ArrayList sorted = sortCardsByDecrease(deck);
        if (isFirstTurn)
        {           
            for (int i = 0; i < sorted.Count; i++)
            {
                card = sorted[i] as CardVO;
                if (card.Suit != Config.HEARTS_SUIT)
                {
                    playingCard = card;  // ходим с самой меньшей карты, но не чирвы
                    break;
                }
            }
            if (playingCard == null)
            {
                playingCard = sorted[sorted.Count - 1] as CardVO;
                if (!checkNoMoreBigger(playingCard)) // если это самая крупная чирва, ходим с нее, если нет, то сбрасываем самую маленькую
                {
                    playingCard = sorted[0] as CardVO;
                }
            }          
        }
        else
        {
            CardVO maxCard = getMaxCard();
            bool hasHearts = false;
            for (int j = 0; j < GameModel.inst.cardsOnDeck.Count; j++)
            {
                card = GameModel.inst.cardsOnDeck[j] as CardVO;
                if (card.Suit == Config.HEARTS_SUIT)
                {
                    hasHearts = true;
                    break;
                }
            }

            for (int a = 0; a < sorted.Count; a++)
            {
                card = sorted[a] as CardVO;

                if (hasHearts)
                {
                    if (card.Suit == maxCard.Suit && card.Rank > maxCard.Rank && checkNoMoreBigger(card)) // карта той же масти, и больше нее в игре сейчас нет
                    {
                        playingCard = card;
                        break;
                    }
                    if (isLastTurn && card.Rank > maxCard.Rank) // мы ходим последними, поэтому достаточно чтоб карта была больше максимальной
                    {
                        playingCard = card;
                        break;
                    }
                }
                else
                {
                    if (card.Suit == maxCard.Suit)
                    {
                        playingCard = card;
                        break;
                    }
                }

                if (smallestPlayingCard == null && card.Suit == maxCard.Suit)
                {
                    smallestPlayingCard = card;
                }
             }

            if (smallestPlayingCard == null)
            {
                smallestPlayingCard = sorted[0] as CardVO;
                for (int b = 0; b < sorted.Count; b++)
                {
                    card = sorted[b] as CardVO;
                    if (card.Suit != Config.HEARTS_SUIT)
                    {
                        smallestPlayingCard = card;
                        break;
                    }
                }               
            }

            if (playingCard == null)
            {
                playingCard = smallestPlayingCard; // если карты не найдено, ходим самой меньшей
            }
        }
        return playingCard;
    }

    public static CardVO getCardByBoysLogic(ArrayList deck)
    {
        CardVO playingCard = null;
        CardVO card = null;
        CardVO smallestPlayingCard = null;
        ArrayList sorted = sortCardsByDecrease(deck);
        if (isFirstTurn)
        {         
            playingCard = sorted[sorted.Count - 1] as CardVO;
            if (!checkNoMoreBigger(playingCard)) // если в игре есть карты больше, то ходим с самой маленькой
            {
                playingCard = sorted[0] as CardVO;
            }
        }
        else
        {
            CardVO maxCard = getMaxCard();
            bool hasBoys = false;
            for (int j = 0; j < GameModel.inst.cardsOnDeck.Count; j++)
            {
                card = GameModel.inst.cardsOnDeck[j] as CardVO;
                if (isJackOrKing(card))
                {
                    hasBoys = true;
                    break;
                }
            }

            for (int a = 0; a < sorted.Count; a++)
            {
                card = sorted[a] as CardVO;

                if (hasBoys)
                {
                    if (card.Suit == maxCard.Suit && card.Rank > maxCard.Rank && checkNoMoreBigger(card)) // карта той же масти, и больше нее в игре сейчас нет
                    {
                        playingCard = card;
                        break;
                    }
                    if (isLastTurn && card.Rank > maxCard.Rank) // мы ходим последними, поэтому достаточно чтоб карта была больше максимальной
                    {
                        playingCard = card;
                        break;
                    }
                }
                else
                {
                    if (card.Suit == maxCard.Suit)
                    {
                        playingCard = card;
                        break;
                    }
                }

                if (smallestPlayingCard == null && card.Suit == maxCard.Suit)
                {
                    smallestPlayingCard = card;
                }
            }

            if (smallestPlayingCard == null)
            {
                smallestPlayingCard = sorted[0] as CardVO;
                for (int b = 0; b < sorted.Count; b++)
                {
                    card = sorted[b] as CardVO;
                    if (!isJackOrKing(card))
                    {
                        smallestPlayingCard = card;
                        break;
                    }
                }
            }

            if (playingCard == null)
            {
                playingCard = smallestPlayingCard; // если карты не найдено, ходим самой меньшей
            }
        }
        return playingCard;
    }

    public static CardVO getCardByGirlsLogic(ArrayList deck)
    {
        CardVO playingCard = null;
        CardVO card = null;
        CardVO smallestPlayingCard = null;
        ArrayList sorted = sortCardsByDecrease(deck);
        if (isFirstTurn)
        {
            playingCard = sorted[sorted.Count - 1] as CardVO;
            if (!checkNoMoreBigger(playingCard)) // если в игре есть карты больше, то ходим с самой маленькой
            {
                playingCard = sorted[0] as CardVO;
            }
        }
        else
        {
            CardVO maxCard = getMaxCard();
            bool hasBoys = false;
            for (int j = 0; j < GameModel.inst.cardsOnDeck.Count; j++)
            {
                card = GameModel.inst.cardsOnDeck[j] as CardVO;
                if (isQueenCard(card))
                {
                    hasBoys = true;
                    break;
                }
            }

            for (int a = 0; a < sorted.Count; a++)
            {
                card = sorted[a] as CardVO;

                if (hasBoys)
                {
                    if (card.Suit == maxCard.Suit && card.Rank > maxCard.Rank && checkNoMoreBigger(card)) // карта той же масти, и больше нее в игре сейчас нет
                    {
                        playingCard = card;
                        break;
                    }
                    if (isLastTurn && card.Rank > maxCard.Rank) // мы ходим последними, поэтому достаточно чтоб карта была больше максимальной
                    {
                        playingCard = card;
                        break;
                    }
                }
                else
                {
                    if (card.Suit == maxCard.Suit)
                    {
                        playingCard = card;
                        break;
                    }
                }

                if (smallestPlayingCard == null && card.Suit == maxCard.Suit)
                {
                    smallestPlayingCard = card;
                }
            }

            if (smallestPlayingCard == null)
            {
                smallestPlayingCard = sorted[0] as CardVO;
                for (int b = 0; b < sorted.Count; b++)
                {
                    card = sorted[b] as CardVO;
                    if (!isQueenCard(card))
                    {
                        smallestPlayingCard = card;
                        break;
                    }
                }
            }

            if (playingCard == null)
            {
                playingCard = smallestPlayingCard; // если карты не найдено, ходим самой меньшей
            }
        }
        return playingCard;
    }

    public static CardVO getCardByLastTricksLogic(ArrayList deck)
    {
        CardVO playingCard = null;
        CardVO card = null;
        CardVO smallestPlayingCard = null;
        ArrayList sorted = sortCardsByDecrease(deck);
        if (isFirstTurn)
        {
            if (isLastOfTwoTurns(deck))
            {
                playingCard = sorted[sorted.Count - 1] as CardVO;
                if (!checkNoMoreBigger(playingCard)) // если в игре есть карты больше, то ходим с самой маленькой
                {
                    playingCard = sorted[0] as CardVO;
                }
            }
            else
            {
                playingCard = sorted[0] as CardVO;
            }
        }
        else
        {
            CardVO maxCard = getMaxCard();
            for (int a = 0; a < sorted.Count; a++)
            {
                card = sorted[a] as CardVO;

                if (isLastOfTwoTurns(deck))
                {
                    if (card.Suit == maxCard.Suit && card.Rank > maxCard.Rank && checkNoMoreBigger(card)) // карта той же масти, и больше нее в игре сейчас нет
                    {
                        playingCard = card;
                        break;
                    }
                    if (isLastTurn && card.Rank > maxCard.Rank) // мы ходим последними, поэтому достаточно чтоб карта была больше максимальной
                    {
                        playingCard = card;
                        break;
                    }
                }
                else
                {
                    if (card.Suit == maxCard.Suit)
                    {
                        playingCard = card;
                        break;
                    }
                }

                if (smallestPlayingCard == null && card.Suit == maxCard.Suit)
                {
                    smallestPlayingCard = card;
                }
            }
        }

        if (smallestPlayingCard == null)
        {
            smallestPlayingCard = sorted[0] as CardVO;
            for (int b = 0; b < sorted.Count; b++)
            {
                card = sorted[b] as CardVO;
                if (!isQueenCard(card))
                {
                    smallestPlayingCard = card;
                    break;
                }
            }
        }

        if (playingCard == null)
        {
            playingCard = smallestPlayingCard; // если карты не найдено, ходим самой меньшей
        }

        return playingCard;
    }

    public static CardVO getCardByKingLogic(ArrayList deck)
    {
        CardVO playingCard = null;
        CardVO card = null;
        CardVO kingCard = null;
        CardVO smallestPlayingCard = null;
        ArrayList sorted = sortCardsByDecrease(deck);
        if (isFirstTurn)
        {
            for (int i = 0; i < sorted.Count; i++)
            {
                card = sorted[i] as CardVO;
                if (card.Suit != Config.HEARTS_SUIT)
                {
                    playingCard = card;
                    break;
                }
                else
                {
                    if (smallestPlayingCard == null && !isKingCard(card))
                    {
                        smallestPlayingCard = card;
                    }

                    if (isKingCard(card) && checkNoMoreBigger(card))
                    {
                        smallestPlayingCard = card;
                    }
                }
            }

            if (playingCard == null)
            {
                if (smallestPlayingCard == null)
                {
                    playingCard = sorted[0] as CardVO;
                }
                else
                {
                    playingCard = smallestPlayingCard;
                }
            }
        }
        else
        {
            CardVO maxCard = getMaxCard();
            bool hasKing = false;

            for (int j = 0; j < GameModel.inst.cardsOnDeck.Count; j++)
            {
                card = GameModel.inst.cardsOnDeck[j] as CardVO;
                if (isKingCard(card))
                {
                    hasKing = true;
                    break;
                }
            }

            for (int j = 0; j < sorted.Count; j++)
            {
                card = sorted[j] as CardVO;

                if (card.Suit == maxCard.Suit)
                {
                    if (playingCard == null)
                    {
                        playingCard = card;
                    }
                    if (hasKing)
                    {
                        if ((isLastTurn && card.Rank > maxCard.Rank) || checkNoMoreBigger(card))
                        {
                            playingCard = card;
                        }
                    }
                }

                if (smallestPlayingCard == null && !isKingCard(card))
                {
                    smallestPlayingCard = card;
                }

                if (isKingCard(card))
                {
                    kingCard = card;
                }
            }

            if (smallestPlayingCard == null)
            {
                smallestPlayingCard = sorted[0] as CardVO;
            }

            if (playingCard == null)
            {
                if (kingCard == null)
                {
                    playingCard = smallestPlayingCard;
                }
                else
                {
                    playingCard = kingCard;
                }
            }
        }
        return playingCard;
    }

    public static CardVO getCardByTakeAllLogic(ArrayList deck)
    {
        CardVO playingCard = null;
        CardVO card = null;
        CardVO kingCard = null;
        CardVO smallestPlayingCard = null;
        ArrayList sorted = sortCardsByDecrease(deck);
        if (isFirstTurn)
        {
            for (int i = 0; i < sorted.Count; i++)
            {
                card = sorted[i] as CardVO;
                if (card.Suit != Config.HEARTS_SUIT)
                {
                    if (checkNoMoreBigger(card))
                    {
                        playingCard = card;
                    }
                    if (smallestPlayingCard == null || smallestPlayingCard.Suit == Config.HEARTS_SUIT)
                    {
                        smallestPlayingCard = card;
                    }                  
                }
                else
                {
                    if (smallestPlayingCard == null && !isKingCard(card))
                    {
                        smallestPlayingCard = card;
                    }

                    if (isKingCard(card) && checkNoMoreBigger(card))
                    {
                        smallestPlayingCard = card;
                    }
                }
            }

            if (playingCard == null)
            {
                if (smallestPlayingCard == null)
                {
                    playingCard = sorted[0] as CardVO;
                }
                else
                {
                    playingCard = smallestPlayingCard;
                }
            }
        }
        else
        {
            CardVO maxCard = getMaxCard();

            for (int a = 0; a < sorted.Count; a++)
            {
                card = sorted[a] as CardVO;

                if (isKingCard(card))
                {
                    kingCard = card;
                }

                if (card.Suit == maxCard.Suit)
                {
                    if (playingCard == null)
                    {
                        playingCard = card; // первая карта такой масти
                    }
                    else
                    {
                        if (card.Rank > maxCard.Rank && checkNoMoreBigger(card))
                        {
                            playingCard = card;
                        }

                        if (isLastTurn && card.Rank > maxCard.Rank)
                        {
                            if (card.Rank < playingCard.Rank || playingCard.Rank < maxCard.Rank)
                            {
                                playingCard = card; // если ходим последними, то все равно забираем
                            }
                        }
                    }

                }

                if (smallestPlayingCard == null && !isKingCard(card))
                {
                    smallestPlayingCard = card;
                }
            }

            if (smallestPlayingCard == null)
            {
                smallestPlayingCard = sorted[0] as CardVO;
            }

            if (playingCard == null)
            {
                if (kingCard == null)
                {
                    playingCard = smallestPlayingCard;
                }
                else
                {
                    playingCard = kingCard;
                }
            }
        }

        return playingCard;
    }
}
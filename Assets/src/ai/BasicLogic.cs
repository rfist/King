using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class BasicLogic
{
    static CardVO getMaxValueOfStack()
    {
        CardVO maxCard = null;
            
        if (GameModel.inst.cardsOnDeck.Count > 0)
        {
            maxCard = GameModel.inst.cardsOnDeck[0] as CardVO;

            for (int m = 0; m < GameModel.inst.cardsOnDeck.Count; m++)  // находим самую большую играющую карту
            {
                CardVO card = GameModel.inst.cardsOnDeck[m] as CardVO;
                if (card.Suit == maxCard.Suit && card.Rank > maxCard.Rank)
                {
                    maxCard = card;
                }
            }
     
            }

        return maxCard;
    }

    static protected ArrayList sortCardsByDecrease(ArrayList deck)
	{
        // ArrayList newSortedArray = new ArrayList();
        //for (int i= 0; i< deck.Count; i++)
        //{
        //	newSortedArray[i] = Number(String(Cards_Of_Player[number][i]).substr(1, 1) + String(Cards_Of_Player[number][i]).substr(0, 1));
        //}
        //      newSortedArray.sort();

        CardVO[] array = deck.ToArray(typeof(CardVO)) as CardVO[];
        // array = array.OrderBy(o => o.Suit).ThenBy(o => o.Rank).ToArray();
        array = array.OrderBy(o => o.Rank).ToArray();
        return new ArrayList(array);
	}

    static protected CardVO getMaxCard()
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

        return maxCard;
    } 
    
    static protected bool isFirstTurn
    {
        get
        {
            return GameModel.inst.cardsOnDeck.Count == 0;
        }
    }

    static protected bool isLastTurn
    {
        get
        {
            return GameModel.inst.cardsOnDeck.Count == 3;
        }
    }


    static protected bool checkNoMoreBigger(CardVO card)
    {
        CardVO historyCard;
        if (card.Rank == Config.ACE_RANK)
        {
            return true;
        }

        for (int rank = card.Rank + 1; rank <= Config.ACE_RANK; rank++)
        {
            bool isFindedInHistory = false;
            for (int j = 0; j < GameModel.inst.level.History.Count; j++)
            {
                historyCard = GameModel.inst.level.History[j] as CardVO;
                if (historyCard.Suit == card.Suit && historyCard.Rank == rank)
                {
                    isFindedInHistory = true;
                    break;
                }
            }
            if (!isFindedInHistory)
            {
                return false;
            }
        }

        return true;
    }

    static protected bool isJackOrKing(CardVO card)
    {
        return (card.Rank == Config.JACK_RANK || card.Rank == Config.KING_RANK);
    }

    static protected bool isQueenCard(CardVO card)
    {
        return card.Rank == Config.QUEEN_RANK;
    }

    static protected bool isKingCard(CardVO card)
    {
        return card.Rank == Config.KING_RANK && card.Suit == Config.HEARTS_SUIT;
    }    

    static protected bool isLastOfTwoTurns(ArrayList deck)
    {
        return deck.Count <= 2;
    }

}

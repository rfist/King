using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardStrategy
{
    public CardStrategy() {

        //Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>();
        // commands.Add(RuleModel.NO_TRICKS, new PlayTurnCommand());
    }

    public static CardVO getCardForPlay(ArrayList deck)
    {
        CardVO playingCard;

        switch (GameModel.inst.level.Strategy)
        {
            case GameStrategy.NO_TRICKS:
                playingCard = NoTricksLogic.getCardForPlay(deck);
                break;

            case GameStrategy.NO_HEARTS:
                playingCard = NoHeartsLogic.getCardForPlay(deck);
                break;

            case GameStrategy.NO_BOYS:
                playingCard = NoBoysLogic.getCardForPlay(deck);
                break;

            case GameStrategy.NO_GIRLS:
                playingCard = NoGirlsLogic.getCardForPlay(deck);
                break;

            case GameStrategy.NO_LAST_TRICKS:
                playingCard = NoLastTricksLogic.getCardForPlay(deck);
                break;

            case GameStrategy.NO_KING:
                playingCard = NoKingLogic.getCardForPlay(deck);
                break;

            case GameStrategy.NO_ANY:
                playingCard = NoAnyLogic.getCardForPlay(deck);
                break;

            case GameStrategy.TAKE_TRICKS:
                playingCard = TakeLogic.getCardByTricksLogic(deck);
                break;

            case GameStrategy.TAKE_HEARTS:
                playingCard = TakeLogic.getCardByHeartsLogic(deck);
                break;

            case GameStrategy.TAKE_BOYS:
                playingCard = TakeLogic.getCardByBoysLogic(deck);
                break;

            case GameStrategy.TAKE_GIRLS:
                playingCard = TakeLogic.getCardByGirlsLogic(deck);
                break;

            case GameStrategy.TAKE_LAST_TRICKS:
                playingCard = TakeLogic.getCardByLastTricksLogic(deck);
                break;

            case GameStrategy.TAKE_KING:
                playingCard = TakeLogic.getCardByKingLogic(deck);
                break;

            case GameStrategy.TAKE_ANY:
                playingCard = TakeLogic.getCardByTakeAllLogic(deck);
                break;

            default:
                playingCard = NoTricksLogic.getCardForPlay(deck);
                break;
        }


        return playingCard;
    }



}

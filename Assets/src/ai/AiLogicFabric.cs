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
                

            default:
                playingCard = NoTricksLogic.getCardForPlay(deck);
                break;
        }


        return playingCard;
    }



}

using UnityEngine;
using System.Collections;

public class PlayTurnCommand : ICommand
{

    public PlayTurnCommand() { }


    public void execute()
    {
        CardVO playingCard = CardStrategy.getCardForPlay(GameModel.inst.currentPlayer.Deck);
        AnimationHelper.makeTurn(playingCard);
    }

}

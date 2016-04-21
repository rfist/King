using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class ChooseLevelCommand : ICommand
{
    int level = 0;
    public void execute()
    {
        Debug.Log("ChooseLevelCommand");
        List<int> levelRange = new List<int>();
        for (int i = 0; i < GameModel.inst.currentPlayer.LevelsData.Count; i++)
        {
            if (!GameModel.inst.currentPlayer.LevelsData[i].IsPassed)
            {
                levelRange.Add(i);
                //GameModel.inst.level = GameModel.inst.currentPlayer.LevelsData[i];
                //break;
            }
        }
        level = levelRange[UnityEngine.Random.Range(0, levelRange.Count - 1)];
        Context.TextForDisplay = level.ToString();
        GameModel.inst.level = GameModel.inst.currentPlayer.LevelsData[level];
    }
}

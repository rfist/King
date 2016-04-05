using UnityEngine;
using System.Collections;

public class LevelVO
{
    public int Id = 0;
    public string RuleImage = "";
    public bool isNegative = false;
    public string[] Goals;
    public GameStrategy Strategy;
    public int FirstPlayer = 0;
    public int GoalCost = 0;
    public ArrayList History;


    public LevelVO(int Id, bool isNegative, GameStrategy Strategy, string RuleImage, int FirstPlayer, int GoalCost)
    {
        this.Id = Id;
        this.isNegative = isNegative;
        this.RuleImage = RuleImage;
        this.Strategy = Strategy;
        this.FirstPlayer = FirstPlayer;
        this.GoalCost = GoalCost;
        this.History = new ArrayList();
    }

    public LevelVO Clone
    {
        get
        {
            LevelVO clone = new LevelVO(Id, isNegative, Strategy, RuleImage, FirstPlayer, GoalCost);
            string[] targetArray = new string[Goals.Length];
            Goals.CopyTo(targetArray, 0);
            clone.Goals = targetArray;
            return clone;
        }
    }




}

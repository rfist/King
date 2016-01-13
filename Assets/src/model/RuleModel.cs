using UnityEngine;
using System.Collections;

public class RuleModel
{
    public static string GOAL_TRICK = "GOAL_TRICK";


    public static string NO_TRICKS = "NO_TRICKS";

    public RuleModel() { }



}

public enum GameStrategy
{
    NO_TRICKS,
    NO_HEARTS,
    Spades,
    Diamonds
}

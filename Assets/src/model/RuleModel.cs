using UnityEngine;
using System.Collections;

public class RuleModel
{
    public static string GOAL_TRICK = "GOAL_TRICK";


    public static string NO_TRICKS = "NO_TRICKS";
    public static string TRICK_7 = "TRICK_7";
    public static string TRICK_8 = "TRICK_8";
    public static string ANY = "ANY";

    public RuleModel() { }



}

public enum GameStrategy
{
    NO_TRICKS,
    NO_HEARTS,
    NO_BOYS,
    NO_GIRLS,
    NO_LAST_TRICKS,
    NO_KING,
    NO_ANY,
    TAKE_TRICKS,
    TAKE_HEARTS,
    TAKE_BOYS,
    TAKE_GIRLS,
    TAKE_LAST_TRICKS,
    TAKE_KING,
    TAKE_ANY
}

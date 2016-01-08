using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Config
{
    public static float DISTANCE_BETWEEN_CARDS = -0.15f;
    public static float DISTANCE_BETWEEN_CARDS_FOR_PLAYER = 0.5f;

    public static int PLAYER_A = 1;
    public static int PLAYER_B = 2;
    public static int PLAYER_C = 3;
    public static int PLAYER_ME = 0;

    // public static string[] NameOfPlayersPointsOnTable = string[] {"tableCardPlaceMe", ""};
    public static Dictionary<int, string> NameOfPlayersPointsOnTable = new Dictionary<int, string>()
    {
        {PLAYER_A, "tableCardPlaceA"},
        {PLAYER_B, "tableCardPlaceB"},
        {PLAYER_C, "tableCardPlaceC"},
        {PLAYER_ME, "tableCardPlaceMe"}
    };

    // events name
    public static string ON_LEVEL_CHANGED = "ON_LEVEL_CHANGED";
    public static string ON_SCORE_CHANGED = "ON_SCORE_CHANGED";
    public static string ON_TURN_ANIMATION_FINISHED = "ON_TURN_ANIMATION_FINISHED";
    public static string ON_GRAB_ANIMATION_FINISHED = "ON_GRAB_ANIMATION_FINISHED"; 

}

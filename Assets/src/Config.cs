using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Config
{
    public static float DISTANCE_BETWEEN_CARDS = -0.15f;
    public static float DISTANCE_BETWEEN_CARDS_FOR_PLAYER = 0.54f;

    public static bool IS_SOUNDS_ON = false;

    public static bool IS_AI_CARDS_OPEN = false;

    public static int DIAMONDS_SUIT = 1; // бубны
    public static int CLUBS_SUIT = 2; // трефа
    public static int SPADES_SUIT = 3; // пики
    public static int HEARTS_SUIT = 4;

    public static int JACK_RANK = 5;
    public static int QUEEN_RANK = 6;
    public static int KING_RANK = 7;
    public static int ACE_RANK = 8;

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


    public static string CREATE_CARD_POSITION = "tableCardPlaceStart";
    public static float SPEED_DISTRIBUTION = 0.2f;

    // events name
    public static string ON_LEVEL_CHANGED = "ON_LEVEL_CHANGED";
    public static string ON_SCORE_CHANGED = "ON_SCORE_CHANGED";
    public static string ON_TURN_ANIMATION_FINISHED = "ON_TURN_ANIMATION_FINISHED";
    public static string ON_GRAB_ANIMATION_FINISHED = "ON_GRAB_ANIMATION_FINISHED";
    public static string ON_START_ANIMATION_FINISHED = "ON_START_ANIMATION_FINISHED";
    public static string ON_SHOW_FINAL_SCORE_PLAYER_NUMBER = "ON_SHOW_FINAL_SCORE_PLAYER_NUMBER";

    public static string ON_GAME_END = "ON_GAME_END";
    public static string ON_GAME_RESTART = "ON_GAME_RESTART";

    public static string GAME_STATUS_ACTIVE = "GAME_STATUS_ACTIVE";
    public static string GAME_STATUS_SELECT_LEVEL = "GAME_STATUS_SELECT_LEVEL";
    public static string GAME_STATUS_FINISHED = "GAME_STATUS_FINISHED";
    public static string GAME_STATUS_SHOW_RESULTS = "GAME_STATUS_SHOW_RESULTS";
    public static string GAME_STATUS_READY_FOR_NEW = "GAME_STATUS_READY_FOR_NEW";

}

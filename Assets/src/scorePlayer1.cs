using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class scorePlayer1 : MonoBehaviour {

    Text txt;
    // Use this for initialization
    void Start () {
        txt = gameObject.GetComponent<Text>();
        txt.text = "";
        HideScore();
        EventManager.StartListening(EventManager.SHOW_SCORE, ShowScore);
        EventManager.StartListening(EventManager.HIDE_SCORE, HideScore);
    }

    void DestroyListeners()
    {
        EventManager.StopListening(EventManager.SHOW_SCORE, ShowScore);
        EventManager.StopListening(EventManager.HIDE_SCORE, HideScore);
    }

    private string GetScore()
    {
        string[] Params = gameObject.name.Split('.');
        int PlayerNumber = int.Parse(Params[0]);
        bool IsTotalScore = Params[1] == "total";
        bool IsFinalScore = GameModel.inst.GameStatus == Config.GAME_STATUS_SHOW_RESULTS;
        int score;
        if (IsTotalScore || IsFinalScore)
        {
            score = GameModel.inst.Players[PlayerNumber].Score;
        }
        else
        {
            score = GameModel.inst.Players[PlayerNumber].LevelScore;
        }

        return score.ToString();
    }

    void ShowScore()
    {
        string[] Params = gameObject.name.Split('.');
        bool IsFinalScore = Params[1] == "final";

        if (GameModel.inst.GameStatus == Config.GAME_STATUS_SHOW_RESULTS)
        {
            if (!IsFinalScore)
            {
                return;
            }
        }
        else
        {
            if (IsFinalScore)
            {
                return;
            }
        }

        txt.text = "" + GetScore();
        if (GetScore() == "")
        {
            return;
        }

        if (int.Parse(GetScore()) >= 0)
        {
            txt.color = Color.yellow;
        }
        else
        {
            txt.color = Color.cyan;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void HideScore()
    {
       txt.text = "";
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class scorePlayer1 : MonoBehaviour {

    Text txt;
    private int currentscore = -150;
    // Use this for initialization
    void Start () {
        txt = gameObject.GetComponent<Text>();
        txt.text = "" + GetScore();
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
        Boolean IsTotalScore = Params[1] == "total";
        int score;
        if (IsTotalScore)
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
        txt.text = "" + GetScore();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void HideScore()
    {
       txt.text = "";
    }
}

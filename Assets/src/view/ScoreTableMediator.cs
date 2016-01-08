using UnityEngine;
using System.Collections;

public class ScoreTableMediator : MonoBehaviour {

    SpriteRenderer scoreA;
    SpriteRenderer scoreB;
    SpriteRenderer scoreC;
    SpriteRenderer scoreD;
    SpriteRenderer scoreMain;
    // Use this for initialization
    void Start () {
        scoreA = gameObject.transform.Find("scoreA").gameObject.GetComponent<SpriteRenderer>();
        scoreB = gameObject.transform.Find("scoreB").gameObject.GetComponent<SpriteRenderer>();
        scoreC = gameObject.transform.Find("scoreC").gameObject.GetComponent<SpriteRenderer>();
        scoreD = gameObject.transform.Find("scoreD").gameObject.GetComponent<SpriteRenderer>();
        scoreMain = gameObject.transform.Find("scoreMain").gameObject.GetComponent<SpriteRenderer>();
        EventManager.StartListening(Config.ON_SCORE_CHANGED, onScoreChanged);
	}

    void onScoreChanged()
    {
        scoreA.sprite = getScoreSprite(GameModel.inst.Players[Config.PLAYER_A].Goals.Count);
        scoreB.sprite = getScoreSprite(GameModel.inst.Players[Config.PLAYER_B].Goals.Count);
        scoreC.sprite = getScoreSprite(GameModel.inst.Players[Config.PLAYER_C].Goals.Count);
        scoreD.sprite = getScoreSprite(GameModel.inst.Players[Config.PLAYER_ME].Goals.Count);
        scoreMain.sprite = getScoreSprite(GameModel.inst.level.Goals.Length);
    }

    Sprite getScoreSprite(int score)
    {

        switch (score)
        {
            case 1:
                return Resources.Load<Sprite>("Score/C01");
                 
            case 2:
                return Resources.Load<Sprite>("Score/C02");
                 
            case 3:
                return Resources.Load<Sprite>("Score/C03");
                 
            case 4:
                return Resources.Load<Sprite>("Score/C04");
                 
            case 5:
                return Resources.Load<Sprite>("Score/C05");
                 
            case 6:
                return Resources.Load<Sprite>("Score/C06");
                 
            case 7:
                return Resources.Load<Sprite>("Score/C07");
                 
            case 8:
                return Resources.Load<Sprite>("Score/C08");
                 
        }

        return Resources.Load<Sprite>("Score/C00");
    }

        // Update is called once per frame
        void Update () {
	
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class FinalScoreMediator : MonoBehaviour {

    public Text txt;
    public GameObject Snikers;
    public GameObject Mars;
    public GameObject Money;
    public int playerNumber;
    List<GameObject> list;

    private GameObject randomCandyBar;

    private static float APPEAR_TIME = 0.15f;
    private float Timer = APPEAR_TIME;
    private int AnimationCount;
    private SpriteRenderer r; 

    // Use this for initialization
    void Start () {
        float random = Random.Range(0f, 2.0f);
        if (random > 1.0f )
        {
            randomCandyBar = Snikers;
        }
        else
        {
            randomCandyBar = Mars;
        }
        list = new List<GameObject>();

        r = GetComponent<SpriteRenderer>();
        r.enabled = false;
        txt.text = "";
        EventManager.StartListening(Config.ON_SHOW_FINAL_SCORE_PLAYER_NUMBER + playerNumber.ToString(), CalculateAndShow);
        EventManager.StartListening(Config.ON_GAME_END, Destroy);
    }

    public void Destroy()
    {
        for (int i = 0; i < list.Count; i++)
        {
            GameObject go = list[i];
            Destroy(go);
        }
        list = new List<GameObject>();
        r.enabled = false;
        txt.text = "";
    }

    public void CalculateAndShow()
    {
        if (GetScore() < 0)
        {
            AnimationCount = System.Math.Abs(GetScore() / 45);
            if (AnimationCount < 1)
            {
                AnimationCount = 1;
            }
        }
        else
        {
            AnimationCount = GetScore() / 10;
        }

        if (GetScore() == 0)
        {
            ShowScore();
            TriggerNextScore();
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (AnimationCount > 0)
        {
            Timer -= Time.deltaTime;
            if (Timer <= 0)
            {
                Timer = APPEAR_TIME;
                GameObject go;
                // float x = Random.Range(-2.3f, -2.7f);
                //float y = Random.Range(1.65f, 1.3f);
                float x = Random.Range(0.0f, 0.4f);
                float y = Random.Range(0.0f, -0.35f);
                go = (GameObject)Instantiate(getPrize(), new Vector3(x + gameObject.transform.position.x, gameObject.transform.position.y + y, 0), Quaternion.identity);
                go.transform.parent = gameObject.transform;
                list.Add(go);
                AnimationCount--;
                AudioManager.inst.playAddPrize();
                if (AnimationCount == 0)
                {
                    ShowScore();
                    AudioManager.inst.playAllPrizesAdded();
                    TriggerNextScore();
                }
            }
        }
    }

    GameObject getPrize()
    {
        if (GetScore() < 0)
        {
            return randomCandyBar;
        }
        else
        {
            return Money;
        }
    }

    void ShowScore()
    {
        r.enabled = true;
        txt.text = "" + GetScore();
        if (GetScore() >= 0)
        {
            txt.color = Color.red;
            txt.text = "+" + txt.text;
        }
        else
        {
            txt.color = Color.cyan;
        }
    }

    void TriggerNextScore ()
    {
        int nextPlayer = playerNumber + 1;
        if (nextPlayer > 3)
        {
            nextPlayer = 0;
        }
        if (playerNumber != Config.PLAYER_ME)
        {
            EventManager.TriggerEvent(Config.ON_SHOW_FINAL_SCORE_PLAYER_NUMBER + nextPlayer.ToString());
        }
        else
        {
            GameModel.inst.GameStatus = Config.GAME_STATUS_READY_FOR_NEW;
        }
    }

    private int GetScore()
    {
        return GameModel.inst.Players[playerNumber].Score;
    }
}

using UnityEngine;
using System.Collections;

public class EndLevelScoreMediator : MonoBehaviour {

    public Material[] materials;
    public Renderer rend;

    private GameObject scorePlayerA;
    private GameObject scorePlayerB;
    private GameObject scorePlayerC;

    private SpriteRenderer _spriteRenderer;

    private bool isAnimationEnded = false;


    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[0];

        scorePlayerA = gameObject.transform.Find("ScorePlayerA").gameObject;
        rend = scorePlayerA.GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[0];

        scorePlayerB = gameObject.transform.Find("ScorePlayerB").gameObject;
        rend = scorePlayerB.GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[0];

        scorePlayerC = gameObject.transform.Find("ScorePlayerC").gameObject;
        rend = scorePlayerC.GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[0];

        EventManager.StartListening(EventManager.SHOW_SCORE_TABLE, show);
        gameObject.SetActive(false);

        _spriteRenderer = GetComponent<SpriteRenderer>();

        EventManager.StartListening(Config.ON_GAME_END, onRestore);
        // EventManager.StartListening(Config.ON_GAME_RESTART, onRestore);
    }

    void show()
    {
        AudioManager.inst.playResults();
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[1];

        rend = scorePlayerA.GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[1];

        rend = scorePlayerB.GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[1];

        rend = scorePlayerC.GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[1];

        gameObject.SetActive(true);
        
        Vector3 position = gameObject.transform.position;
        Vector3 newPosition = gameObject.transform.position;
        
        newPosition.y = position.y - (GetComponent<Renderer>().bounds.size.y * 2);
        gameObject.transform.position = newPosition;

        isAnimationEnded = false;
        iTween.MoveTo(gameObject, iTween.Hash("x", position.x, "y", position.y, "time", 1.5f, "oncomplete", "endMove")); 
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && isAnimationEnded)
        {
            Hide();
        }
    }

    void Hide()
    {
        if (GameModel.inst.GameStatus == Config.GAME_STATUS_SHOW_RESULTS)
        {
            return;
        }

        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[0];

        rend = scorePlayerA.GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[0];

        rend = scorePlayerB.GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[0];

        rend = scorePlayerC.GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[0];

        gameObject.SetActive(false);
        EventManager.TriggerEvent(EventManager.HIDE_SCORE_TABLE);
        EventManager.TriggerEvent(EventManager.HIDE_SCORE);

        if (GameModel.inst.GameStatus == Config.GAME_STATUS_FINISHED)
        {
            GameModel.inst.GameStatus = Config.GAME_STATUS_SHOW_RESULTS;
            EventManager.StopListening(EventManager.SHOW_SCORE_TABLE, show);
            EventManager.TriggerEvent(EventManager.SHOW_SCORE_TABLE);
            onGameFinished();
        }
    }

    // Update is called once per frame
    void Update () {
	
	}

    void endMove()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[0];

        rend = scorePlayerA.GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[0];

        rend = scorePlayerB.GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[0];

        rend = scorePlayerC.GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[0];
        Debug.Log("endMove animation complete" + GameModel.inst.GameStatus);
        EventManager.TriggerEvent(EventManager.SHOW_SCORE);

        isAnimationEnded = true;
        if (GameModel.inst.GameStatus == Config.GAME_STATUS_SHOW_RESULTS)
        {
            EventManager.TriggerEvent(Config.ON_SHOW_FINAL_SCORE_PLAYER_NUMBER + 1.ToString());
        }
    }

    void onGameFinished()
    {              
        _spriteRenderer.sprite = Resources.Load<Sprite>("scoreFinal");
        show();
    }

    void onRestore()
    {
        _spriteRenderer.sprite = Resources.Load<Sprite>("score");
        EventManager.StartListening(EventManager.SHOW_SCORE_TABLE, show);
    }


}

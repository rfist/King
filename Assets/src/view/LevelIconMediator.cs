using UnityEngine;
using System.Collections;

public class LevelIconMediator : MonoBehaviour {

    public int LevelNumber;
    
	// Use this for initialization
	void Start () {
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        if (LevelNumber > 6)
        {
            renderer.sprite = Resources.Load<Sprite>("Mars");
        }
        EventManager.StartListening(EventManager.SHOW_SELECT_WINDOW, Show);
    }

    void Show()
    {        
        gameObject.SetActive(!GameModel.inst.currentPlayer.LevelsData[LevelNumber].IsPassed);
    }

    // Update is called once per frame
    void Update () {
        if ((GameModel.inst.level.Id - 1) == LevelNumber && gameObject.activeSelf &&  GameModel.inst.FirstPlayer != Config.PLAYER_ME)
        {
            Invoke("PrepareToStartLevel", 0.5f);
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && GameModel.inst.currentPlayer.Id == Config.PLAYER_ME)
        {
            GameModel.inst.level = GameModel.inst.currentPlayer.LevelsData[LevelNumber];
            PrepareToStartLevel();
        }
    }

    void PrepareToStartLevel()
    {       
       if ((GameModel.inst.level.Id - 1) == LevelNumber)
        {
            gameObject.SetActive(false);
            Invoke("StartLevel", 0.5f);
        }
    }

    void StartLevel()
    {
        GameModel.inst.level.History = new ArrayList();
        GameModel.inst.currentPlayer.LevelsData[LevelNumber].IsPassed = true;
        GameController.inst.startGame();
        EventManager.TriggerEvent(Config.ON_LEVEL_CHANGED);
        EventManager.TriggerEvent(EventManager.HIDE_SELECT_WINDOW);
        GameModel.inst.GameStatus = Config.GAME_STATUS_ACTIVE;
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Context : MonoBehaviour {

    // Use this for initialization
    public int startLevel = 1;

	void Start () {
        GameModel.inst.GameStatus = Config.GAME_STATUS_ACTIVE;
        initPlayers();
        initCards();
        initLevels();
        GameController.inst.currentLevelIndex = startLevel - 2;
        GameController.inst.changeLevel();
        //CommandManager.inst.addCommand(typeof(ShuffleDeckCommand));
        //CommandManager.inst.addCommand(typeof(DistributeCardsCommand));
        //GameModel.inst.currentPlayer = GameModel.inst.Players[GameModel.inst.level.FirstPlayer]; //GameModel.inst.Players[Config.PLAYER_ME];
        GameController.inst.addListeners();
    }

    void initLevels()
    {
        LevelVO level;

        GameModel.inst.LevelsData = new List<LevelVO>(); 
        GameModel.inst.LevelsDataForCount = new List<LevelVO>(); 

        level = new LevelVO(1, true, GameStrategy.NO_TRICKS, "Levels/Level1", Config.PLAYER_A, -20);
        level.Goals = new string[] { RuleModel.GOAL_TRICK, RuleModel.GOAL_TRICK, RuleModel.GOAL_TRICK, RuleModel.GOAL_TRICK, RuleModel.GOAL_TRICK, RuleModel.GOAL_TRICK, RuleModel.GOAL_TRICK, RuleModel.GOAL_TRICK };
        GameModel.inst.LevelsData.Add(level);
        GameModel.inst.LevelsDataForCount.Add(level.Clone);       

        level = new LevelVO(2, true, GameStrategy.NO_HEARTS, "Levels/Level2", Config.PLAYER_B, -20);
        level.Goals = new string[] { "4_1", "4_2", "4_3", "4_4", "4_5", "4_6", "4_7", "4_8" };
        GameModel.inst.LevelsData.Add(level);
        GameModel.inst.LevelsDataForCount.Add(level.Clone);

        level = new LevelVO(3, true, GameStrategy.NO_BOYS, "Levels/Level3", Config.PLAYER_C, -20);
        level.Goals = new string[] { "4_7", "4_5", "3_7", "3_5", "2_7", "2_5", "1_7", "1_5" };
        GameModel.inst.LevelsData.Add(level);
        GameModel.inst.LevelsDataForCount.Add(level.Clone);

        level = new LevelVO(4, true, GameStrategy.NO_GIRLS, "Levels/Level4", Config.PLAYER_ME, -40);
        level.Goals = new string[] { "4_6", "3_6", "2_6", "1_6" };
        GameModel.inst.LevelsData.Add(level);
        GameModel.inst.LevelsDataForCount.Add(level.Clone);

        level = new LevelVO(5, true, GameStrategy.NO_LAST_TRICKS, "Levels/Level5", Config.PLAYER_A, -80);
        level.Goals = new string[] { RuleModel.TRICK_7, RuleModel.TRICK_8 };
        GameModel.inst.LevelsData.Add(level);
        GameModel.inst.LevelsDataForCount.Add(level.Clone);

        level = new LevelVO(6, true, GameStrategy.NO_KING, "Levels/Level6", Config.PLAYER_B, -160);
        level.Goals = new string[] { "4_7" };
        GameModel.inst.LevelsData.Add(level);
        GameModel.inst.LevelsDataForCount.Add(level.Clone);

        level = new LevelVO(7, true, GameStrategy.NO_ANY, "Levels/Level7", Config.PLAYER_C, 0);
        level.Goals = new string[] { RuleModel.ANY, RuleModel.ANY, RuleModel.ANY, RuleModel.ANY, RuleModel.ANY, RuleModel.ANY, RuleModel.ANY, RuleModel.ANY };
        GameModel.inst.LevelsData.Add(level);
        GameModel.inst.LevelsDataForCount.Add(level.Clone);

        level = new LevelVO(8, false, GameStrategy.TAKE_TRICKS, "Levels/Level8", Config.PLAYER_A, 20);
        level.Goals = new string[] { RuleModel.GOAL_TRICK, RuleModel.GOAL_TRICK, RuleModel.GOAL_TRICK, RuleModel.GOAL_TRICK, RuleModel.GOAL_TRICK, RuleModel.GOAL_TRICK, RuleModel.GOAL_TRICK, RuleModel.GOAL_TRICK };
        GameModel.inst.LevelsData.Add(level);

        level = new LevelVO(9, false, GameStrategy.TAKE_HEARTS, "Levels/Level9", Config.PLAYER_B, 20);
        level.Goals = new string[] { "4_1", "4_2", "4_3", "4_4", "4_5", "4_6", "4_7", "4_8" };
        GameModel.inst.LevelsData.Add(level);

        level = new LevelVO(10, false, GameStrategy.TAKE_BOYS, "Levels/Level10", Config.PLAYER_C, 20);
        level.Goals = new string[] { "4_7", "4_5", "3_7", "3_5", "2_7", "2_5", "1_7", "1_5" };
        GameModel.inst.LevelsData.Add(level);

        level = new LevelVO(11, false, GameStrategy.TAKE_GIRLS, "Levels/Level11", Config.PLAYER_ME, 40);
        level.Goals = new string[] { "4_6", "3_6", "2_6", "1_6" };
        GameModel.inst.LevelsData.Add(level);

        level = new LevelVO(12, false, GameStrategy.TAKE_LAST_TRICKS, "Levels/Level12", Config.PLAYER_A, 80);
        level.Goals = new string[] { RuleModel.TRICK_7, RuleModel.TRICK_8 };
        GameModel.inst.LevelsData.Add(level);

        level = new LevelVO(13, false, GameStrategy.TAKE_KING, "Levels/Level13", Config.PLAYER_B, 160);
        level.Goals = new string[] { "4_7" };
        GameModel.inst.LevelsData.Add(level);

        level = new LevelVO(14, false, GameStrategy.TAKE_ANY, "Levels/Level14", Config.PLAYER_C, 0);
        level.Goals = new string[] { RuleModel.ANY, RuleModel.ANY, RuleModel.ANY, RuleModel.ANY, RuleModel.ANY, RuleModel.ANY, RuleModel.ANY, RuleModel.ANY };
        GameModel.inst.LevelsData.Add(level);

        for (int playerIndex = 0; playerIndex < 4; playerIndex++)
        {
            for (int i = 0; i < GameModel.inst.LevelsData.Count; i++)
            {
                GameModel.inst.Players[playerIndex].LevelsData.Add(GameModel.inst.LevelsData[i].Clone);
            }
        }
    }


    // TODO: rewrite in future, add player selector
    void initPlayers ()
    {

        PlayerVO playerD = new PlayerVO(Config.PLAYER_ME, "", "1", "PlayerMe");
        GameModel.inst.Players[Config.PLAYER_ME] = playerD;

        if (SelectPlayerContext.Count < 4)
        {
            PlayerVO playerA = new PlayerVO(Config.PLAYER_A, "", "101", "PlayerA");
            PlayerVO playerB = new PlayerVO(Config.PLAYER_B, "", "102", "PlayerB");
            PlayerVO playerC = new PlayerVO(Config.PLAYER_C, "", "103", "PlayerC");
            GameModel.inst.Players[Config.PLAYER_A] = playerA;
            GameModel.inst.Players[Config.PLAYER_B] = playerB;
            GameModel.inst.Players[Config.PLAYER_C] = playerC;
        }

        for (int i = 0; i < GameModel.inst.Players.Length; i++)
        {
            if (i != Config.PLAYER_ME)
            {
                GameObject container = GameObject.Find(GameModel.inst.Players[i].Container);
                GameObject avatar = container.transform.Find("Avatar").gameObject;
                SpriteRenderer renderer = avatar.GetComponent<SpriteRenderer>();
                renderer.sprite = Resources.Load<Sprite>("PLayers/" + GameModel.inst.Players[i].ImageName);

                GameObject scoreContainer = GameObject.Find("EndLevelScore");
                GameObject avatarScore = scoreContainer.transform.Find("Score" + GameModel.inst.Players[i].Container).gameObject;
                SpriteRenderer rendererScore = avatarScore.GetComponent<SpriteRenderer>();
                rendererScore.sprite = Resources.Load<Sprite>("PLayers/" + GameModel.inst.Players[i].ImageName);
            }
        }
        
        Debug.Log("players init!");
    }


    void initCards()
    {
        int i = 0;
        for (int Suit = 1; Suit <= 4; Suit++)
        {
            for (int Rank = 1; Rank <= 8; Rank++)
            { 
                GameModel.inst.Deck[i] = new CardVO(Rank, Suit, "Card" + i.ToString());                
                i++;
            }

        }
        Debug.Log("cards init!");

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if (GameModel.inst.GameStatus == Config.GAME_STATUS_READY_FOR_NEW)
            {
                EventManager.TriggerEvent(Config.ON_GAME_END);
                SceneManager.LoadScene("MainMenu");
            }
            Debug.Log("Pressed left click.");
        }
    }

    public static string TextForDisplay = "";
    void OnGUI()
    {
        //GUI.Box(new Rect(Screen.width - 300, 0, 300, 50), "TextForDisplay " + TextForDisplay);
    }
}

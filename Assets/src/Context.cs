using UnityEngine;
using System.Collections;

public class Context : MonoBehaviour {

	// Use this for initialization
	void Start () {
        initPLayers();
        initCards();
        initLevels();        
        GameController.inst.changeLevel();
        CommandManager.inst.addCommand(typeof(ShuffleDeckCommand));
        CommandManager.inst.addCommand(typeof(DistributeCardsCommand));
        GameModel.inst.currentPlayer = GameModel.inst.Players[GameModel.inst.level.FirstPlayer]; //GameModel.inst.Players[Config.PLAYER_ME];
        GameController.inst.startGame();
    }

    void initLevels()
    {
        LevelVO level;

        level = new LevelVO(1, true, GameStrategy.NO_TRICKS, "Levels/Level1", Config.PLAYER_A);
        level.Goals = new string[] { RuleModel.GOAL_TRICK, RuleModel.GOAL_TRICK, RuleModel.GOAL_TRICK, RuleModel.GOAL_TRICK, RuleModel.GOAL_TRICK, RuleModel.GOAL_TRICK, RuleModel.GOAL_TRICK, RuleModel.GOAL_TRICK };
        GameModel.inst.LevelsData.Add(level);

        level = new LevelVO(2, true, GameStrategy.NO_HEARTS, "Levels/Level2", Config.PLAYER_B);
        level.Goals = new string[] { "4_1", "4_2", "4_3", "4_4", "4_5", "4_6", "4_7", "4_8" };
        GameModel.inst.LevelsData.Add(level);

        level = new LevelVO(3, true, GameStrategy.NO_BOYS, "Levels/Level3", Config.PLAYER_C);
        level.Goals = new string[] { "4_7", "4_5", "3_7", "3_5", "2_7", "2_5", "1_7", "1_5" };
        GameModel.inst.LevelsData.Add(level);

        level = new LevelVO(4, true, GameStrategy.NO_GIRLS, "Levels/Level4", Config.PLAYER_ME);
        level.Goals = new string[] { "4_6", "3_6", "2_6", "1_6" };
        GameModel.inst.LevelsData.Add(level);

        level = new LevelVO(5, true, GameStrategy.NO_LAST_TRICKS, "Levels/Level5", Config.PLAYER_A);
        level.Goals = new string[] { RuleModel.TRICK_7, RuleModel.TRICK_8 };
        GameModel.inst.LevelsData.Add(level);

        level = new LevelVO(6, true, GameStrategy.NO_KING, "Levels/Level6", Config.PLAYER_B);
        level.Goals = new string[] { "4_7" };
        GameModel.inst.LevelsData.Add(level);

        level = new LevelVO(7, true, GameStrategy.NO_ANY, "Levels/Level7", Config.PLAYER_C);
        level.Goals = new string[] { RuleModel.ANY, RuleModel.ANY, RuleModel.ANY, RuleModel.ANY, RuleModel.ANY, RuleModel.ANY, RuleModel.ANY, RuleModel.ANY };
        GameModel.inst.LevelsData.Add(level);
    }


    // TODO: rewrite in future, add player selector
    void initPLayers ()
    {
        PlayerVO playerA = new PlayerVO(Config.PLAYER_A, "", "101", "PlayerA");
        PlayerVO playerB = new PlayerVO(Config.PLAYER_B, "", "102", "PlayerB");
        PlayerVO playerC = new PlayerVO(Config.PLAYER_C, "", "103", "PlayerC");
        PlayerVO playerD = new PlayerVO(Config.PLAYER_ME, "", "1", "PlayerMe");

        GameModel.inst.Players[Config.PLAYER_ME] = playerD;
        GameModel.inst.Players[Config.PLAYER_A] = playerA;
        GameModel.inst.Players[Config.PLAYER_B] = playerB;
        GameModel.inst.Players[Config.PLAYER_C] = playerC;


        for (int i = 0; i < GameModel.inst.Players.Length; i++)
        {
            if (i != Config.PLAYER_ME)
            {
                GameObject container = GameObject.Find(GameModel.inst.Players[i].Container);
                GameObject avatar = container.transform.Find("Avatar").gameObject;
                SpriteRenderer renderer = avatar.GetComponent<SpriteRenderer>();
                //SpriteRenderer renderer = avatar.AddComponent<SpriteRenderer>();
                renderer.sprite = Resources.Load<Sprite>("PLayers/" + GameModel.inst.Players[i].ImageName);
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
	
	}
}

using UnityEngine;
using System.Collections;

public class Context : MonoBehaviour {

	// Use this for initialization
	void Start () {
        initPLayers();
        initCards();
        initLevels();
        EventManager.TriggerEvent(Config.ON_SCORE_CHANGED);
        CommandManager.inst.addCommand(typeof(ShuffleDeckCommand));
        CommandManager.inst.addCommand(typeof(DistributeCardsCommand));
        GameModel.inst.currentPlayer = GameModel.inst.Players[Config.PLAYER_ME];
        GameController.inst.startGame();
    }

    void initLevels()
    {
        // LevelVO level = new LevelVO(1, true, GameStrategy.NO_TRICKS, "Levels/Level1");
        // level.Goals = new string[] { RuleModel.GOAL_TRICK, RuleModel.GOAL_TRICK, RuleModel.GOAL_TRICK, RuleModel.GOAL_TRICK, RuleModel.GOAL_TRICK, RuleModel.GOAL_TRICK , RuleModel.GOAL_TRICK, RuleModel.GOAL_TRICK };

        LevelVO level = new LevelVO(2, true, GameStrategy.NO_HEARTS, "Levels/Level2");
        level.Goals = new string[] { "4_1", "4_2", "4_3", "4_4", "4_5", "4_6", "4_7", "4_8" };
        GameModel.inst.level = level;      
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

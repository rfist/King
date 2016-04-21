using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameModel
{

    private static GameModel instance;
    public LevelVO level;
    public bool IsOriginalKing = false;
    public PlayerVO currentPlayer;
    public CardVO[] Deck = new CardVO[32];
    public ArrayList cardsOnDeck = new ArrayList();
    public PlayerVO[] Players = new PlayerVO[4];
    public RuleModel ruleModel;
    public List<LevelVO> LevelsData = new List<LevelVO>(); 
    public List<LevelVO> LevelsDataForCount = new List<LevelVO>(); 
    public string GameStatus = Config.GAME_STATUS_ACTIVE;
    public int FirstPlayer = 0;

    public GameModel() {
        ruleModel = new RuleModel();
    }

    public static GameModel inst
    {
        get
        {
            if (instance == null)
            {
                instance = new GameModel();
            }
            return instance;
        }
    }
}
﻿using UnityEngine;
using System.Collections;


public class GameModel
{

    private static GameModel instance;
    public LevelVO level;
    public PlayerVO currentPlayer;
    public CardVO[] Deck = new CardVO[32];
    public ArrayList cardsOnDeck = new ArrayList();
    public PlayerVO[] Players = new PlayerVO[4];
    public RuleModel ruleModel;

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
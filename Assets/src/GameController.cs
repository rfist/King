﻿using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class GameController
{
    private static GameController instance;
    public GameController() { }
    private int _currentLevelIndex = -1;


    public static GameController inst
    {
        get
        {
            if (instance == null)
            {
                instance = new GameController();
            }
            return instance;
        }
    }

    public bool checkIfTurnValid(CardVO card)
    {
        return true;
    }

    public void changeLevel()
    {
        _currentLevelIndex++;
        GameModel.inst.level = GameModel.inst.LevelsData[_currentLevelIndex];
        EventManager.TriggerEvent(Config.ON_SCORE_CHANGED);
    }

    public void startGame()
    {
        EventManager.StartListening(Config.ON_TURN_ANIMATION_FINISHED, onTurnFinished);
        EventManager.StartListening(Config.ON_GRAB_ANIMATION_FINISHED, onGrabFinished);
    }

    void onGrabFinished()
    {
        Debug.Log("onGrabFinished " + GameModel.inst.cardsOnDeck.Count);
        if (GameModel.inst.cardsOnDeck.Count == 0)
        {
            EventManager.TriggerEvent(Config.ON_SCORE_CHANGED);
            // TODO: check if level ended
            if (GameModel.inst.currentPlayer.Deck.Count == 0 || GameModel.inst.level.Goals.Length == 0)
            {
                CommandManager.inst.addCommand(typeof(EndRoundCommand));
            }
            else if (GameModel.inst.currentPlayer.Id != Config.PLAYER_ME)
            {
                CommandManager.inst.addCommand(typeof(PlayTurnCommand));
            }
        }
    }

    void onTurnFinished()
    {
        Debug.Log("turn finished");
        if (GameModel.inst.cardsOnDeck.Count >= GameModel.inst.Players.Length)
        {
            Debug.Log("all players makes their turns!");
            endRound();
        }
        else
        {
            int currentPlayer = GameModel.inst.currentPlayer.Id;
            currentPlayer++;
            if (currentPlayer >= GameModel.inst.Players.Length)
            {
                currentPlayer = 0;
            }
            Debug.Log("turn finished " + currentPlayer);
            GameModel.inst.currentPlayer = GameModel.inst.Players[currentPlayer];
            if (GameModel.inst.currentPlayer.Id != Config.PLAYER_ME)
            {
                CommandManager.inst.addCommand(typeof(PlayTurnCommand));
            }
        }        
    }

    void endRound()
    {
        PlayerVO grabber;
        CardVO maxCard = GameModel.inst.cardsOnDeck[0] as CardVO;

        string[] goals = new string[4]; 
        for (int i = 0; i < GameModel.inst.cardsOnDeck.Count; i++)
        {
            CardVO currentCard = GameModel.inst.cardsOnDeck[i] as CardVO;
            string goalType = currentCard.Suit + "_"  + currentCard.Rank;
            goals[i] = goalType;
            if (maxCard.Suit == currentCard.Suit && currentCard.Rank > maxCard.Rank)
            {
                maxCard = GameModel.inst.cardsOnDeck[i] as CardVO;       
            }
        }
        grabber = maxCard.Owner;
        // Count results
        int numIndex;
        numIndex = Array.IndexOf(GameModel.inst.level.Goals, RuleModel.GOAL_TRICK);
        if (numIndex > -1)
        {
            grabber.Goals.Add(RuleModel.GOAL_TRICK);
            GameModel.inst.level.Goals = GameModel.inst.level.Goals.Where((val, idx) => idx != numIndex).ToArray();
        }

        for (int j = 0; j < goals.Length; j++)
        {
            numIndex = Array.IndexOf(GameModel.inst.level.Goals, goals[j]);
            if (numIndex > -1)
            {
                grabber.Goals.Add(goals[j]);
                GameModel.inst.level.Goals = GameModel.inst.level.Goals.Where((val, idx) => idx != numIndex).ToArray();
            }
        }

        // check last tricks
        numIndex = Array.IndexOf(GameModel.inst.level.Goals, RuleModel.TRICK_7);
        if (grabber.Deck.Count == 2 && numIndex > -1)
        {
            grabber.Goals.Add(RuleModel.TRICK_7);
            GameModel.inst.level.Goals = GameModel.inst.level.Goals.Where((val, idx) => idx != numIndex).ToArray();
        }
        numIndex = Array.IndexOf(GameModel.inst.level.Goals, RuleModel.TRICK_8);
        if (grabber.Deck.Count == 1 && numIndex > -1)
        {
            grabber.Goals.Add(RuleModel.TRICK_8);
            GameModel.inst.level.Goals = GameModel.inst.level.Goals.Where((val, idx) => idx != numIndex).ToArray();
        }

        // all rules together
        numIndex = Array.IndexOf(GameModel.inst.level.Goals, RuleModel.ANY);
        if (numIndex > -1)
        {
            grabber.Goals.Add(goals);
            GameModel.inst.level.Goals = GameModel.inst.level.Goals.Where((val, idx) => idx != numIndex).ToArray();
        }

        GameModel.inst.currentPlayer = grabber;        
        Debug.Log("max card " + maxCard.Id + " grabber " + grabber.Id + " left cards " + grabber.Deck.Count);
        AnimationHelper.grabCards(grabber);
    }


}

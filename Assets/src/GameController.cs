using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class GameController
{
    private static GameController instance;
    public GameController() { }
    public int currentLevelIndex = -1;


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
        if (GameModel.inst.level.Strategy == GameStrategy.NO_HEARTS || GameModel.inst.level.Strategy == GameStrategy.NO_ANY 
            || GameModel.inst.level.Strategy == GameStrategy.TAKE_HEARTS || GameModel.inst.level.Strategy == GameStrategy.TAKE_ANY 
            || GameModel.inst.level.Strategy == GameStrategy.NO_KING|| GameModel.inst.level.Strategy == GameStrategy.TAKE_KING)
        {
            if (GameModel.inst.cardsOnDeck.Count == 0 && card.Suit == Config.HEARTS_SUIT &&
                (card.Owner.hasSuit(Config.DIAMONDS_SUIT) || card.Owner.hasSuit(Config.CLUBS_SUIT) || card.Owner.hasSuit(Config.SPADES_SUIT)) // если есть какая-то масть кроме чирвы, то с чирвы ходить нельзя
                )
            {
                return false;
            }
       }

        if (GameModel.inst.level.Strategy == GameStrategy.NO_KING || GameModel.inst.level.Strategy == GameStrategy.NO_ANY || GameModel.inst.level.Strategy == GameStrategy.TAKE_KING || GameModel.inst.level.Strategy == GameStrategy.TAKE_ANY)
        {
            bool hasKingCard = false;
            bool hasSameSuite = false;
            CardVO playerCard;
            if (GameModel.inst.cardsOnDeck.Count > 0)
            {
                for (int i = 0; i < GameModel.inst.Players[Config.PLAYER_ME].Deck.Count; i++)
                {
                    playerCard = GameModel.inst.Players[Config.PLAYER_ME].Deck[i] as CardVO;
                    if (playerCard.Suit == Config.HEARTS_SUIT && playerCard.Rank == Config.KING_RANK)
                    {
                        hasKingCard = true;
                    }
                    if (playerCard.Suit == (GameModel.inst.cardsOnDeck[0] as CardVO).Suit)
                    {
                        hasSameSuite = true;
                    }
                }

                if ((GameModel.inst.cardsOnDeck[0] as CardVO).Suit != card.Suit && !hasSameSuite && hasKingCard)
                {
                    return card.Suit == Config.HEARTS_SUIT && card.Rank == Config.KING_RANK;
                }
            }
        }


        if (GameModel.inst.cardsOnDeck.Count == 0)
        {
            return true;
        }
        CardVO firstCatd = GameModel.inst.cardsOnDeck[0] as CardVO;
        if (card.Suit == firstCatd.Suit || !card.Owner.hasSuit(firstCatd.Suit))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void changeLevel()
    {
        if (GameModel.inst.IsOriginalKing)
        {
            currentLevelIndex++;
        }
        else
        {
            currentLevelIndex = 1;
        }
        bool isOriginalKingEnd = currentLevelIndex > 13 && GameModel.inst.IsOriginalKing;
        bool isAllLevelsPassed = true;
        for (int levelId = 0; levelId < GameModel.inst.Players[Config.PLAYER_ME].LevelsData.Count; levelId++)
        {
            if (!GameModel.inst.Players[Config.PLAYER_ME].LevelsData[levelId].IsPassed)
            {
                isAllLevelsPassed = false;
            }
        }
        bool isSelectedKingEnd = !GameModel.inst.IsOriginalKing && isAllLevelsPassed;

        if (isOriginalKingEnd || isSelectedKingEnd) 
        {
            GameModel.inst.GameStatus = Config.GAME_STATUS_FINISHED;
            // EventManager.TriggerEvent(Config.ON_GAME_END);
        }
        else
        {
            GameModel.inst.level = GameModel.inst.LevelsData[currentLevelIndex];
            GameModel.inst.level.History = new ArrayList();
            CommandManager.inst.addCommand(typeof(ShuffleDeckCommand));
            CommandManager.inst.addCommand(typeof(DistributeCardsCommand));

            GameModel.inst.FirstPlayer++;
            if (GameModel.inst.FirstPlayer >= 4)
            {
                GameModel.inst.FirstPlayer = 0;
            }

            if (!GameModel.inst.IsOriginalKing)
            {
                GameModel.inst.GameStatus = Config.GAME_STATUS_SELECT_LEVEL;
            }

            GameModel.inst.currentPlayer = GameModel.inst.Players[GameModel.inst.FirstPlayer];            

            for (int i = 0; i < GameModel.inst.Players.Length; i++)
            {
                GameModel.inst.Players[i].LevelScore = 0;
            }
        }        
        EventManager.TriggerEvent(Config.ON_SCORE_CHANGED);
    }

    public void addListeners()
    {
        EventManager.StartListening(Config.ON_TURN_ANIMATION_FINISHED, onTurnFinished);
        EventManager.StartListening(Config.ON_GRAB_ANIMATION_FINISHED, onGrabFinished);
        EventManager.StartListening(Config.ON_START_ANIMATION_FINISHED, prepareToStartGame);
    }

    public void startGame()
    {
        GameModel.inst.cardsOnDeck = new ArrayList();
        Debug.Log("start game" + GameModel.inst.level.Id);
        onGrabFinished();
    }

    public void prepareToStartGame()
    {
        if (GameModel.inst.IsOriginalKing)
        {
            startGame();
        }
        else
        {
            GameModel.inst.GameStatus = Config.GAME_STATUS_SELECT_LEVEL;
            EventManager.TriggerEvent(EventManager.SHOW_SELECT_WINDOW);
            if (GameModel.inst.FirstPlayer != Config.PLAYER_ME)
            {
                CommandManager.inst.addCommand(typeof(ChooseLevelCommand));
            }
        }
    }

    void onGrabFinished()
    {
        Debug.Log("onGrabFinished " + GameModel.inst.cardsOnDeck.Count + " player id " + GameModel.inst.currentPlayer.Id);
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
        Debug.Log("turn finished by player " + GameModel.inst.currentPlayer.Id);
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
            Debug.Log("next player " + currentPlayer);
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
            Debug.Log("num card " + i + " card " + goalType);
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
            grabber.LevelScore += GameModel.inst.level.GoalCost;
            grabber.Score += GameModel.inst.level.GoalCost;
            GameModel.inst.level.Goals = GameModel.inst.level.Goals.Where((val, idx) => idx != numIndex).ToArray();
        }

        for (int j = 0; j < goals.Length; j++)
        {
            numIndex = Array.IndexOf(GameModel.inst.level.Goals, goals[j]);
            if (numIndex > -1)
            {
                grabber.Goals.Add(goals[j]);
                grabber.LevelScore += GameModel.inst.level.GoalCost;
                grabber.Score += GameModel.inst.level.GoalCost;
                GameModel.inst.level.Goals = GameModel.inst.level.Goals.Where((val, idx) => idx != numIndex).ToArray();
            }
        }

        // check last tricks
        numIndex = Array.IndexOf(GameModel.inst.level.Goals, RuleModel.TRICK_7);
        if (grabber.Deck.Count == 2 && numIndex > -1)
        {
            grabber.Goals.Add(RuleModel.TRICK_7);
            grabber.LevelScore += GameModel.inst.level.GoalCost;
            grabber.Score += GameModel.inst.level.GoalCost;
            GameModel.inst.level.Goals = GameModel.inst.level.Goals.Where((val, idx) => idx != numIndex).ToArray();
        }
        numIndex = Array.IndexOf(GameModel.inst.level.Goals, RuleModel.TRICK_8);
        if (grabber.Deck.Count == 1 && numIndex > -1)
        {
            grabber.Goals.Add(RuleModel.TRICK_8);
            grabber.LevelScore += GameModel.inst.level.GoalCost;
            grabber.Score += GameModel.inst.level.GoalCost;
            GameModel.inst.level.Goals = GameModel.inst.level.Goals.Where((val, idx) => idx != numIndex).ToArray();
        }

        // all rules together
        numIndex = Array.IndexOf(GameModel.inst.level.Goals, RuleModel.ANY);
        if (numIndex > -1)
        {
            int scoreForAdd;
            grabber.Goals.Add(goals);
            GameModel.inst.level.Goals = GameModel.inst.level.Goals.Where((val, idx) => idx != numIndex).ToArray();

            // обычная взятка
            scoreForAdd = GameModel.inst.LevelsData[0].GoalCost;
            if (!GameModel.inst.level.isNegative)
            {
                scoreForAdd = -scoreForAdd;
            }
            grabber.LevelScore += scoreForAdd;
            grabber.Score += scoreForAdd;

            // check last tricks
            if (grabber.Deck.Count <= 2)
            {
                scoreForAdd = GameModel.inst.LevelsData[4].GoalCost;
                if (!GameModel.inst.level.isNegative)
                {
                    scoreForAdd = -scoreForAdd;
                }
                grabber.LevelScore += scoreForAdd;
                grabber.Score += scoreForAdd;
            }

            for (int a = 0; a < goals.Length; a++)
            {
                for (int b = 1; b < 7; b++)
                {
                    numIndex = Array.IndexOf(GameModel.inst.LevelsDataForCount[b].Goals, goals[a]);
                    if (numIndex > -1)
                    {
                        scoreForAdd = GameModel.inst.LevelsDataForCount[b].GoalCost;
                        if (!GameModel.inst.level.isNegative)
                        {
                            scoreForAdd = -scoreForAdd;
                        }
                        grabber.LevelScore += scoreForAdd;
                        grabber.Score += scoreForAdd;
                    }
                }
            }
        }

        GameModel.inst.currentPlayer = grabber;        
        Debug.Log("max card " + maxCard.Id + " grabber " + grabber.Id + " left cards " + grabber.Deck.Count);
        AnimationHelper.grabCards(grabber);
    }


}

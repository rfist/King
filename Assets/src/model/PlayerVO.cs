using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerVO
{
    public int Id = 0;
    public string Name = "";
    public int Score = 0;
    public int LevelScore = 0;
    public ArrayList Goals = new ArrayList();
    public string ImageName = "";
    //public CardVO[] Deck;
    public ArrayList Deck = new ArrayList();
    public string Container = "";    public List<LevelVO> LevelsData = new List<LevelVO>();


    public PlayerVO(int Id, string Name, string ImageName, string Container)
    {
        this.Id = Id;
        this.Name = Name;
        this.ImageName = ImageName;
        this.Container = Container;
        LevelsData = new List<LevelVO>();
    }

    public bool hasSuit(int Suit)
    {
        for (int i = 0; i < Deck.Count; i++)
        {
            if ((Deck[i] as CardVO).Suit == Suit)
            {
                return true;
            }
        }
        return false;
    }

}

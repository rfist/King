using UnityEngine;
using System.Collections;

public class CardVO
{
    public int Rank = 0;
    public int Suit = 0;
    public PlayerVO Owner;
    public string ImageName = "";
    public string Id;
    public GameObject gameObject;


    public CardVO(int Rank, int Suit, string ImageName)
    {
        this.Rank = Rank;
        this.Suit = Suit;
        this.ImageName = ImageName;
        Id = Rank.ToString() + '_' + Suit.ToString();
    }
}

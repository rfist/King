using UnityEngine;
using System.Collections;

public class PlayerVO
{
    public int Id = 0;
    public string Name = "";
    public int Score = 0;
    public ArrayList Goals = new ArrayList();
    public string ImageName = "";
    //public CardVO[] Deck;
    public ArrayList Deck = new ArrayList();
    public string Container = "";

    public PlayerVO(int Id, string Name, string ImageName, string Container)
    {
        this.Id = Id;
        this.Name = Name;
        this.ImageName = ImageName;
        this.Container = Container;
    }

}

﻿using UnityEngine;
using System.Collections;

public class LevelVO
{
    public int Id = 0;
    public string RuleImage = "";
    public bool isNegative = false;
    public string[] Goals;
    public string[] Rules;
    public GameStrategy Strategy;
    public int FirstPlayer = 0;


    public LevelVO(int Id, bool isNegative, GameStrategy Strategy, string RuleImage, int FirstPLayer)
    {
        this.Id = Id;
        this.isNegative = isNegative;
        this.RuleImage = RuleImage;
        this.Strategy = Strategy;
        this.FirstPlayer = FirstPlayer;
    }




}

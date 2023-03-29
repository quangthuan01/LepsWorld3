using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreModel
{
    public int score{get;set;}
    public string username{get;set;}

    public ScoreModel(int score, string username)
    {
        this.score = score;
        this.username = username;
    }
}

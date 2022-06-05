using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Realms;

public class PlayerStats : RealmObject
{
    [PrimaryKey]
    public string Username { get; set; }

    public RealmInteger<int> Score { get; set; }

    // public RealmInteger<int> HighestScore1 { get; set; }
    // public RealmInteger<int> HighestScore2 { get; set; }
    // public RealmInteger<int> HighestScore3 { get; set; }
    // public RealmInteger<int> HighestScore4 { get; set; }
    // public RealmInteger<int> HighestScore5 { get; set; }

    public PlayerStats() {} //Create an empty PlayerStats Set
    
    public PlayerStats(string username, int score) {
        this.Username = username;
        this.Score = score;
    }

    // public PlayerStats(string username, int HighestScore1, int HighestScore2, int HighestScore3, int HighestScore4, int HighestScore5) {
    //     this.Username = username;
    //     this.HighestScore1 = HighestScore1;
    //     this.HighestScore2 = HighestScore2;
    //     this.HighestScore3 = HighestScore3;
    //     this.HighestScore4 = HighestScore4;
    //     this.HighestScore5 = HighestScore5;
    // }
}

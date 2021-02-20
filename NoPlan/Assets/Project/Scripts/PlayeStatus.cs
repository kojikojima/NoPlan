using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeStatus
{
    public int hp;
    public int maxhp;
    public int mp;
    public int maxmp;
    public int atk;

    private static PlayeStatus playeStatus = new PlayeStatus();

    public static PlayeStatus GetInstance()
    {
        return playeStatus;
    }

    private PlayeStatus()
    {
        hp = 80;
        maxhp = 100;
        mp = 80;
        maxmp = 100;
        atk = 5;
    }
}

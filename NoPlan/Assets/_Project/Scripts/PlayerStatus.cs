using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus
{
    public int hp;
    public int maxhp;
    public int mp;
    public int maxmp;
    public int atk;
    IAttackable hoge;

    private static PlayerStatus playeStatus = new PlayerStatus();

    public static PlayerStatus GetInstance()
    {
        return playeStatus;
    }

    private PlayerStatus()
    {
        hp = 80;
        maxhp = 100;
        mp = 80;
        maxmp = 100;
        atk = 5;

        int damage = hoge.AttackValue;
    }
}

public interface IAttackable
{
    int AttackValue { get;}
}

public class PlayerDamage : IAttackable
{
    PlayerStatus status;
    int weaponAttack;
    int hitpoint;

    public int HitPoint { get { return hitpoint; } }
    public int AttackValue { get { return status.atk + weaponAttack; } }
}

public class EnemyDamage : IAttackable
{
    int level;
    int attack;

    public int AttackValue { get { return attack * level; } }
}
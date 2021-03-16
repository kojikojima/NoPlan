using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーユニットの管理クラス
/// </summary>
public class PlayerUnit : UnitBase
{
    /*-------------------------------------------------------------------*/
    /* プロパティ                                                        */
    /*-------------------------------------------------------------------*/
    public override int Power => 10;
    public override int Defense => 10;
    public override int HitValue => 10;
}

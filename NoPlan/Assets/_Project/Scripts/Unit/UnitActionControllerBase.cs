using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ユニット行動管理クラス
/// </summary>
public abstract class UnitActionControllerBase : MonoBehaviour
{
    /*-------------------------------------------------------------------*/
    /* 変数                                                              */
    /*-------------------------------------------------------------------*/
    protected UnitBase _ownerUnit = null;

    /*-------------------------------------------------------------------*/
    /* 外部メソッド                                                      */
    /*-------------------------------------------------------------------*/
    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="owner"></param>
    public abstract void SetUp(UnitBase owner);

    /// <summary>
    /// 破棄処理
    /// </summary>
    public abstract void Dispose();
}

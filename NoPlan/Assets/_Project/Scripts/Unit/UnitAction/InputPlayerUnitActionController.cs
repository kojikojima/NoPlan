using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

/// <summary>
/// 入力操作のプレイヤー行動管理クラス
/// </summary>
public class InputPlayerUnitActionController : UnitActionControllerBase
{
    /*-------------------------------------------------------------------*/
    /* 外部メソッド                                                      */
    /*-------------------------------------------------------------------*/
    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="owner"></param>
    public override void SetUp(UnitBase owner)
    {
        InputAsObservable.GetButton("Fire01")
            .Subscribe(_ =>
            {

            });
    }

    public override void Dispose()
    {

    }
}

using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class NormalShotUnitAction : UnitActionBase
{
    [SerializeField]
    private float _actionSpeed = 1f;

    [SerializeField]
    private GameObject _shotPrefab;

    public async override UniTask<ActionInfo> Execute()
    {
        var info = new ActionInfo();

        await UniTask.Delay(500);

        info.IsCompleted = true;

        return info;
    }

    public async override UniTask<ActionInfo> Cancel()
    {
        var info = new ActionInfo() { IsCompleted = false };

        await UniTask.Delay(500);

        info.IsCompleted = true;

        return info;
    }
}

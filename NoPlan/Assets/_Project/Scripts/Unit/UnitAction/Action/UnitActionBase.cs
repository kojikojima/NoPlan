﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public abstract class UnitActionBase : ScriptableObject
{
    public abstract UniTask<ActionInfo> Execute();

    public abstract UniTask<ActionInfo> Cancel();
}

public struct ActionInfo
{
    public bool IsCompleted; 
}
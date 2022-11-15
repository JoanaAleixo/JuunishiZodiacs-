using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public enum TARGETING
{
    self,
    singleAlly,
    singleEnemy,
    multipleAlly,
    multipleEnemy
}

[System.Serializable]
public abstract class Modifiers
{
    [SerializeField] int yes;
    [SerializeField] TARGETING _targetType;

    public TARGETING TargetType { get => _targetType; set => _targetType = value; }

    public Modifiers()
    {
    }

    public abstract void Draw();
}

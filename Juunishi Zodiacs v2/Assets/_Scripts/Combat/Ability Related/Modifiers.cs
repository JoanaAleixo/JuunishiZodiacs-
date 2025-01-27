using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] TARGETING _targetType;

    public TARGETING TargetType { get => _targetType; set => _targetType = value; }

    public Modifiers()
    {
    }

    public virtual void ExecuteMod(GameObject[] target)
    {

    }

    public abstract void Draw();
}

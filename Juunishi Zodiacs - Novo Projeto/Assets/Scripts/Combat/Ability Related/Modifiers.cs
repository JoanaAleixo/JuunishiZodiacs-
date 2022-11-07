using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum TARGETING
{
    single,
    multiple
}

[System.Serializable]
public abstract class Modifiers
{
    public Modifiers()
    {
    }

    public abstract void Draw();
    

}

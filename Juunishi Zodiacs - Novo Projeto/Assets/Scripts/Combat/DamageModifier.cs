using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

enum DAMAGETYPE
{
    Fisical,
    Magical
}

public class DamageModifier : Modifiers
{
    [SerializeField] int Quantity;
    [SerializeField] DAMAGETYPE damageType;
    [SerializeField] string yes;
    public override void Draw()
    {
        yes = (string)EditorGUILayout.TextField("Damage: ", yes);
    }
}


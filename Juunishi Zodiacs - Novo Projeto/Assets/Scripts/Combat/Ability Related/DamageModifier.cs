using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

enum DAMAGETYPE
{
    Physical,
    Fire,
    Water,
    Rock,
    Nature,
    Metal
}

public class DamageModifier : Modifiers
{
    [SerializeField] int _quantity;
    [SerializeField] DAMAGETYPE _damageType;
    [SerializeField] TARGETING _targetType;
    public override void Draw()
    {
        _quantity = (int)EditorGUILayout.IntField("Damage: ", _quantity);
        _damageType = (DAMAGETYPE)EditorGUILayout.EnumPopup("Damage Type: ",_damageType);
        _targetType = (TARGETING)EditorGUILayout.EnumPopup("Targeting Type: ",_targetType);
    }
}


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
[System.Serializable]
public class DamageModifier : Modifiers
{
    [SerializeField] int _quantity;
    [SerializeField] DAMAGETYPE _damageType;
    
    GameObject target;

    public int Quantity { get => _quantity; set => _quantity = value; }
    
    internal DAMAGETYPE DamageType { get => _damageType; set => _damageType = value; }

    public override void Draw()
    {
        Quantity = (int)EditorGUILayout.IntField("Damage: ", Quantity);
        DamageType = (DAMAGETYPE)EditorGUILayout.EnumPopup("Damage Type: ",DamageType);
        TargetType = (TARGETING)EditorGUILayout.EnumPopup("Targeting Type: ",TargetType);
    }

    public void ExecuteDamage(GameObject target)
    {
        if(TargetType == TARGETING.singleEnemy)
        {
           // Damage(target);
        }
        else if(TargetType == TARGETING.multipleEnemy)
        {
            //Damage(all);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum DAMAGETYPE
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

    public int Quantity { get => _quantity; set => _quantity = value; }
    
    internal DAMAGETYPE DamageType { get => _damageType; set => _damageType = value; }

    public override void Draw()
    {
        Quantity = (int)EditorGUILayout.IntField("Damage: ", Quantity);
        DamageType = (DAMAGETYPE)EditorGUILayout.EnumPopup("Damage Type: ",DamageType);
        TargetType = (TARGETING)EditorGUILayout.EnumPopup("Targeting Type: ",TargetType);
    }

    public override void ExecuteMod(GameObject[] target)
    {
        if (TargetType == TARGETING.singleEnemy || TargetType == TARGETING.singleAlly || TargetType == TARGETING.self)
        {
            target[0].GetComponent<BaseStats>().TakeDamage(Quantity, DamageType);
        }
        else if (TargetType == TARGETING.multipleEnemy || TargetType == TARGETING.multipleAlly)
        {
            for (int i = 0; i < target.Length; i++)
            {
                target[i].GetComponent<BaseStats>().TakeDamage(Quantity, DamageType);
            }
        }
    }

    public void ExecuteDamageMod(GameObject[] target)
    {
        
    }
}


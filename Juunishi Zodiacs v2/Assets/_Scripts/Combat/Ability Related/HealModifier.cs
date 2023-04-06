using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HealModifier : Modifiers
{
    [SerializeField] int _quantity;

    public int Quantity { get => _quantity; set => _quantity = value; }

    public override void Draw()
    {
        Quantity = (int)EditorGUILayout.IntField("Healing Amount: ", Quantity);
        TargetType = (TARGETING)EditorGUILayout.EnumPopup("Targeting Type: ", TargetType);
    }

    public override void ExecuteMod(GameObject[] target)
    {
        if (TargetType == TARGETING.singleEnemy || TargetType == TARGETING.singleAlly || TargetType == TARGETING.self)
        {
            target[0].GetComponent<BaseStats>().HealCaracter(Quantity);
        }
        else if (TargetType == TARGETING.multipleEnemy || TargetType == TARGETING.multipleAlly)
        {
            for (int i = 0; i < target.Length; i++)
            {
                target[i].GetComponent<BaseStats>().HealCaracter(Quantity);
            }
        }
    }
}                                  
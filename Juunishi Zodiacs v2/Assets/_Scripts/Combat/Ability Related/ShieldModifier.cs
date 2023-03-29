using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShieldModifier : Modifiers
{
    public override void Draw()
    {
        TargetType = (TARGETING)EditorGUILayout.EnumPopup("Targeting Type: ", TargetType);
    }

    public override void ExecuteMod(GameObject[] target)
    {
        if (TargetType == TARGETING.singleEnemy || TargetType == TARGETING.singleAlly || TargetType == TARGETING.self)
        {
            target[0].GetComponent<BaseStats>().IsShielded = true;
        }
        else if (TargetType == TARGETING.multipleEnemy || TargetType == TARGETING.multipleAlly)
        {
            for (int i = 0; i < target.Length; i++)
            {
                target[i].GetComponent<BaseStats>().IsShielded = true;
            }
        }
    }
}

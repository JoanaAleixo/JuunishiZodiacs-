using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

enum STATUSTYPES
{
    Bound
}

public class StatusModifier : Modifiers
{
    [SerializeField] int Quantity;
    [SerializeField] bool isBuff;
    [SerializeField] STATUSTYPES status;

    public bool IsBuff { get => isBuff; set => isBuff = value; }

    public override void Draw()
    {
        status = (STATUSTYPES)EditorGUILayout.EnumPopup("Status: ", status);
        TargetType = (TARGETING)EditorGUILayout.EnumPopup("Targeting Type: ", TargetType);
    }

    public override void ExecuteMod(GameObject[] target)
    {
        StatusFx effect;
        switch (status)
        {
            case STATUSTYPES.Bound:
                effect = new BoundFx(true, false, false, 5);
                Quantity = 1;
                break;
            default:
                effect = new BoundFx(true, false, false, 5);
                Quantity = 0;
                break;
        }

        if (TargetType == TARGETING.singleEnemy || TargetType == TARGETING.singleAlly || TargetType == TARGETING.self)
        {
            Debug.Log("Status Mod Executed!");
            target[0].GetComponent<BaseStats>().currentStatus.Add(effect, Quantity);
        }
        else if (TargetType == TARGETING.multipleEnemy || TargetType == TARGETING.multipleAlly)
        {
            for (int i = 0; i < target.Length; i++)
            {
                target[i].GetComponent<BaseStats>().currentStatus.Add(effect, Quantity);
            }
        }
    }
}

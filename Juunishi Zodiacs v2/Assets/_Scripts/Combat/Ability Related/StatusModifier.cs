using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

enum STATUSTYPES
{
    Bound,
    Paralize,
    Drowsy,
    Shield,
    CureDebuff
}

public class StatusModifier : Modifiers
{
    [SerializeField] int Quantity;
    [SerializeField] bool isBuff;
    [SerializeField] STATUSTYPES status;
    [SerializeField] int maxQuant;

    public bool IsBuff { get => isBuff; set => isBuff = value; }


    public override void Draw()
    {
#if UNITY_EDITOR
        status = (STATUSTYPES)EditorGUILayout.EnumPopup("Status: ", status);
        TargetType = (TARGETING)EditorGUILayout.EnumPopup("Targeting Type: ", TargetType);
#endif
    }


    public override void ExecuteMod(GameObject[] target)
    {
        StatusFx effect = new BoundFx(true, false, false, 5);
        switch (status)
        {
            case STATUSTYPES.Bound:
                effect = new BoundFx(true, false, false, 5);
                Quantity = 1;
                maxQuant = 100;
                break;
            case STATUSTYPES.Paralize:
                effect = new ParalizeFx(false,true,false);
                Quantity = 1;
                maxQuant = 100;
                break;
            case STATUSTYPES.Drowsy:
                effect = new DrowsyFx(false, true, false);
                Quantity = 3;
                maxQuant = 5;
                break;
            case STATUSTYPES.Shield:
                effect = new ShieldFx(false, false, true); 
                Quantity = 1;
                maxQuant = 1;
                break;
            case STATUSTYPES.CureDebuff:
                effect = new CureDebuffFx(true, false, true);
                Quantity = 1;
                maxQuant = 1;
                break;
            default:
                effect = new BoundFx(true, false, false, 5);
                Quantity = 0;
                maxQuant = 100;
                break;
        }

        if (TargetType == TARGETING.singleEnemy || TargetType == TARGETING.singleAlly || TargetType == TARGETING.self)
        {
            bool foundEffect = false;
            foreach (var curstatus in target[0].GetComponent<BaseStats>().currentStatus.ToList())
            {
                if(curstatus.Key.GetType() == effect.GetType() )
                {
                    foundEffect = true;
                    target[0].GetComponent<BaseStats>().currentStatus[curstatus.Key] += Quantity;
                    if(target[0].GetComponent<BaseStats>().currentStatus[curstatus.Key] > maxQuant)
                    {
                        target[0].GetComponent<BaseStats>().currentStatus[curstatus.Key] -= target[0].GetComponent<BaseStats>().currentStatus[curstatus.Key] - maxQuant;
                    }
                }
            }
            if (foundEffect == false)
            {
                target[0].GetComponent<BaseStats>().currentStatus.Add(effect, Quantity);
            }

        }
        else if (TargetType == TARGETING.multipleEnemy || TargetType == TARGETING.multipleAlly)
        {
            for (int i = 0; i < target.Length; i++)
            {
                bool foundEffect = false;
                foreach (var curstatus in target[i].GetComponent<BaseStats>().currentStatus.ToList())
                {
                    if (curstatus.Key.GetType() == effect.GetType())
                    {
                        foundEffect = true;
                        target[i].GetComponent<BaseStats>().currentStatus[curstatus.Key] += Quantity;
                        if (target[i].GetComponent<BaseStats>().currentStatus[curstatus.Key] > maxQuant)
                        {
                            target[i].GetComponent<BaseStats>().currentStatus[curstatus.Key] -= target[i].GetComponent<BaseStats>().currentStatus[curstatus.Key] - maxQuant;
                        }
                    }
                }
                if (foundEffect == false)
                {
                    target[i].GetComponent<BaseStats>().currentStatus.Add(effect, Quantity);
                }
            }
        }
    }
}

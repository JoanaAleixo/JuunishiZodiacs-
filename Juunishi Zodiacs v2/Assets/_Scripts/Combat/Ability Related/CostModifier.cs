using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

public enum COSTTYPE
{
    Hp,
    Sp
}

[System.Serializable]
public class CostModifier : Modifiers
{
    [SerializeField] int _quantity;
    [SerializeField] COSTTYPE _costType;

    public int Quantity { get => _quantity; set => _quantity = value; }
    public COSTTYPE CostType { get => _costType; set => _costType = value; }

    public override void Draw()
    {
#if UNITY_EDITOR
        Quantity = (int)EditorGUILayout.IntField("Damage: ", Quantity);
        CostType = (COSTTYPE)EditorGUILayout.EnumPopup("Damage Type: ", CostType);
        TargetType = TARGETING.self;
#endif
    }

    public override void ExecuteMod(GameObject[] target)
    {
        if (TargetType == TARGETING.self)
        {
            if (target[0].GetComponent<BaseStats>() is PlayableCaracter)
            {
                PlayableCaracter caracter = (PlayableCaracter)target[0].GetComponent<BaseStats>();
                if (CostType == COSTTYPE.Hp)
                {
                    caracter.TakeDamage(Quantity, DAMAGETYPE.None);
                }
                else if(CostType == COSTTYPE.Sp)
                {
                    int total = -Quantity;
                    caracter.UpdateSp(total);
                }
            }
            else
            {
                Enemy enemy = (Enemy)target[0].GetComponent<BaseStats>();
                if (CostType == COSTTYPE.Hp)
                {
                    enemy.TakeDamage(Quantity, DAMAGETYPE.None);
                }
                else if (CostType == COSTTYPE.Sp)
                {
                    
                }
            }
        }
    }
}

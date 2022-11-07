using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum ELEMENT
{
    Water, 
    Nature, 
    Fire, 
    Rock, 
    Metal, 
    NoElement
}

public class BaseStats : MonoBehaviour
{
    CombatManager combatMg;

    [SerializeField] string _name;
    //[SerializeField] TextMeshProUGUI nomeDisplay;
    [SerializeField] ELEMENT _type; 
    [SerializeField] int _hpMax;
    [SerializeField] int _curHp;
    [SerializeField] int _shieldHp;
    [SerializeField] Ability _physicalAbility;
    [SerializeField] Ability[] _abilities = new Ability[3];

    public Ability[] Abilities { get => _abilities; set => _abilities = value; }
    public Ability PhysicalAbility { get => _physicalAbility; set => _physicalAbility = value; }

    //[SerializeField] Image barraHP;
    //[SerializeField] TextMeshProUGUI vidaText;

    //[SerializeField] AtaqueFisico ataqFisico;


    //[SerializeField] Image barraSP;
    //[SerializeField] TextMeshProUGUI spText;
    //[SerializeField] AtaqueMagico[] ataqMagicos;
    //[SerializeField] Sprite iconElementoSprite;
    //[SerializeField] Image iconElementoImage;

    void Start()
    {
        combatMg = CombatManager.combatInstance;
    }

    void Update()
    {
        
    }
}

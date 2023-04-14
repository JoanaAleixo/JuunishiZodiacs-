using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaracterCreation : ScriptableObject
{
    [SerializeField] string _name;
    [SerializeField] ELEMENT _type;
    [SerializeField] FloatVariable _hpMax;
    [SerializeField] Ability _physicalAbility;
    [SerializeField] Ability[] _abilities = new Ability[3];
    [SerializeField] GameEvent takeDamageEv;

    public Ability[] Abilities { get => _abilities; set => _abilities = value; }
    public Ability PhysicalAbility { get => _physicalAbility; set => _physicalAbility = value; }
    public ELEMENT Type { get => _type; set => _type = value; }
    public string Name { get => _name; set => _name = value; }
    public FloatVariable HpMax { get => _hpMax; set => _hpMax = value; }
    public GameEvent TakeDamageEv { get => takeDamageEv;}
}

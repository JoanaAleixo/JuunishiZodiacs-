using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Abilty")]
public class Ability : ScriptableObject
{
    [SerializeReference] List<Modifiers> _mods = new List<Modifiers>();

    public List<Modifiers> Mods { get => _mods; set => _mods = value; }
}

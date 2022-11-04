using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Abilty")]
public class Ability : ScriptableObject
{
    [SerializeReference] List<Modifiers> mods = new List<Modifiers>();

    public List<Modifiers> Mods { get => mods; set => mods = value; }
}

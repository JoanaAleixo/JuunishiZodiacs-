using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObjects/Abilty")]
public class Ability : ScriptableObject
{
    [SerializeReference] List<Modifiers> _mods = new List<Modifiers>();
    [SerializeReference] string abilityName;
    [SerializeReference, TextArea] string description;
    public List<Modifiers> Mods { get => _mods; set => _mods = value; }
    public string Description { get => description; set => description = value; }
    public string AbilityName { get => abilityName; set => abilityName = value; }
}

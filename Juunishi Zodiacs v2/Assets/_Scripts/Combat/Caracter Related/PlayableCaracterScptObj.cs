using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/PlayableCaracter")]
public class PlayableCaracterScptObj : CaracterCreation
{
    [SerializeField] FloatVariable _spMax;
    [SerializeField] Ability[] _allCaracterAbilities;

    public FloatVariable SpMax { get => _spMax; set => _spMax = value; }
    public Ability[] AllCaracterAbilities { get => _allCaracterAbilities; set => _allCaracterAbilities = value; }
}

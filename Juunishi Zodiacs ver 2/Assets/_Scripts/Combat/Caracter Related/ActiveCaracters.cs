using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Active Caracters")]
public class ActiveCaracters : ScriptableObject
{
    [SerializeField] PlayableCaracterScptObj[] _activeCaracters = new PlayableCaracterScptObj[3];

    public PlayableCaracterScptObj[] ActiveCaractersInGame { get => _activeCaracters; set => _activeCaracters = value; }
}

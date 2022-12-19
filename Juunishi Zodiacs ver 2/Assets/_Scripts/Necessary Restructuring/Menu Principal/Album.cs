using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Album")]
public class Album : ScriptableObject
{
    [SerializeField] List<Sprite> imagens;

    public List<Sprite> Imagens { get => imagens; set => imagens = value; }
}

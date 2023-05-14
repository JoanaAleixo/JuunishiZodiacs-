using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create New/Item/Photo")]
public class PhotoItem : BaseItem
{
    [SerializeField] ScriptableDialogue _itemDialogue;

    public ScriptableDialogue ItemDialogue { get => _itemDialogue; set => _itemDialogue = value; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create New/Item/Item")]
public class KeyItem : BaseItem
{
    [SerializeField] Sprite _iconForBag;
    [SerializeField] string _itemDescription;
    [SerializeField] ScriptableDialogue _itemDialogue;
    public Sprite IconForBag { get => _iconForBag; set => _iconForBag = value; }

    public string ItemDescription { get => _itemDescription; set => _itemDescription = value; }
    public ScriptableDialogue ItemDialogue { get => _itemDialogue; set => _itemDialogue = value; }
}

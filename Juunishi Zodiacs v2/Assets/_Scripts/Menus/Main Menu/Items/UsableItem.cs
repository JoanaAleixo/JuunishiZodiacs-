using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create New/Item/UsableItem")]

public class UsableItem : BaseItem
{
    [SerializeField] ScriptableDialogue _itemDialogue;
    [SerializeField] Sprite _iconForBag;
    [SerializeField] string _itemDescription;
    public ScriptableDialogue ItemDialogue { get => _itemDialogue; set => _itemDialogue = value; }

    public Sprite IconForBag { get => _iconForBag; set => _iconForBag = value; }

    public string ItemDescription { get => _itemDescription; set => _itemDescription = value; }
}

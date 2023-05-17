using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create New/Item/UsableItem")]

public class UsableItem : BaseItem
{
   
    [SerializeField] Sprite _iconForBag;
    [SerializeField] string _itemDescription;
  

    public Sprite IconForBag { get => _iconForBag; set => _iconForBag = value; }

    public string ItemDescription { get => _itemDescription; set => _itemDescription = value; }
}

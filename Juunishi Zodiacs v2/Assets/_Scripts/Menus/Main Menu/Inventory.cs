using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InventoryInfoStorage")]

public class Inventory : ScriptableObject
{
    Dictionary<BaseItem, int> _inventoryDic = new Dictionary<BaseItem, int>();

    public Dictionary<BaseItem, int> InventoryDic { get => _inventoryDic; set => _inventoryDic = value; }

}

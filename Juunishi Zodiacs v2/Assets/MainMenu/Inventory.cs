using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InventoryInfoStorage")]

public class Inventory : ScriptableObject
{
    Dictionary<ScriptableItem, int> _inventoryDic = new Dictionary<ScriptableItem, int>();

    public Dictionary<ScriptableItem, int> InventoryDic { get => _inventoryDic; set => _inventoryDic = value; }

}

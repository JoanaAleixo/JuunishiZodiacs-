using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemCollect : MonoBehaviour
{
    [SerializeField] int itemId;
    [SerializeField] BaseItem _thisItem;
    [SerializeField] int _itemAmount = 1;

    public int ItemId { get => itemId; set => itemId = value; }
    public BaseItem ThisItem { get => _thisItem; set => _thisItem = value; }

    public void CollectItem()
    {

            NavigationManager.instance.ItemDic.Remove(itemId);

        if (_thisItem is KeyItem || _thisItem is UsableItem)
        {
            AddInventory();
        }
        else if( _thisItem is PhotoItem)
        {

        }

        gameObject.SetActive(false);
    }

    void AddInventory()
    {
        if (MenuManager.instance.InventoryInfo.InventoryDic.ContainsKey(ThisItem))
        {

            _itemAmount++;
           
            MenuManager.instance.InventoryInfo.InventoryDic[ThisItem] = _itemAmount;


        }
        else
        {

            MenuManager.instance.InventoryInfo.InventoryDic.Add(ThisItem, _itemAmount);


        }

        foreach (var item in MenuManager.instance.InventoryInfo.InventoryDic)
        {
            Debug.Log(item.Key + " " + item.Value);
        }
    }

    //  Dictionary<string, int> dic = new Dictionary<string, int>();

    //  private void Awake()
    //  {
    //     dic.Add("a", 5);
    //      dic.TryGetValue("a", out int a);
    //     a++;
    //  }


}

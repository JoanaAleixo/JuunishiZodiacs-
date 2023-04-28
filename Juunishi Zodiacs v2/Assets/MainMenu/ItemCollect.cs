using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemCollect : MonoBehaviour
{
    [SerializeField] int itemId;
    [SerializeField] ScriptableItem _thisItem;
    [SerializeField] int _itemAmmount = 1;

    public int ItemId { get => itemId; set => itemId = value; }
    public ScriptableItem ThisItem { get => _thisItem; set => _thisItem = value; }

    public void CollectItem()
    {

      

            NavigationManager.instance.ItemDic.Remove(itemId);

       
            if (MenuManager.instance.Inventory.ContainsKey(ThisItem))
            {
          
            _itemAmmount++;
            //  MenuManager.instance.Inventory.TryGetValue(ThisItem, out _itemAmmount);
            MenuManager.instance.Inventory[ThisItem] = _itemAmmount;


        }
            else
            {
           
            MenuManager.instance.Inventory.Add(ThisItem, _itemAmmount);

          
        }

        foreach (var item in MenuManager.instance.Inventory)
        {
          Debug.Log(item.Key + " " + item.Value);
      }

        gameObject.SetActive(false);
    }


    //  Dictionary<string, int> dic = new Dictionary<string, int>();

    //  private void Awake()
    //  {
    //     dic.Add("a", 5);
    //      dic.TryGetValue("a", out int a);
    //     a++;
    //  }


}

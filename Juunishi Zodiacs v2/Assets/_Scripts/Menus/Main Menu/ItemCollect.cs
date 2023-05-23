using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
//using static UnityEditor.Progress;

public class ItemCollect : MonoBehaviour
{
    [SerializeField] int itemId;
    [SerializeField] BaseItem _thisItem;
    [SerializeField] int _itemAmount = 1;
  

    public int ItemId { get => itemId; set => itemId = value; }
    public BaseItem ThisItem { get => _thisItem; set => _thisItem = value; }

    public void CollectItem()
    {
        OpenDialogue();
        NavigationManager.instance.ItemDic.Remove(itemId);

        if (_thisItem is KeyItem || _thisItem is UsableItem || _thisItem is PhotoItem)
        {
            AddInventory();
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

    void OpenDialogue()
    {
        NavigationManager.instance.DialogueCanvas.SetActive(true);
            
        if (_thisItem is KeyItem)
        {
            KeyItem keyItem = (KeyItem)_thisItem;
            DialogueManager.instance.MyDialogTree[0] = keyItem.ItemDialogue;
        }
        else if (_thisItem is PhotoItem)
        {
            PhotoItem photoItem = (PhotoItem)_thisItem;
            DialogueManager.instance.MyDialogTree[0] = photoItem.ItemDialogue;
        }
       else if( _thisItem is UsableItem)
        {
            return;
        }

        
      
        //nesta parte seguinte, em função de um erro no texto ser excrito pos o dialogo ser encerrado, o dialogo que é escrito,
        //enquanto ainda esta em typing effect e executado duas vezes, para que termine esse dialogo e escreva o novo


        if (DialogUIManager.instance.typingeffectCoroutine == null)
        {
            DialogueManager.instance.UpdateOnUI();
        }
        else
        {
            DialogueManager.instance.UpdateOnUI();
            DialogueManager.instance.UpdateOnUI();

        }

     

    }

  
}

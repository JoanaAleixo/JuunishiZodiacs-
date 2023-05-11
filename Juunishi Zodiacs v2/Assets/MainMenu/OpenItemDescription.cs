using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpenItemDescription : MonoBehaviour
{
    BaseItem _thisItemOnButton;
  
    bool _isActive = false;

    public BaseItem ThisItemOnButton { get => _thisItemOnButton; set => _thisItemOnButton = value; }

    public void ItemDescription()
    {
       
            GameObject itemDescriptionObject = MenuManager.instance.ItemDescriptionBox;
            itemDescriptionObject.SetActive(true);
            TextMeshProUGUI descriptionText = itemDescriptionObject.GetComponentInChildren<TextMeshProUGUI>();

        if(_thisItemOnButton is KeyItem)
        {
            KeyItem keyItem = (KeyItem)_thisItemOnButton;
            descriptionText.text = keyItem.ItemDescription;
        }
        else if(_thisItemOnButton is UsableItem)
        {
            UsableItem usableItem = (UsableItem)_thisItemOnButton;
            descriptionText.text = usableItem.ItemDescription;
        }
         
         itemDescriptionObject.transform.position = new Vector3(itemDescriptionObject.transform.position.x, transform.position.y, itemDescriptionObject.transform.position.z);

       
    }
    public void CloseItemDescription()
    {
  
        MenuManager.instance.ItemDescriptionBox.SetActive(false);
    }
    
}

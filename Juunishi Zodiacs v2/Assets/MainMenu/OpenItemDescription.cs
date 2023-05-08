using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpenItemDescription : MonoBehaviour
{
    ScriptableItem _thisItemOnButton;
  
    bool _isActive = false;

    public ScriptableItem ThisItemOnButton { get => _thisItemOnButton; set => _thisItemOnButton = value; }

    public void ItemDescription()
    {
        if(_isActive == false)
        {
            GameObject itemDescriptionObject = MenuManager.instance.ItemDescriptionBox;
            itemDescriptionObject.SetActive(true);
            TextMeshProUGUI descriptionText = itemDescriptionObject.GetComponentInChildren<TextMeshProUGUI>();
            descriptionText.text = _thisItemOnButton.ItemDescription;
         itemDescriptionObject.transform.position = new Vector3(itemDescriptionObject.transform.position.x, transform.position.y, itemDescriptionObject.transform.position.z);
          
            
            _isActive = true;
        }
        else
        {
            _isActive = false;
            MenuManager.instance.ItemDescriptionBox.SetActive(false);
        }
       
    }
}

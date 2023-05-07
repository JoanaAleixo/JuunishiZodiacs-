using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class ScriptableItem : ScriptableObject
{
    [SerializeField] string _itemName;
    [SerializeField] Sprite _icon;
    [SerializeField] Vector3 _itemPositionInNav;
    [SerializeField] string _itemDescription;
    
  

    public Vector3 ItemPositionInNav { get => _itemPositionInNav; set => _itemPositionInNav = value; }
    public Sprite Icon { get => _icon; set => _icon = value; }
    public string ItemName { get => _itemName; set => _itemName = value; }
    public string ItemDescription { get => _itemDescription; set => _itemDescription = value; }
}

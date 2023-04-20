using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class ScriptableItem : ScriptableObject
{
    [SerializeField] string _itemName;
    [SerializeField] Sprite _icon;
    [SerializeField] Vector3 _itemPositionInNav;
    
  

    public Vector3 ItemPositionInNav { get => _itemPositionInNav; set => _itemPositionInNav = value; }
    public Sprite Icon { get => _icon; set => _icon = value; }

}

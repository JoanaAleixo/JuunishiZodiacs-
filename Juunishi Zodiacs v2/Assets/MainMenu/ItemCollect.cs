using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollect : MonoBehaviour
{
    [SerializeField] int itemId;

    public int ItemId { get => itemId; set => itemId = value; }
}

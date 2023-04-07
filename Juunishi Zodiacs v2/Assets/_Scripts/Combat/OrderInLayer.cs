using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderInLayer : MonoBehaviour
{
    [SerializeField] int orderInLayer;

    void Start()
    {
        Renderer rend = gameObject.GetComponent<Renderer>();
        rend.sortingOrder = orderInLayer;
    }
}

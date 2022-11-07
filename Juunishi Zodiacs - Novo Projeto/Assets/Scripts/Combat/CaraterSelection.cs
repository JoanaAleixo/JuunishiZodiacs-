using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CaraterSelection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject _selectionArrow;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _selectionArrow.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _selectionArrow.SetActive(false);
    }
}

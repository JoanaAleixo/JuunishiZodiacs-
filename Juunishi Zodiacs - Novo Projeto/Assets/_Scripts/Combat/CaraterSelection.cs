using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CaraterSelection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    CombatManager cM;
    [SerializeField] GameObject _selectionArrow;

    private void Start()
    {
        cM = CombatManager.combatInstance;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(cM.CurState == BATTLESTATE.PlayerTurn)
            _selectionArrow.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (cM.CurState == BATTLESTATE.PlayerTurn)
            _selectionArrow.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayableCaracter : BaseStats
{

    [SerializeField] int _spMax;
    [SerializeField] int _sp;

    protected override void Start()
    {
        base.Start();
    }


    void Update()
    {
        
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (uIManager.AllyTargetSelecting == true)
        {
            if (combatMg.CurState == BATTLESTATE.SelectingTarget && uIManager.TemporarySelectedTarget == this)
            {
                uIManager.LockTarget(this);
            }
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (uIManager.AllyTargetSelecting == true)
        {
            if (combatMg.CurState == BATTLESTATE.SelectingTarget && uIManager.TemporarySelectedTarget != this)
            {
                uIManager.ChangeTarget(this);
            }
        }
    }
}

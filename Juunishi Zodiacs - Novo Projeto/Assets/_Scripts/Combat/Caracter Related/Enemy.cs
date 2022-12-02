using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : BaseStats
{

    protected override void Start()
    {
        base.Start();

    }

    void Update()
    {
        
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerEnter.name);

        if (uIManager.EnemyTargetSelecting == true)
        {
            if (combatMg.CurState == BATTLESTATE.SelectingTarget && uIManager.TemporarySelectedTarget == this)
            {
                uIManager.LockTarget(this);
            }
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerEnter.name);

        Debug.Log("123");
        if(uIManager.EnemyTargetSelecting == true)
        {
            if (combatMg.CurState == BATTLESTATE.SelectingTarget && uIManager.TemporarySelectedTarget != this)
            {
                uIManager.ChangeTarget(this);
            }
        }
    }
}

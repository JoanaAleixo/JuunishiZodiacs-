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

    public void ChoseAbility()
    {
        Ability abi = MyCaracter.Abilities[Random.Range(0, MyCaracter.Abilities.Length)];
        combatMg.EnemyAbility(abi);
    }

    public int ChoseEnemyTarget()
    {
        return Random.Range(0, combatMg.Caracters.Count);
    }

    public int ChoseAllyTarget()
    {
        return Random.Range(0, combatMg.Enemies.Count);
    }

    protected override void CheckIfDead()
    {
        base.CheckIfDead();
        if(myCaracter.HpMax.value <= 0)
        {
            combatMg.Enemies.Remove(this);
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
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
        if(uIManager.EnemyTargetSelecting == true)
        {
            if (combatMg.CurState == BATTLESTATE.SelectingTarget && uIManager.TemporarySelectedTarget != this)
            {
                uIManager.ChangeTarget(this);
            }
        }
    }
}

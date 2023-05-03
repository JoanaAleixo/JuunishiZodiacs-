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
        bool rollDrowsy = false;
        bool passedRoll = true;
        int chance = 0;
        foreach (var status in currentStatus)
        {
            if (status.Key is DrowsyFx)
            {
                rollDrowsy = true;
                switch (status.Value)
                {
                    case 1:
                        chance = 90;
                        break;
                    case 2:
                        chance = 80;
                        break;
                    case 3:
                        chance = 0;
                        break;
                    case 4:
                        chance = 60;
                        break;
                    case 5:
                        chance = 50;
                        break;
                    default:
                        chance = 100;
                        break;
                }
            }
        }
        if (rollDrowsy)
        {
            int rand = UnityEngine.Random.Range(1, 101);
            if (rand <= chance)
            {
                Debug.Log("Passed chance of " + chance + "% with a " + rand);
                passedRoll = true;
            }
            else
            {
                Debug.Log("Did not pass chance of " + chance + "% with a " + rand);
                passedRoll = false;
            }
        }
        if (passedRoll)
        {
            Ability abi = MyCaracter.Abilities[Random.Range(0, MyCaracter.Abilities.Length)];
            Debug.Log(abi.name);
            combatMg.EnemyAbility(abi);
        }
        else
        {
            combatMg.EnemyAbility(combatMg.EmptyAbility);
        }
        
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

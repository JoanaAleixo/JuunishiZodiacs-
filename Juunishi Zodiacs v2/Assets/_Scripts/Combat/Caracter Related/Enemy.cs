using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Enemy : BaseStats
{
    [SerializeField] GameObject[] statusSprites;

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
                        chance = 70;
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
            if(deathSp == null)
            {
                GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = deathSp;
            }
        }
    }

    public void EnemyStatusFx()
    {
        int cont = 0;

        foreach (var status in currentStatus)
        {
            statusSprites[cont].SetActive(true);
            if (status.Key is BoundFx)
            {
                statusSprites[cont].GetComponent<Image>().sprite = uIManager.BoundFxSprite;
            }
            else if (status.Key is ParalizeFx)
            {
                statusSprites[cont].GetComponent<Image>().sprite = uIManager.ParalizeFxSprite;
            }
            else if (status.Key is DrowsyFx)
            {
                statusSprites[cont].GetComponent<Image>().sprite = uIManager.DrowsyFxSprite;
            }
            else if (status.Key is ShieldFx)
            {
                statusSprites[cont].GetComponent<Image>().sprite = uIManager.ShieldFxSprite;
            }
            else if (status.Key is CureDebuffFx)
            {
                statusSprites[cont].GetComponent<Image>().sprite = uIManager.CureDebuffFxSprite;
            }
            /*else if (status.Key is RageFx)
            {

            }
            else if (status.Key is OutroFx)
            {
                        
            }*/
            statusSprites[cont].GetComponentInChildren<TextMeshProUGUI>().text = status.Value.ToString();
            cont++;
        }
        for (int i = cont; i < 5; i++)
        {
            statusSprites[i].SetActive(false);
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if(myCaracter.HpMax.value > 0)
        {
            if (uIManager.EnemyTargetSelecting == true)
            {
                if (combatMg.CurState == BATTLESTATE.SelectingTarget && uIManager.TemporarySelectedTarget == this)
                {
                    uIManager.LockTarget(this);
                }
            }
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if(myCaracter.HpMax.value > 0)
        {
            if (uIManager.EnemyTargetSelecting == true)
            {
                if (combatMg.CurState == BATTLESTATE.SelectingTarget && uIManager.TemporarySelectedTarget != this)
                {
                    uIManager.ChangeTarget(this);
                }
            }
        }
    }
}

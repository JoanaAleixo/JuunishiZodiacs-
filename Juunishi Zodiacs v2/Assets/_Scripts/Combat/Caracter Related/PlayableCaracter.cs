using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayableCaracter : BaseStats
{

    /*[SerializeField] int _spMax;
    [SerializeField] int _sp;*/
    
    [SerializeField] ActiveCaracters _allCaracters;


    protected override void Start()
    {
        MyCaracter = _allCaracters.ActiveCaractersInGame[CaracterNumber];
        StartCoroutine(WaitASec());
        
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

    protected override void CheckIfDead()
    {
        base.CheckIfDead();
        if (myCaracter.HpMax.value <= 0)
        {
            combatMg.Caracters.Remove(this);
            combatMg.AdjustCaracterNumbers(CaracterNumber);
            if (deathSp != null)
            {
                GetComponent<SpriteRenderer>().sprite = deathSp;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = null;
            }
        }
    }

    public void UpdateSp(int value)
    {
        PlayableCaracterScptObj myCaracterScpt = (PlayableCaracterScptObj)myCaracter;
        myCaracterScpt.SpMax.value += value;
        takeDamageEV.Raise();
    }

    IEnumerator WaitASec()
    {
        yield return new WaitForSeconds(2);
        takeDamageEV.Raise();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;

public enum ELEMENT
{
    Water, 
    Nature, 
    Fire, 
    Rock, 
    Metal, 
    NoElement
}

public class BaseStats : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] protected CombatManager combatMg;
    [SerializeField] protected CombatUiManager uIManager;
    /*[SerializeField] protected string _name;
    //[SerializeField] TextMeshProUGUI nomeDisplay;
    [SerializeField] protected ELEMENT _type; 
    [SerializeField] protected int _hpMax;
    [SerializeField] protected int _curHp;
    [SerializeField] protected int _shieldHp;
    [SerializeField] protected Ability _physicalAbility;
    [SerializeField] protected Ability[] _abilities = new Ability[3];
    public Ability[] Abilities { get => _abilities; set => _abilities = value; }
    public Ability PhysicalAbility { get => _physicalAbility; set => _physicalAbility = value; }
    */
    //[SerializeField] Image barraHP;
    //[SerializeField] TextMeshProUGUI vidaText;

    //[SerializeField] AtaqueFisico ataqFisico;


    //[SerializeField] Image barraSP;
    //[SerializeField] TextMeshProUGUI spText;
    //[SerializeField] AtaqueMagico[] ataqMagicos;
    //[SerializeField] Sprite iconElementoSprite;
    //[SerializeField] Image iconElementoImage;

    [SerializeField] protected CaracterCreation myCaracter;
    [SerializeField] private int caracterNumber;
    [SerializeField] protected GameEvent takeDamageEV;
    [SerializeField] SpriteRenderer sRenderer;
    [SerializeField] string spritePath;

    [Header("Sprites")]

    [SerializeField] Sprite OriginalSp;
    [SerializeField] Sprite damageEarthSp;
    [SerializeField] Sprite damageFireSp;
    [SerializeField] Sprite damageMetalSp;
    [SerializeField] Sprite damagePhysicalSp;
    [SerializeField] Sprite damagePlantSp;
    [SerializeField] Sprite damageWaterSp;
    [SerializeField] protected Sprite deathSp;
    [SerializeField] Sprite healingSp;
    [SerializeField] Sprite blockDamageSp;


    public Dictionary<StatusFx, int> currentStatus = new Dictionary<StatusFx, int>();


    public CaracterCreation MyCaracter { get => myCaracter; set => myCaracter = value; }
    public int CaracterNumber { get => caracterNumber; set => caracterNumber = value; }

    protected virtual void Start()
    {
        combatMg = CombatManager.combatInstance;
        uIManager = CombatUiManager.uiInstance;
        sRenderer = GetComponent<SpriteRenderer>();
        //StartCoroutine(WaitASec());
    }

    void Update()
    {
        
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
    }

    public virtual void TakeDamage(int dmToTake, DAMAGETYPE dmType)
    {
        bool isShielded = false;
        foreach (var status in currentStatus.ToList())
        {
            if(status.Key is ShieldFx)
            {
                isShielded = true;
                currentStatus.Remove(status.Key);
            }
        }
        if (this.GetComponent<BaseStats>() is PlayableCaracter)
        {
            Debug.Log("WTF");
            uIManager.RepresentStatusFx(this);
        }
        else if (this.GetComponent<BaseStats>() is Enemy)
        {
            this.GetComponent<Enemy>().EnemyStatusFx();
        }
        if (isShielded == false)
        {
            float val = ElementInteractions.CheckInteraction(MyCaracter.Type, dmType);
            if(val != 0)
            {
                float finalDamage = dmToTake * val;
                myCaracter.HpMax.value -= (int)finalDamage;
                if(myCaracter.HpMax.value > 0)
                {
                    SpriteChange(dmType);
                }
                combatMg.SpawnFloatingDamage(transform.position + new Vector3(2,0,0), (int)finalDamage);
            }
            else
            {
                SpriteChange();
            }
            takeDamageEV.Raise();
        }
        else
        {
            Debug.Log("Was shielded");
        }
        CheckIfDead();
    }

    protected virtual void CheckIfDead()
    {

    }

    public virtual void HealCaracter(int healingAmount)
    {
        myCaracter.HpMax.value += healingAmount;
        StartCoroutine(SpriteChangor(healingSp));
        if (myCaracter.HpMax.value > myCaracter.HpMax.resetValue)
        {
            myCaracter.HpMax.value = myCaracter.HpMax.resetValue;
        }
        takeDamageEV.Raise();
    }

    private void SpriteChange(DAMAGETYPE dmType)
    {
        switch (dmType)
        {
            case DAMAGETYPE.Physical:
                StartCoroutine(SpriteChangor(damagePhysicalSp));
                break;
            case DAMAGETYPE.Fire:
                StartCoroutine(SpriteChangor(damageFireSp));
                break;
            case DAMAGETYPE.Water:
                StartCoroutine(SpriteChangor(damageWaterSp));
                break;
            case DAMAGETYPE.Rock:
                StartCoroutine(SpriteChangor(damageEarthSp));
                break;
            case DAMAGETYPE.Nature:
                StartCoroutine(SpriteChangor(damagePlantSp));
                break;
            case DAMAGETYPE.Metal:
                StartCoroutine(SpriteChangor(damageMetalSp));
                break;
            default:    
                break;
        }
    }

    private void SpriteChange()
    {
        StartCoroutine(SpriteChangor(blockDamageSp));
    }

    private IEnumerator SpriteChangor(Sprite sp)
    {
        sRenderer.sprite = sp; 
        yield return new WaitForSeconds(2f);
        sRenderer.sprite = OriginalSp;
    }


}

// Asset bundle e Addressables
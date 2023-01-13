using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

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

    [SerializeField] private CaracterCreation myCaracter;
    [SerializeField] protected int _caracterNumber;
    [SerializeField] protected GameEvent takeDamageEV;
    public CaracterCreation MyCaracter { get => myCaracter; set => myCaracter = value; }

    protected virtual void Start()
    {
        combatMg = CombatManager.combatInstance;
        uIManager = CombatUiManager.uiInstance;
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
        myCaracter.HpMax.value -= dmToTake;
        takeDamageEV.Raise();
    }

    
}

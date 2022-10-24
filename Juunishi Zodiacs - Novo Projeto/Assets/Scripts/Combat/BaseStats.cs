using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum TipoPoder
{
    Agua, Plantas, Fogo, Terra, Metal, Fisico
}

public class BaseStats : MonoBehaviour
{
    CombatManager combatMg;

    [SerializeField] string nome;
    //[SerializeField] TextMeshProUGUI nomeDisplay;
    [SerializeField] TipoPoder tipo; 

    [SerializeField] int _hpMax;
    [SerializeField] int _curHp;
    [SerializeField] int _shieldHp;
    //[SerializeField] Image barraHP;
    //[SerializeField] TextMeshProUGUI vidaText;
    
    //[SerializeField] AtaqueFisico ataqFisico;

    
    //[SerializeField] Image barraSP;
    //[SerializeField] TextMeshProUGUI spText;
    //[SerializeField] AtaqueMagico[] ataqMagicos;
    //[SerializeField] Sprite iconElementoSprite;
    //[SerializeField] Image iconElementoImage;

    void Start()
    {
        combatMg = CombatManager.combatInstance;
    }

    void Update()
    {
        
    }
}

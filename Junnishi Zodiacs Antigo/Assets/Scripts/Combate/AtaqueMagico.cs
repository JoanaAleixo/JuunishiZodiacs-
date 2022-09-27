using System.Collections;
using System.Collections.Generic;
using UnityEngine;

     enum Range { All, One } //quando elementos da party ele afeta
     enum Consumo { HP, SP} //implementar so depois

[CreateAssetMenu(menuName = "Ataque Mágico")]
public class AtaqueMagico : ScriptableObject
{
    //Stats do ataque mágico

    [SerializeField] string nome;
    [SerializeField] Sprite icon;

    [SerializeField] TipoPoder tipo;
    [SerializeField] Range tipoRange; 
    [SerializeField] Consumo tipoConsumo;

    [SerializeField] int dano;
    [SerializeField] int custo;

    #region Propriedades
    public Sprite Icon { get => icon; set => icon = value; }
    public TipoPoder Tipo { get => tipo; set => tipo = value; }
    public string Nome { get => nome; set => nome = value; }
    public int Dano { get => dano; }
    public int Custo { get => custo; set => custo = value; }
    internal Range TipoRange { get => tipoRange; set => tipoRange = value; }
    internal Consumo TipoConsumo { get => tipoConsumo; set => tipoConsumo = value; }
    #endregion
}
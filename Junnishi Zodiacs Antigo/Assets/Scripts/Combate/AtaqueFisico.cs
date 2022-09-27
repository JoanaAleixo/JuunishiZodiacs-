using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ataque Fisico")]
public class AtaqueFisico : ScriptableObject
{
    //stats do ataque fisico

    [SerializeField] int dano;
    [SerializeField] string descri��o;
    //efeito

    public int Dano { get => dano; }
    public string Descri��o { get => descri��o; set => descri��o = value; }
}
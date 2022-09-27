using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ataque Fisico")]
public class AtaqueFisico : ScriptableObject
{
    //stats do ataque fisico

    [SerializeField] int dano;
    [SerializeField] string descrição;
    //efeito

    public int Dano { get => dano; }
    public string Descrição { get => descrição; set => descrição = value; }
}
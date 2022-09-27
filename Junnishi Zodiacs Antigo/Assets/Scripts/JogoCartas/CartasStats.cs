using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoCarta
{
    Pedra, Papel, Tesoura
}

[CreateAssetMenu(menuName = "Carta")]

public class CartasStats : ScriptableObject
{
    [SerializeField] TipoCarta tipoDeCarta;
    [SerializeField] TipoCarta vence;
    [SerializeField] TipoCarta perde;
    [SerializeField] Sprite spriteCarta;

    public TipoCarta TipoDeCarta { get => tipoDeCarta; set => tipoDeCarta = value; }
    public TipoCarta Vence { get => vence; set => vence = value; }
    public TipoCarta Perde { get => perde; set => perde = value; }
    public Sprite SpriteCarta { get => spriteCarta; set => spriteCarta = value; }
}

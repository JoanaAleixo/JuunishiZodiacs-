using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JogadorDeCartas : MonoBehaviour
{
    [SerializeField] string nome;
    [SerializeField] TextMeshProUGUI nomeDisplay;
    
    [SerializeField] CartasStats[] cartas;

    [SerializeField] GameObject danoEfeito;

    public string Nome { get => nome; set => nome = value; }
    public CartasStats[] Cartas { get => cartas; set => cartas = value; }

    void Start()
    {
        nomeDisplay.GetComponent<TextMeshProUGUI>();
        nomeDisplay.text = Nome;
    }

    //Personagem tem nome
    //Personagem tem pontuação
    //Personagem tem que receber dano ao perder
    //Personagem tem que ter uma seta a indicar a sua vez
    //Personagem tem que ter as cartas obviamente
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

enum EstadoJogoCartas
{ 
    Come�oJogo, Jogador1Turn, Jogador2Turn, Vitoria, Derrota
}

public class JogoDeCartasManager : MonoBehaviour
{
    [SerializeField] int pontua��oMax;
    [SerializeField] TextMeshProUGUI descri�aoDisplay;
    int pontos1;
    int pontos2;
    [SerializeField] JogadorDeCartas jogador1;
    [SerializeField] JogadorDeCartas jogador2;
    [SerializeField] TextMeshProUGUI pontos1Display;
    [SerializeField] TextMeshProUGUI pontos2Display;

    EstadoJogoCartas estadoJogo;

    [SerializeField] CartasStats cartaGuardadaJogador1;
    [SerializeField] CartasStats cartaGuardadaJogador2;
    [SerializeField] Sprite cartaCostasOriginal;
    [SerializeField] Image cartaGuardadaJogador2Sprite;
    [SerializeField] CartasInterface cartaGuardadaMudarSprite;

    //[SerializeField] TextMeshProUGUI cartaJogador2Display;

    bool jogador1PrimeiroJogar;

    #region Propriedades
    internal EstadoJogoCartas EstadoJogo { get => estadoJogo; set => estadoJogo = value; }
    public CartasStats CartaGuardadaJogador1 { get => cartaGuardadaJogador1; set => cartaGuardadaJogador1 = value; }
    #endregion

    void Start()
    {
        pontos1Display.GetComponent<TextMeshProUGUI>();
        pontos2Display.GetComponent<TextMeshProUGUI>();

        cartaGuardadaJogador2Sprite.GetComponent<Image>();
        cartaGuardadaMudarSprite = cartaGuardadaJogador2Sprite.GetComponent<CartasInterface>();

        EstadoJogo = EstadoJogoCartas.Come�oJogo;
        Debug.Log("Come�o do jogo");
        StartCoroutine(Come�arJogo());
    }

    IEnumerator Come�arJogo()
    {
        descri�aoDisplay.text = ("Start!!!");
        yield return new WaitForSeconds(1.5f);
        EstadoJogo = EstadoJogoCartas.Jogador1Turn;
        Debug.Log("Vez de" + jogador1.Nome);
        descri�aoDisplay.text = ("Choose Your Cards!!!");
        jogador1PrimeiroJogar = true;
    }


    public void ProximoMovimento()
    {
        if(EstadoJogo == EstadoJogoCartas.Jogador1Turn) //se o estado for o jogador 1
        {
            if (cartaGuardadaJogador2 != null) //se ambos tiverem carta, Check Derrota (situa��o que jogador 1, � o segundo a jogar)
            {
                jogador1PrimeiroJogar = true;
                CheckDerrota();
            }
            else
            {
                StartCoroutine(JogadaInimigo()); //se o jogador 2 nao tiver carta, � ele (situa��o que jogador 1, � o primeiro a jogar)
            }
        }
        else if (EstadoJogo == EstadoJogoCartas.Jogador2Turn) //se o estado for o jogador 2
        {
            if (CartaGuardadaJogador1 != null) //(situa��o em que jogador 1 � primeiro)
            {
                jogador1PrimeiroJogar = false;
                CheckDerrota();
            }
            else
            {
                Debug.Log("Vez de " + jogador1.Nome);
                EstadoJogo = EstadoJogoCartas.Jogador1Turn; //(situa��o em que jogador 2 � primeiro)
            }
        }
    }

    IEnumerator JogadaInimigo()
    {
        Debug.Log("Vez de " + jogador2.Nome);
        estadoJogo = EstadoJogoCartas.Jogador2Turn;
        
        yield return new WaitForSeconds(0.5f);

        if(cartaGuardadaJogador2 == null)
        {
            int cartaRandom = Random.Range(0, jogador2.Cartas.Length);
            CartasStats cartaEscolhida = jogador2.Cartas[cartaRandom];
            cartaGuardadaJogador2 = cartaEscolhida;
            ProximoMovimento();
        }
    }

    public void CheckDerrota()
    {
        if(pontos1 != pontua��oMax && pontos2 != pontua��oMax)
        {
            descri�aoDisplay.text = ("Show Your Cards!!!");

            //cartaJogador2Display.text = cartaGuardadaJogador2.name;
            cartaGuardadaMudarSprite.MostrarCarta(cartaGuardadaJogador2Sprite, cartaGuardadaJogador2.SpriteCarta);


            if (cartaGuardadaJogador1.TipoDeCarta == cartaGuardadaJogador2.TipoDeCarta) //se os tipos forem iguais
            {
                Debug.Log("Empate");
                StartCoroutine(MovimentoPosPonto());
            }
            else if (cartaGuardadaJogador2.TipoDeCarta == cartaGuardadaJogador1.Vence) //se o tipo de carta 1 vencer do 2
            {
                Debug.Log("Jogador 1 recebe ponto");
                AdicionarScore();
            }
            else if (cartaGuardadaJogador2.TipoDeCarta == cartaGuardadaJogador1.Perde) //se o tipo de carta 1 perder do 2
            {
                Debug.Log("Jogador 2 recebe ponto");
                AdicionarScore();
            }
        }
        else
        {
            if (pontos1 >= pontua��oMax)
            {
                estadoJogo = EstadoJogoCartas.Vitoria;
                Debug.Log("Vitoria");
                descri�aoDisplay.text = ("Victory!!!");
            }
            else if (pontos2 >= pontua��oMax)
            {
                estadoJogo = EstadoJogoCartas.Derrota;
                Debug.Log("Derrota");
                descri�aoDisplay.text = ("Defeat!!!");
            }
        }
    }

    void AdicionarScore()
    {
        if (cartaGuardadaJogador2.TipoDeCarta == cartaGuardadaJogador1.Vence) //se o tipo de carta 1 vencer do 2
        {
            pontos1++;
            pontos1Display.text = pontos1.ToString();
            StartCoroutine(MovimentoPosPonto());
        }
        else if (cartaGuardadaJogador2.TipoDeCarta == cartaGuardadaJogador1.Perde) //se o tipo de carta 1 perder do 2
        {
            pontos2++;
            pontos2Display.text = pontos2.ToString();
            StartCoroutine(MovimentoPosPonto());
        }
    }

    IEnumerator MovimentoPosPonto()
    {
        yield return new WaitForSeconds (2f);

        if (pontos1 != pontua��oMax || pontos2 != pontua��oMax)
        {
            cartaGuardadaJogador1 = null;
            cartaGuardadaJogador2 = null;
            cartaGuardadaJogador2Sprite.sprite = cartaCostasOriginal;
            //cartaJogador2Display.text = "";
            descri�aoDisplay.text = ("Choose Your Cards!!!");

            if (jogador1PrimeiroJogar)
            {
                Debug.Log("Vez de" + jogador1.Nome);
                EstadoJogo = EstadoJogoCartas.Jogador1Turn;
            }
            else
            {
                StartCoroutine(JogadaInimigo());
            }
        }
    }


    //jogador come�a
    //jogador pode escolher carta ao carreg�-la
    //inimigo escolhe a sua carta � toa
    //manager deve verificar a situa��o e atribuir a pontua��o
    //tamb�m deve verificar se a pontua��o � inferior � pontua��o max
    //inimigo come�a primeiro a seguir
    //por assim vai
}

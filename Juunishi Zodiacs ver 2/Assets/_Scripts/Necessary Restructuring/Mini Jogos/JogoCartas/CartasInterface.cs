using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CartasInterface : MonoBehaviour
{
    [SerializeField] JogoDeCartasManager jogoCartasManager;
    CartasStats cartaGuardada;

    SpriteRenderer spriteRenderer;
    Image imagem;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void GuardarCartaBotao(CartasStats carta)
    {
        if(jogoCartasManager.EstadoJogo == EstadoJogoCartas.Jogador1Turn)
        {
            if(jogoCartasManager.CartaGuardadaJogador1 == null)
            {
                cartaGuardada = carta;
                jogoCartasManager.CartaGuardadaJogador1 = cartaGuardada;
                jogoCartasManager.ProximoMovimento();
            }
        }
    }

    public void MostrarCarta(Image imagem, Sprite imagemCarta)
    {
        imagem.sprite = imagemCarta;
    }
}

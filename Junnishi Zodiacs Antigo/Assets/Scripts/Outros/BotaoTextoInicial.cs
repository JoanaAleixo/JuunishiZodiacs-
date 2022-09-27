using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoTextoInicial : MonoBehaviour
{
    [SerializeField] Transi��o transicao;

    private void Start()
    {
        transicao = transicao.gameObject.GetComponent<Transi��o>();
    }

    public void TextoInicialBotao()
    {
        foreach (var item in transicao.CoisasAparecer)
            item.gameObject.SetActive(true);
        foreach (var item in transicao.CoisasDesaparecer)
            item.gameObject.SetActive(false);

        transicao.Animator.SetTrigger("FadeIn");

        gameObject.SetActive(false);
    }
}

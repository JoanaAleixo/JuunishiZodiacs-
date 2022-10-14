using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoTextoInicial : MonoBehaviour
{
    [SerializeField] Transição transicao;

    private void Start()
    {
        transicao = transicao.gameObject.GetComponent<Transição>();
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

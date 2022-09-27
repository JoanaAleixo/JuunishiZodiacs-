using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoEscolhas : MonoBehaviour
{
    [SerializeField] GameObject escolhas;
    [SerializeField] GameObject dialogogrupo;
    [SerializeField] GameObject dialogoSeguinte;
    [SerializeField] GameObject cg;

    [SerializeField] GameObject textoDisplay;
    [SerializeField] GameObject nomeDisplay;

    //adicionar estas desgraças ^ depois aos arrays v 

    [SerializeField] GameObject[] coisasAparecer;
    [SerializeField] GameObject[] coisasDesaparecer;
    [SerializeField] bool mudançaSitio;
    [SerializeField] MovementManager movementManager;
    [SerializeField] int cenario;

    [SerializeField] bool obejtivoFinalMissao;
    [SerializeField] GameObject grid;

    public void EscolhaBotao() //botao usado nas escolhas
    {
        textoDisplay.gameObject.SetActive(true);
        nomeDisplay.gameObject.SetActive(true);

        //auge da programação, foi para isto que paguei o curso v

        if(dialogogrupo!=null)
            dialogogrupo.gameObject.SetActive(true);
        if(dialogoSeguinte!=null)
            dialogoSeguinte.gameObject.SetActive(true);
        if(cg != null)
            cg.gameObject.SetActive(true);

        foreach (var item in coisasAparecer)
            item.gameObject.SetActive(true);

        foreach (var item in coisasDesaparecer)
            item.gameObject.SetActive(false);

        if(mudançaSitio)
        {
            movementManager.BotaoMovimento(cenario);

            if (obejtivoFinalMissao)
            {
                for (int i = 0; i < grid.transform.childCount; i++)
                {
                    Destroy(grid.transform.GetChild(i).gameObject);
                }

                obejtivoFinalMissao = false;
            }
        }

        escolhas.gameObject.SetActive(false);
    }
}

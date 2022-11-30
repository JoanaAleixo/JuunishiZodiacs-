using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BotaoMovFalso : MonoBehaviour
{
    Color corOriginal;
    Color corRatoPorCima = Color.cyan;
    SpriteRenderer spriteRenderer;

    [SerializeField] MovementManager movManager;
    [SerializeField] int cenario;

    [SerializeField] GameObject paraOndeVai;
    [SerializeField] TextMeshProUGUI nomecenarioDisplay;
    [SerializeField] string nomeCenario;

    [SerializeField] GameObject sitio;

    [SerializeField] GameObject dialogo;
    [SerializeField] GameObject[] coisasADesaparecer;
    [SerializeField] GameObject[] coisasADesaparecerDialogo;
    [SerializeField] GameObject[] aparecePorAmordaSanta;

    [SerializeField] bool obejtivoFinalMissao;
    [SerializeField] GameObject grid;
    

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        corOriginal = spriteRenderer.color;
        movManager = movManager.GetComponent<MovementManager>();

        if (paraOndeVai != null)
        {
            nomecenarioDisplay.GetComponent<TextMeshProUGUI>();
            nomecenarioDisplay.text = nomeCenario;
        }
    }

    private void OnMouseOver()
    {
        if(GameManager.Instance.EstadoDoJogo == EstadoDoJogo.NaoDialogo)
        {
            spriteRenderer.color = corRatoPorCima;
            if(paraOndeVai != null)
                paraOndeVai.gameObject.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
            spriteRenderer.color = corOriginal;
            if (paraOndeVai != null)
                paraOndeVai.gameObject.SetActive(false);
    }


    public void OnMouseDown()
    {
        if (GameManager.Instance.EstadoDoJogo == EstadoDoJogo.NaoDialogo)
        {
            movManager.BotaoMovimento(cenario);

            if(obejtivoFinalMissao)
            {
                for (int i = 0; i < grid.transform.childCount; i++)
                {
                    Destroy(grid.transform.GetChild(i).gameObject);
                }

                obejtivoFinalMissao = false;
            }
        
            if (sitio != null)
            {
                sitio.gameObject.SetActive(true);
                movManager.PosiçaoCamera = sitio.transform;
                movManager.CameraMover.transform.position = new Vector3(movManager.PosiçaoCamera.transform.position.x, movManager.PosiçaoCamera.transform.position.y, -10);
            
            }

            foreach (var item in aparecePorAmordaSanta)
            {
                if (item != null)
                    item.gameObject.SetActive(true);
            }

            foreach (var item in coisasADesaparecer)
            {
                if(item != null)
                    item.gameObject.SetActive(false);
            }




            if (dialogo != null)
            {
                dialogo.gameObject.SetActive(true);

                spriteRenderer.color = corOriginal;
                if (paraOndeVai != null)
                    paraOndeVai.gameObject.SetActive(false);

                foreach (var item in coisasADesaparecerDialogo)
                {
                    if (item != null)
                        item.gameObject.SetActive(false);
                }

            }

        }
    }
}

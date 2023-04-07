using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
    
enum ItemCategoria { Photo, ConsumivelHP, ConsumivelSP, KeyItem, Gift, WorldItem}

public class ItemColetar : MonoBehaviour
{
    [SerializeField] ItemCategoria categoriaItem;
    [SerializeField] Item info;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite sprite;

    Color corOriginal;
    Color corRatoPorCima = Color.cyan;


    [SerializeField] Transform mensagemParent;
    //[SerializeField] MensagemInfoInterface mensagemPrefab;

    Album album;

    [SerializeField] Inventário inventario;

    [SerializeField] GameObject dialogoDesbloqueado;
    [SerializeField] GameObject[] coisasDesaparecer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        corOriginal = spriteRenderer.color;
    }

    private void OnMouseOver()
    {
        spriteRenderer.color = corRatoPorCima;
    }

    private void OnMouseExit()
    {
        spriteRenderer.color = corOriginal;
    }

    void OnMouseDown() //ao carregar no item
    {
        //if (GameManager.Instance.EstadoDoJogo == EstadoDoJogo.NaoDialogo)
        //{
            if (dialogoDesbloqueado)
            {
                foreach (var item in coisasDesaparecer)
                {
                    item.SetActive(false);
                }
                dialogoDesbloqueado.SetActive(true);
            }
            //AparecerMensagem(info);
            ColetarItem();
        //}
        //else
        //    return;
    }
    //void AparecerMensagem(Item item)
    //{
    //    MensagemInfoInterface mensagem = Instantiate(mensagemPrefab, mensagemParent.transform);
    //    mensagem.Mensagem(item);
    //}

    void ColetarItem()
    {
        if(categoriaItem == ItemCategoria.Photo)
        {
            album = MainMenuManager.Instance.Album;
            album.Imagens.Add(spriteRenderer.sprite);
        }
        else
        {
            inventario.AdicionarItemInventario(info);
        }

        Debug.Log("Item coletado");
        gameObject.SetActive(false);
    }

}

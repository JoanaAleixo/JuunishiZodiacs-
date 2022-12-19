using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    static MainMenuManager instance;

    [SerializeField] bool tutorialUsado;
    [SerializeField] GameObject tutorial;
    
    [SerializeField] GameObject iconeMenuPrincipal;

    [SerializeField] GameObject menuPrincipal;
    [SerializeField] Image ecraTelemóvel;
    [SerializeField] GameObject botoesMenu;
    [SerializeField] Sprite[] spritesEcra;

    [SerializeField] GameObject[] interfaces;
    int interfaceAtiva;
    [SerializeField] GameObject botaoVoltarAtras;


    [SerializeField] Album album;
    [SerializeField] Image foto;
    int posicaoNoAlbum;

    [SerializeField] ItemInterface itemPrefab;
    [SerializeField] Transform invetarioParent;

    [SerializeField] RemindersInterface reminderPrefab;
    [SerializeField] Transform reminderPrefabParent;

    [SerializeField] GameObject transicao;
    Animator animator;

    #region Propriedades
    public static MainMenuManager Instance { get => instance; set => instance = value; }
    public Album Album { get => album; set => album = value; }
    #endregion

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(instance);
        }
    }

    void Start()
    {
        ecraTelemóvel.GetComponent<Sprite>();
        foto.GetComponent<Sprite>();
        if(transicao != null)
            animator = transicao.GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    //Botoes do ecra

    #region Bootões menu principal
    public void AbrirMenuPrinciPalBotao()
    {
        menuPrincipal.gameObject.SetActive(true);
        iconeMenuPrincipal.gameObject.SetActive(false);

        if (tutorialUsado == false)
            tutorial.gameObject.SetActive(true);

        //GameManager.Instance.EstadoDoJogo = EstadoDoJogo.MenuPrincipal;
    }

    public void FecharTutorialBotao()
    {
        tutorialUsado = true;
        Destroy(tutorial);
    }

    public void FecharMenuPrinciPalBotao()
    {
        iconeMenuPrincipal.gameObject.SetActive(true);
        menuPrincipal.gameObject.SetActive(false);

        //GameManager.Instance.EstadoDoJogo = EstadoDoJogo.NaoDialogo;
    }


    void AtivarInterfaceApp(int numero)
    {
        botaoVoltarAtras.gameObject.SetActive(true);
        interfaceAtiva = numero;
        MudarSpriteEcra(spritesEcra[interfaceAtiva]);
        interfaces[interfaceAtiva].gameObject.SetActive(true);
    }

    void MudarSpriteEcra(Sprite sprite)
    {
        botoesMenu.gameObject.SetActive(false);
        ecraTelemóvel.sprite = sprite;
    }

    public void VoltarEcraPrincipalBotao(int numero)
    {
        numero = interfaceAtiva;
        interfaces[numero].gameObject.SetActive(false);
        botoesMenu.gameObject.SetActive(true);
        ecraTelemóvel.sprite = spritesEcra[0];
        botaoVoltarAtras.gameObject.SetActive(false);
    }

    #endregion

    #region Abrir Apps
    public void FotosBotao()                //Fotos
    {
        AtivarInterfaceApp(1);
    }

    public void MessagensBotao()            //Mensagens
    {
        AtivarInterfaceApp(2);
    }

    public void ContactosBotao()            //Contactos
    {
        AtivarInterfaceApp(3);
    }

    public void NotasBotao()                // Notas
    {
        AtivarInterfaceApp(4);
    }
    public void MapaBotao()                 //Mapa
    {
        AtivarInterfaceApp(5);
    }

    public void LembretesBotao()            //Missoes
    {
        AtivarInterfaceApp(6);
    }

    public void BagBotao()                  //Inventario
    {
        AtivarInterfaceApp(7);
    }

    public void OpcoesBotao()               //Opcoes
    {
        AtivarInterfaceApp(8);
    }
    #endregion

    //Elementos das interfaces

    #region Album 
    public void ProximaFotoBotao()
    {
        if(posicaoNoAlbum >= album.Imagens.Count-1) //se o album chegar ao fim
        {
            posicaoNoAlbum = 0;
            foto.sprite = Album.Imagens[posicaoNoAlbum];
        }
        else
        {
            posicaoNoAlbum++;
            foto.sprite = Album.Imagens[posicaoNoAlbum];
        }
    }

    public void FotoAnteriorBotao()
    {
        if (posicaoNoAlbum == 0) //se o album chegar novamente ao inicio
        {
            posicaoNoAlbum = album.Imagens.Count -1;
            foto.sprite = Album.Imagens[posicaoNoAlbum];
        }
        else
        {
            posicaoNoAlbum--;
            foto.sprite = Album.Imagens[posicaoNoAlbum];
        }
    }
    #endregion

    #region Bag
    public void AdicionarItemInventario(Item item)
    {
        ItemInterface itemInstaciado = Instantiate(itemPrefab, invetarioParent);
        itemInstaciado.MudarIconENome(item);
    }
    #endregion

    #region Reminders
    public void Reminders(string missao)
    {
        RemindersInterface mensagem = Instantiate(reminderPrefab, reminderPrefabParent.transform);
        mensagem.Missao(missao);
  
    }

    #endregion

    #region Options
    public void TelaInicialBotao()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("MenuInicial");
    }

    #endregion
}

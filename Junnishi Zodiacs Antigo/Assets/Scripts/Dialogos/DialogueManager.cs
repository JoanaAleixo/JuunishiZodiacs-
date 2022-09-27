using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    [SerializeField] UIManager ui;
    SpriteRenderer spriteRenderer;

    [SerializeField] bool destruirTemporario;

    [SerializeField] Image caixaDeTexto;
    [SerializeField] Image spritePers;

    [SerializeField] DialogueTree arvoreDialogo;
    int posicaoNoDialogo;
    int posicaoNoNome;
    Image spriteCaixa;
    Image spritePersonagem;

    [SerializeField] bool transicaoMeioDialogo;
    bool jaComeçouLindo;

    #region Variaveis a seguir a dialogo
    [SerializeField] GameObject[] coisasAAtivar;
    [SerializeField] GameObject[] coisasADesativar;

    [SerializeField] string proximaScene;

    [SerializeField] string mensagemInformativa;
    [SerializeField] string mensagemInformativaOpcional;
    Transform mensagemParent;
    [SerializeField] MensagemInfoInterface mensagemPrefab;

    #endregion

    public string MensagemInformativa { get => mensagemInformativa; set => mensagemInformativa = value; }
    public DialogueTree ArvoreDialogo { get => arvoreDialogo; set => arvoreDialogo = value; }
    public int PosicaoNoDialogo { get => posicaoNoDialogo; set => posicaoNoDialogo = value; }

    private void Start()
    {
        GameManager.Instance.EstadoDoJogo = EstadoDoJogo.Diálogo;

        spriteCaixa = caixaDeTexto.GetComponent<Image>();
        spritePersonagem = spritePers.GetComponent<Image>();
        if (mensagemPrefab != null)
            mensagemParent = GameObject.Find("/Canvas/MensagemParent").transform;
        jaComeçouLindo = true;
        ContinuarDialogo();
    }
    public void EuSouOBotãoOnClick()
    {

        ContinuarDialogo();
    }

    private void OnEnable()
    {
        if(jaComeçouLindo)
        {
            posicaoNoDialogo = 0;
            posicaoNoNome = 0;
            ContinuarDialogo();
        }
    }

    public void ComeçarDialogo()
    {
        Debug.Log(arvoreDialogo);
        posicaoNoDialogo = 0;
        posicaoNoNome = 0;
        Debug.Log(posicaoNoDialogo);

        spriteCaixa.sprite = ArvoreDialogo.Dialogue[PosicaoNoDialogo].CaixaDialogo; //sprite caixa
        spritePersonagem.sprite = ArvoreDialogo.Dialogue[PosicaoNoDialogo].PersonagemImagem; //sprite personagem
        ui.CaixaNome(ArvoreDialogo.Dialogue[posicaoNoNome].SpeakerNome); //aparecer nome

        ui.ComecarCoroutine(ArvoreDialogo.Dialogue[PosicaoNoDialogo].Fala); //aparecer dialogo
    }


    #region Correr Dialogo
    public void ContinuarDialogo()
    {

        if (PosicaoNoDialogo >= ArvoreDialogo.Dialogue.Length)
        {
            AseguirAoDiálogo();

            return;
        }
        if (ui.CorroutineACorrer) //carregar 2 vez no botao
        {
            ui.TermincarCoroutine();
            ui.Dialogo.text = ArvoreDialogo.Dialogue[PosicaoNoDialogo].Fala;

            PosicaoNoDialogo++;
            posicaoNoNome++;
        }
        else //carregar 1 vez no botao
        {
            spriteCaixa.sprite = ArvoreDialogo.Dialogue[PosicaoNoDialogo].CaixaDialogo; //sprite caixa
            spritePersonagem.sprite = ArvoreDialogo.Dialogue[PosicaoNoDialogo].PersonagemImagem; //sprite personagem
            ui.CaixaNome(ArvoreDialogo.Dialogue[posicaoNoNome].SpeakerNome); //aparecer nome

            ui.ComecarCoroutine(ArvoreDialogo.Dialogue[PosicaoNoDialogo].Fala); //aparecer dialogo
        }
    }
    #endregion

    #region Aseguir ao Dialogo
    void AseguirAoDiálogo()
    {
        GameManager.Instance.EstadoDoJogo = EstadoDoJogo.NaoDialogo;

        foreach (var item in coisasAAtivar) //ativar próximo local, dialogo e personagens
            item.gameObject.SetActive(true);

        if (proximaScene != "") //caso necessário mudar de scene
        {
            if(transicaoMeioDialogo)
                ui.ProximaSceneSemFade(proximaScene);
            else
                ui.ProximaSceneComFade(proximaScene);
        }

        foreach (var item in coisasADesativar) //desativar dialogos atuais
            item.gameObject.SetActive(false);

        if (mensagemInformativa != "") 
        {
            MensagemInfoInterface mensagem = Instantiate(mensagemPrefab, mensagemParent.transform);
            mensagem.Missao(mensagemInformativa);
        }

        if (mensagemInformativaOpcional != "")
        {
            MensagemInfoInterface newMensagem = Instantiate(mensagemPrefab, mensagemParent.transform);
  
            newMensagem.Missao(mensagemInformativaOpcional);
        }

        ui.CaixaDialogo(""); //facilita o trabalho de não ter que desativar e ativar sempre as coisas
        ui.CaixaNome("");

        ui.TermincarCoroutine();

        gameObject.SetActive(false);
        if(destruirTemporario)
            Destroy(gameObject);
    }
    #endregion
}
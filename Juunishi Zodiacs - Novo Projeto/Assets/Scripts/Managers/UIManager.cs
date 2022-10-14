using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject menuInicial;
    [SerializeField] GameObject menuOpçoes;
    [SerializeField] GameObject menuExtras;
    [SerializeField] GameObject menuPausa;

    #region Variáveis Sliders
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider sliderVolume;

    [SerializeField] Slider sliderBrilho;
    [SerializeField] CanvasGroup canvasgroupDaTela;

    #endregion

    #region Variáveis Texto
    [SerializeField] TextMeshProUGUI nomePersonagem;
    [SerializeField] TextMeshProUGUI dialogo;
    [SerializeField] float velocidadeTexto;

    Coroutine typingeffectCoroutine;

    [SerializeField] GameObject transicao;
    Animator animator;
    bool corroutineACorrer;

    public TextMeshProUGUI Dialogo { get => dialogo; set => dialogo = value; }
    public float VelocidadeTexto { get => velocidadeTexto; set => velocidadeTexto = value; }
    public Coroutine TypingeffectCoroutine { get => typingeffectCoroutine; set => typingeffectCoroutine = value; }
    public bool CorroutineACorrer { get => corroutineACorrer; set => corroutineACorrer = value; }
    public GameObject MenuPausa { get => menuPausa; set => menuPausa = value; }
    #endregion


    void Start()
    {
        if (transicao != null)
            animator = transicao.GetComponent<Animator>();

        if(PlayerPrefs.HasKey("Volume"))
        {
            sliderVolume.value = PlayerPrefs.GetFloat("Volume");
            audioMixer.SetFloat("Volume", PlayerPrefs.GetFloat("Volume"));
        }
        if(PlayerPrefs.HasKey("Brilho"))
        {
            sliderBrilho.value = PlayerPrefs.GetFloat("Brilho");
            canvasgroupDaTela.alpha = sliderBrilho.value;
        }

    }


    #region Botões menu inicial
    public void PressToStartBotao()
    {
        menuInicial.gameObject.SetActive(true);
    }

    public void NewGameBotao()
    {
        ProximaSceneComFade("Introdução");
    }

    public void OptionsBotaoInicio()
    {
        menuOpçoes.gameObject.SetActive(true);
    }

    public void ExtrasBotao()
    {
        menuExtras.gameObject.SetActive(true);
    }

    public void BackBotao()
    {
        menuOpçoes.gameObject.SetActive(false);
        menuExtras.gameObject.SetActive(false);
    }

    public void ExitBotao()
    {
        Debug.Log("Sair");
        Application.Quit();
    }
    #endregion

    #region Sliders Volume e Brilho

    public void VolumeSlider(float volume) //guardar os valores de volume
    {
        audioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save(); //<- não esquecer 
    }

    public void BrilhoSlider(float brilho) //guardar os valores de brilho
    {
        sliderBrilho.value = brilho;
        canvasgroupDaTela.alpha = sliderBrilho.value;
        PlayerPrefs.SetFloat("Brilho", brilho);
        PlayerPrefs.Save();
    }
    #endregion

    #region Dialogos
    public void CaixaNome(string nome)
    {
        nomePersonagem.text = nome;
    }

    public void CaixaDialogo(string frases)
    {
        Dialogo.text = frases;
    }


    public void ComecarCoroutine(string linha)
    {
        if (TypingeffectCoroutine != null)
        {
            StopCoroutine(TypingeffectCoroutine);
        }

        TypingeffectCoroutine = StartCoroutine(TypingEffect(linha));
    }

    public IEnumerator TypingEffect(string linha)
    {
        Dialogo.text = "";
        CorroutineACorrer = true;

        foreach (char letra in linha.ToCharArray())
        {
            Dialogo.text += letra;
            yield return new WaitForSeconds(VelocidadeTexto);
        }
    }
    public void TermincarCoroutine()
    {
        StopCoroutine(TypingeffectCoroutine);
        CorroutineACorrer = false;
    }

    #endregion

    #region Mudança de Scene
    public void ProximaSceneComFade(string nomeScene)
    {
        Time.timeScale = 1;
        StartCoroutine(FadeOutScene(nomeScene));
    }

    IEnumerator FadeOutScene(string nomeScene)
    {
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene(nomeScene);
    }

    public void ProximaSceneSemFade(string nomeScene)
    {
        SceneManager.LoadScene(nomeScene);
    }
    #endregion

    #region Botoes Menu de Pausa
    public void ContinueBotao()
    {
        GameManager.Instance.RetomarJogo();
        MenuPausa.gameObject.SetActive(false);
    }
    #endregion


}

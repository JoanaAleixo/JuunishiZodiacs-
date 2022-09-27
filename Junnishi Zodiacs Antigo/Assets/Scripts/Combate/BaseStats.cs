using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum TipoPoder
{
    Agua, Plantas, Fogo, Terra, Metal, Fisico
}

public class BaseStats : MonoBehaviour
{
    [SerializeField] CombatManager combatManager;

    [SerializeField] string nome;
    [SerializeField] TextMeshProUGUI nomeDisplay;
    [SerializeField] TipoPoder tipo; //só para orientar

    [SerializeField] int vidaMax;
    [SerializeField] int vida;
    [SerializeField] Image barraHP;
    [SerializeField] TextMeshProUGUI vidaText;

    [SerializeField] AtaqueFisico ataqFisico;

    [SerializeField] int spMax;
    [SerializeField] int sp;
    [SerializeField] Image barraSP;
    [SerializeField] TextMeshProUGUI spText;
    [SerializeField] AtaqueMagico[] ataqMagicos;

    bool defesaAtiva;
    [SerializeField] bool inimigo;

    [SerializeField] Sprite iconElementoSprite;
    [SerializeField] Image iconElementoImage;

    [SerializeField] GameObject seta;
    [SerializeField] GameObject danoEfeito;


    Color corOriginal;
    Color corEscolherInimigo = Color.cyan;
    Color corDano = Color.red;
    Color corMorte = Color.black;
    SpriteRenderer spriteRenderer;

    #region Propriedades

    public string Nome { get => nome; set => nome = value; }
    public TipoPoder Tipo { get => tipo; set => tipo = value; }
    public int Vida { get => vida; set => vida = value; }
    public AtaqueFisico AtaqFisico { get => ataqFisico; set => ataqFisico = value; }
    public int Sp { get => sp; set => sp = value; }
    public AtaqueMagico[] AtaqMagicos { get => ataqMagicos; set => ataqMagicos = value; }
    public bool DefesaAtiva { get => defesaAtiva; set => defesaAtiva = value; }
    public int VidaMax { get => vidaMax; set => vidaMax = value; }
    public int SpMax { get => spMax; set => spMax = value; }
    public Image BarraSP { get => barraSP; set => barraSP = value; }
    public SpriteRenderer SpriteRenderer { get => spriteRenderer; set => spriteRenderer = value; }
    public Color CorOriginal { get => corOriginal; set => corOriginal = value; }
    public GameObject Seta { get => seta; set => seta = value; }

    #endregion

    private void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        CorOriginal = SpriteRenderer.color;
        combatManager = combatManager.GetComponent<CombatManager>();
        nomeDisplay.GetComponent<TextMeshProUGUI>();

        if (sp > 0)
            UpdateBarraSP(spMax);
        UpdateBarraVida(vidaMax);

        if (inimigo)
        {
            iconElementoImage.GetComponent<Image>();
            iconElementoImage.sprite = iconElementoSprite;
        }

        nomeDisplay.text = nome;
    }

    #region HP
    public void ReceberDano(int danorecebido)
    {
        if (vida > 0)
        {
            spriteRenderer.color = corDano;
            danoEfeito.gameObject.SetActive(true);
            vida -= danorecebido;
            UpdateBarraVida(vida);
            CheckMorte();
            StartCoroutine(VoltarCorOriginal());
        }
    }

    IEnumerator VoltarCorOriginal()
    {
        yield return new WaitForSeconds(1f);
        if (vida > 0)
            spriteRenderer.color = corOriginal;
        if (inimigo)
            combatManager.InimgoEscolhido = false;
        danoEfeito.gameObject.SetActive(false);
    }

    public void ConsumoHP(int hpPercentagem)
    {
        vida = vida - (vidaMax * hpPercentagem / 100);
        UpdateBarraVida(vida);
    }

    public void RegenerarVida(int hprecuperado)
    {
        vida += hprecuperado;
        UpdateBarraVida(vida);
    }

    public void UpdateBarraVida(float hp)
    {
        barraHP.fillAmount = hp / VidaMax;
        if (inimigo == false)
            vidaText.text = hp.ToString();
    }
    #endregion

    #region SP
    public void ConsumoSP(int spconsumido)
    {
        sp -= spconsumido;
        UpdateBarraSP(sp);
    }

    public void RegenerarSP(int sprecuperado)
    {
        sp += sprecuperado;
        UpdateBarraSP(sp);
    }

    public void UpdateBarraSP(float sp)
    {
        BarraSP.fillAmount = sp / SpMax;
        spText.text = sp.ToString();
    }
    #endregion

    #region Escolher target para atacar

    private void OnMouseOver() //passar com o rato por cima
    {
        if (inimigo)
        {
            if (vida > 0)
            {
                if (combatManager.Estado == EstadoBatalha.EspiritoJogador || combatManager.Estado == EstadoBatalha.AtaqueJogador)
                {
                    if (combatManager.AtaqueMágicoGuardado != null || combatManager.AtaqueFisicoGuardado != null)
                    {
                        if (combatManager.InimgoEscolhido == false)
                        {
                            spriteRenderer.color = corDano;

                        }
                    }

                }
            }

        }
    }

    private void OnMouseExit()
    {
        if (inimigo)
        {
            if (vida > 0)
                spriteRenderer.color = corOriginal;
        }
    }

    public void OnMouseDown() //escolher inimigo
    {
        if (inimigo && vida > 0)
        {
            if (combatManager.Estado == EstadoBatalha.AtaqueJogador || combatManager.Estado == EstadoBatalha.EspiritoJogador && combatManager.AtaqueMágicoGuardado != null)
            {
                BaseStats target = combatManager.SelecionarInimigoComoTarget(this);

                if (target != null)
                {
                    spriteRenderer.color = corDano;


                    combatManager.DescriçaoCombate.text = (target.gameObject.name + " received damage.");

                    if (combatManager.Estado == EstadoBatalha.AtaqueJogador)
                    {
                        combatManager.InimgoEscolhido = true; //<-
                        Debug.Log("A");
                        StartCoroutine(combatManager.AtaqueJogador(target));
                    }

                    else if (combatManager.Estado == EstadoBatalha.EspiritoJogador && combatManager.AtaqueMágicoGuardado != null)
                    {
                        combatManager.Estado = EstadoBatalha.Esperar;

                        StartCoroutine(combatManager.AtaqueEspiritoJogador(target));
                    }
                }
            }
        }
    }
    #endregion

    public bool CheckMorte()
    {
        if (vida <= 0)
        {
            Debug.Log("Personagem morreu");
            if (BarraSP != null)
                BarraSP.gameObject.SetActive(false);

            spriteRenderer.color = corMorte;

            return true;
        }
        return false;
    }

}

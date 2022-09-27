using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

enum EstadoBatalha
{
    ComeçoBatalha, EquipaJogadorTurn, EscolherAçao, AtaqueJogador, Esperar, EspiritoJogador, EquipaInimigoTurn, AtaqueInimigo, Vitória, Derrota
}

public class CombatManager : MonoBehaviour
{
    [SerializeField] EstadoBatalha estado;

    [SerializeField] TextMeshProUGUI descriçaoCombate;

    [SerializeField] BaseStats[] baseStatsPersonagem;
    [SerializeField] BaseStats[] baseStatsInimigo;

    [SerializeField] int posicaoAtual;

    [SerializeField] bool inimgoEscolhido;

    //Ataques
    [SerializeField] GameObject botoesAtaqueInterface;

    [SerializeField] AtaqueFisico ataqueFisicoGuardado;
    [SerializeField] AtaqueMagico ataqueMágicoGuardado;
    [SerializeField] GameObject grid;
    [SerializeField] AtaqueMagicoInterface ataqueMagicoPrefab;
    [SerializeField] BaseStats personagemAtual;

    List<AtaqueMagico> listaDeAtaquesMagicos;
    [SerializeField] AtaqueMagicoInterface ataquesInstaciados;

    //Pós Combate
    [SerializeField] UIManager ui;
    [SerializeField] bool gameOverReal;
    [SerializeField] GameObject telaGameOver;
    [SerializeField] GameObject[] coisasAparecerVitoria;
    [SerializeField] GameObject[] coisasAparecerDerrota;
    [SerializeField] GameObject[] coisasParaDesaparecer;


    #region Propriedades
    internal EstadoBatalha Estado { get => estado; set => estado = value; }
    public AtaqueMagico AtaqueMágicoGuardado { get => ataqueMágicoGuardado; set => ataqueMágicoGuardado = value; }
    public TextMeshProUGUI DescriçaoCombate { get => descriçaoCombate; set => descriçaoCombate = value; }
    public BaseStats PersonagemAtual { get => personagemAtual; set => personagemAtual = value; }
    public BaseStats[] BaseStatsInimigo { get => baseStatsInimigo; set => baseStatsInimigo = value; }
    public AtaqueFisico AtaqueFisicoGuardado { get => ataqueFisicoGuardado; set => ataqueFisicoGuardado = value; }
    public bool InimgoEscolhido { get => inimgoEscolhido; set => inimgoEscolhido = value; }
    #endregion


    void Start()
    {
        botoesAtaqueInterface.gameObject.SetActive(false);
        ui = ui.GetComponent<UIManager>();
        Estado = EstadoBatalha.ComeçoBatalha;
        DescriçaoCombate.text = ("The battle will begin!");
        StartCoroutine(ComecarCombate());
    }

    /// Vez dos Jogadores

    #region Começo de Combate //Aqui tá tudo bem

    IEnumerator ComecarCombate()
    {
        Estado = EstadoBatalha.EquipaJogadorTurn;
        PersonagemAtual = baseStatsPersonagem[0];
        posicaoAtual = 1;

        yield return new WaitForSeconds(3f);

        DescriçaoCombate.text = ("It's " + PersonagemAtual.Nome + " turn.");
        botoesAtaqueInterface.gameObject.SetActive(true);

        personagemAtual.Seta.gameObject.SetActive(true);
        Estado = EstadoBatalha.EscolherAçao;
        Debug.Log("Escolher Ação");
    }

    #endregion 

    //Ataque Normal

    #region Botão de ataque jogador, Guardar Ataque
    public void BotãoAtaque()
    {
        if (Estado != EstadoBatalha.EscolherAçao && Estado != EstadoBatalha.EspiritoJogador)
            return;
        else
        {
            AtaqueFisicoGuardado = null;
            AtaqueMágicoGuardado = null;

            if (AtaqueFisicoGuardado == null)
            {
                AtaqueFisicoGuardado = PersonagemAtual.AtaqFisico;
                DescriçaoCombate.text = ("Choose the enemy to attack.");
                Estado = EstadoBatalha.AtaqueJogador; //permite ao jogador escolher que inimigo atacar
            }
        }
    }

    #endregion

    #region Ataque Jogador
    public IEnumerator AtaqueJogador(BaseStats baseStats)
    {
        estado = EstadoBatalha.Esperar;

        if (baseStats != null)
        {
            inimgoEscolhido = true;

            baseStats.ReceberDano(AtaqueFisicoGuardado.Dano);

            if (baseStats.CheckMorte() == true && CheckMorteInimigos() == true)
            {
                Estado = EstadoBatalha.Vitória;
                StartCoroutine(FimCombate());
            }
            else
            {
                yield return new WaitForSeconds(1f);
                botoesAtaqueInterface.gameObject.SetActive(false);
                //ProximoPersonagemPlayer();
                StartCoroutine(CoroutineProximoJogador());
            }
        }
        else
            yield return null;

    }
    #endregion

    //Ataque de Espirito

    #region Botao  de espirito jogador
    public void BotaoEspirito()
    {
        if (PersonagemAtual.Sp > 0)
        {
            if (Estado != EstadoBatalha.EscolherAçao && Estado != EstadoBatalha.AtaqueJogador)
                return;
            else
            {
                AtaqueFisicoGuardado = null;
                AtaqueMágicoGuardado = null;

                Estado = EstadoBatalha.EspiritoJogador;
                DescriçaoCombate.text = ("Choose the spirit power");

                grid.gameObject.SetActive(true);

                for (int i = 0; i < grid.transform.childCount; i++)
                {
                    Destroy(grid.transform.GetChild(i).gameObject);
                }

                foreach (var item in personagemAtual.AtaqMagicos)
                {
                    AtaqueMagicoInterface ataqueMagicoInstanciado = Instantiate(ataqueMagicoPrefab, grid.transform);
                    ataqueMagicoInstanciado.MudarIconENome(item);
                    ataquesInstaciados = ataqueMagicoInstanciado;
                }
            }
        }
        else
            return;
    }
    #endregion

    #region Ataque de Espirito Jogador
    public IEnumerator AtaqueEspiritoJogador(BaseStats baseStats)
    {
        if (baseStats != null)
        {
            if (ataqueMágicoGuardado.TipoConsumo == Consumo.SP)
                PersonagemAtual.ConsumoSP(AtaqueMágicoGuardado.Custo);

            else if (ataqueMágicoGuardado.TipoConsumo == Consumo.HP)
                PersonagemAtual.ConsumoHP(AtaqueMágicoGuardado.Custo);

            baseStats.ReceberDano(AtaqueMágicoGuardado.Dano);

            if (baseStats.CheckMorte() == true && CheckMorteInimigos() == true)
            {
                Estado = EstadoBatalha.Vitória;
                StartCoroutine(FimCombate());
            }
            else
            {
                yield return new WaitForSeconds(1f);
                botoesAtaqueInterface.gameObject.SetActive(false);
                StartCoroutine(CoroutineProximoJogador());
                //ProximoPersonagemPlayer();
            }
        }
        else
            yield return null;
    }

    #endregion

    #region Ataque de Espirito Jogador a Todos Inimigos
    public IEnumerator AtaqueEspiritoATodosInimigos()
    {
        if (ataqueMágicoGuardado.TipoConsumo == Consumo.SP)
            PersonagemAtual.ConsumoSP(AtaqueMágicoGuardado.Custo);

        else if (ataqueMágicoGuardado.TipoConsumo == Consumo.HP)
            PersonagemAtual.ConsumoHP(AtaqueMágicoGuardado.Custo);

        foreach (var item in BaseStatsInimigo)
        {
            if (item != null)
            {
                item.ReceberDano(AtaqueMágicoGuardado.Dano);
                item.CheckMorte();
            }
            else
                yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        if (CheckMorteInimigos() == true)
        {
            Estado = EstadoBatalha.Vitória;
            StartCoroutine(FimCombate());
        }
        else
        {
            botoesAtaqueInterface.gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);
            StartCoroutine(CoroutineProximoJogador());
            //ProximoPersonagemPlayer();
        }


    }
    #endregion

   //Escolher Inimigo

    #region Selecionar Inimigo para atacar
    public BaseStats SelecionarInimigoComoTarget(BaseStats inimgoSelecionado)
    {
        BaseStats target = inimgoSelecionado;
        if (target.Vida > 0)
        {
            inimgoEscolhido = true;
            return target;
        }
        return null;
    }

    #endregion

    //Restantes Botões

    #region Botao de cura jogador (futuro será consumo de item)
    public void BotãoItemCura()
    {
        if (Estado != EstadoBatalha.EscolherAçao && Estado != EstadoBatalha.AtaqueJogador && Estado != EstadoBatalha.EspiritoJogador)
            return;
        else
        {
            AtaqueFisicoGuardado = null;
            AtaqueMágicoGuardado = null;
            PersonagemAtual.RegenerarVida(personagemAtual.VidaMax / 5);
            DescriçaoCombate.text = (PersonagemAtual.Nome + " used healing item.");
            botoesAtaqueInterface.gameObject.SetActive(false);
            StartCoroutine(CoroutineProximoJogador());
            //ProximoPersonagemPlayer();
        }
    }
    #endregion

    #region Botao de defesa jogador
    public void BotaoDefesa()
    {
        if (Estado != EstadoBatalha.EscolherAçao && Estado != EstadoBatalha.AtaqueJogador && Estado != EstadoBatalha.EspiritoJogador)
            return;
        else
        {
            AtaqueFisicoGuardado = null;
            AtaqueMágicoGuardado = null;

            PersonagemAtual.DefesaAtiva = true;
            DescriçaoCombate.text = (PersonagemAtual.Nome + " is in defensive pose.");
            botoesAtaqueInterface.gameObject.SetActive(false);
            StartCoroutine(CoroutineProximoJogador());
            //ProximoPersonagemPlayer();
        }
    }
    #endregion

    #region Proximo a jogar equipa dos Players
    void ProximoPersonagemPlayer()
    {
        //inimgoEscolhido = false;
        botoesAtaqueInterface.gameObject.SetActive(false); //<-
        personagemAtual.Seta.gameObject.SetActive(false); //<-

        if (posicaoAtual < baseStatsPersonagem.Length)
        {
            PersonagemAtual = baseStatsPersonagem[posicaoAtual];
            posicaoAtual++;

            if(PersonagemAtual.Vida > 0)
            {
                AtaqueFisicoGuardado = null; //<-
                AtaqueMágicoGuardado = null;//<-
                DescriçaoCombate.text = ("It's " + PersonagemAtual.Nome + " turn!");
                personagemAtual.Seta.gameObject.SetActive(true);
                Estado = EstadoBatalha.EscolherAçao;
                botoesAtaqueInterface.gameObject.SetActive(true);
            }
            else
                ProximoPersonagemPlayer();
        }
        else
        {
            Estado = EstadoBatalha.EquipaInimigoTurn;

            posicaoAtual = 0;
            PersonagemAtual = baseStatsInimigo [posicaoAtual];

            AtaqueFisicoGuardado = null;//<-
            AtaqueMágicoGuardado = null;//<-
            DescriçaoCombate.text = ("Enemy turn!");
            botoesAtaqueInterface.gameObject.SetActive(false);
            //AtaqueInimigoNaFé(); //inicio do tuno do inimigo~//<-
            PróximoInimigo();
        }
    }
    #endregion

    IEnumerator CoroutineProximoJogador()
    {
        grid.gameObject.SetActive(false);
        personagemAtual.Seta.gameObject.SetActive(false);

        AtaqueFisicoGuardado = null;
        AtaqueMágicoGuardado = null;

        yield return new WaitForSeconds(1.5f);
        ProximoPersonagemPlayer();
    }

    ///Vez dos Inimigos
    
    #region Guardar Ataque Inimigo
    void AtaqueInimigoNaFé()
    {
        Estado = EstadoBatalha.AtaqueInimigo;

        Debug.Log("Inimigo atacou player");

        if (AtaqueMágicoGuardado == null)
        {
            int ataqueRandom = Random.Range(0, PersonagemAtual.AtaqMagicos.Length);
            AtaqueMagico ataque = PersonagemAtual.AtaqMagicos[ataqueRandom];
            AtaqueMágicoGuardado = ataque;
            Debug.Log(ataque.name);

            if(ataqueMágicoGuardado.TipoRange == Range.One)
            {
                SelecionarPlayerComoTarget();
            }
            else
            {
                StartCoroutine(AtaqueInimigoTodosPlayers());
            }
        }
    }
    #endregion

    #region Selecionar Player para atacar
    BaseStats SelecionarPlayerComoTarget()
    {
        if (CheckMorteJogadores() == false)
        {
            int posicaoTargetRandom = Random.Range(0, baseStatsPersonagem.Length);
            BaseStats target = baseStatsPersonagem[posicaoTargetRandom];
            if (target.Vida > 0)
            {
                Debug.Log("Inimigo escolheu player");
                StartCoroutine(AtaqueInimigo1Player(target));
                return target;
            }
            else
            {
                SelecionarPlayerComoTarget();
                return null;
            }
        }
        else
        {
            Estado = EstadoBatalha.Derrota;
            StartCoroutine(FimCombate());
            return null;
        }
    }
    #endregion

    #region Ataque Inimigo a 1 Jogador
    IEnumerator AtaqueInimigo1Player(BaseStats baseStats)
    {
        yield return new WaitForSeconds(2f);

        if (baseStats != null)
        {
            if (baseStats.DefesaAtiva)
            {
                baseStats.ReceberDano(ataqueMágicoGuardado.Dano / 4);
                DescriçaoCombate.text = (baseStats.Nome + " defended");

                yield return new WaitForSeconds(1f);

                baseStats.DefesaAtiva = false;
                Debug.Log("Defesa desativada");
            }
            else
            {
                baseStats.ReceberDano(ataqueMágicoGuardado.Dano);
                DescriçaoCombate.text = (personagemAtual + " attacked " + baseStats.Nome);
            }

            yield return new WaitForSeconds(2f);

            if (baseStats.CheckMorte() == true && CheckMorteJogadores() == true)
            {
                Estado = EstadoBatalha.Derrota;
                StartCoroutine(FimCombate());
            }
            else
                PróximoInimigo();
        }
        else
        {
            yield return null;
        }
    }
    #endregion

    #region Ataque Inimigo a Todos Jogadores
    IEnumerator AtaqueInimigoTodosPlayers()
    {
        yield return new WaitForSeconds(1f);

        foreach (var item in baseStatsPersonagem)
        {
            if (item != null)
            {
                if(item.DefesaAtiva)
                {
                    item.ReceberDano(AtaqueMágicoGuardado.Dano / 4);
                    item.DefesaAtiva = false;
                }
                else
                {
                    item.ReceberDano(AtaqueMágicoGuardado.Dano);
                }
                item.CheckMorte(); 
            }
            else
                yield return null;
        }

        yield return new WaitForSeconds(1f);

        descriçaoCombate.text = (personagemAtual + " attacked all party members");

        if (CheckMorteJogadores() == true)
        {
            Estado = EstadoBatalha.Derrota;
            StartCoroutine(FimCombate());
        }
        else
        {
            PróximoInimigo();
        }
    }
    #endregion

    #region Proximo inimigo a jogar
    void PróximoInimigo()
    {
        personagemAtual.Seta.gameObject.SetActive(false);

        if (posicaoAtual < BaseStatsInimigo.Length )
        {
            PersonagemAtual = BaseStatsInimigo[posicaoAtual];
            posicaoAtual++;

            AtaqueMágicoGuardado = null;

            if(PersonagemAtual.Vida > 0)
            {
                DescriçaoCombate.text = ("It's " + PersonagemAtual.Nome + " turn!");
                personagemAtual.Seta.gameObject.SetActive(true);
                AtaqueInimigoNaFé();
            }
            else
                PróximoInimigo();
        }

        else
        {
            Estado = EstadoBatalha.EquipaJogadorTurn;

            posicaoAtual = 0;
            PersonagemAtual = baseStatsPersonagem[posicaoAtual];

            AtaqueMágicoGuardado = null;
            botoesAtaqueInterface.gameObject.SetActive(true);
            DescriçaoCombate.text = ("Player's turn!");
            Estado = EstadoBatalha.EscolherAçao;
            //StartCoroutine(CoroutineProximoJogador());
            ProximoPersonagemPlayer();
        }

    }
    #endregion


    ///Check Mortes e Fim de Combate

    #region Check Inimigos Mortos 

    bool CheckMorteInimigos()
    {
        foreach (BaseStats item in BaseStatsInimigo)
        {
            if (item.CheckMorte() == false)
            {
                return false;
            }
        }
        return true;
    }

    #endregion

    #region Check jogadores Mortos 
    bool CheckMorteJogadores()
    {
        foreach (var item in baseStatsPersonagem)
        {
            if (item.CheckMorte() == false)
            {
                return false;
            }
        }
        return true;
    }
    #endregion

    #region Fim do Combate
    IEnumerator FimCombate()
    {
        if (Estado == EstadoBatalha.Vitória)
        {
            DescriçaoCombate.text = ("Victory!");
            personagemAtual.Seta.gameObject.SetActive(false);

            yield return new WaitForSeconds(1.5f);

            foreach (var item in coisasAparecerVitoria)
                item.gameObject.SetActive(true);
            foreach (var item in coisasParaDesaparecer)
                item.gameObject.SetActive(false);
        }
        else if (Estado == EstadoBatalha.Derrota)
        {
            DescriçaoCombate.text = ("Defeat!");
            personagemAtual.Seta.gameObject.SetActive(false);

            yield return new WaitForSeconds(1.5f);

            if(gameOverReal)
            {
                telaGameOver.gameObject.SetActive(true);
            }
            else
            {
                foreach (var item in coisasAparecerDerrota)
                    item.gameObject.SetActive(true);
                foreach (var item in coisasParaDesaparecer)
                    item.gameObject.SetActive(false);
            }
        }
    }
    #endregion

    #region GameOver e Recomeco de Batalha
    public void TelaInicialBotao()
    {
        ui.ProximaSceneComFade("MenuInicial");
    }

    public void RestartCombateBotao()
    {
        foreach (var item in baseStatsPersonagem)
        {
            item.BarraSP.gameObject.SetActive(true);
            item.SpriteRenderer.color = item.CorOriginal;
            item.Vida = item.VidaMax;
            item.Sp = item.SpMax;
            item.UpdateBarraVida(item.VidaMax);
            item.UpdateBarraSP(item.VidaMax);
        }
        foreach (var item in BaseStatsInimigo)
        {
            item.SpriteRenderer.color = item.CorOriginal;
            item.Vida = item.VidaMax;
            item.UpdateBarraVida(item.VidaMax);
        }

        telaGameOver.gameObject.SetActive(false);

        botoesAtaqueInterface.gameObject.SetActive(true);

        Estado = EstadoBatalha.ComeçoBatalha;
        DescriçaoCombate.text = ("The battle will begin!");
        StartCoroutine(ComecarCombate());
    }
    #endregion
}

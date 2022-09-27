using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

enum EstadoBatalha
{
    Come�oBatalha, EquipaJogadorTurn, EscolherA�ao, AtaqueJogador, Esperar, EspiritoJogador, EquipaInimigoTurn, AtaqueInimigo, Vit�ria, Derrota
}

public class CombatManager : MonoBehaviour
{
    [SerializeField] EstadoBatalha estado;

    [SerializeField] TextMeshProUGUI descri�aoCombate;

    [SerializeField] BaseStats[] baseStatsPersonagem;
    [SerializeField] BaseStats[] baseStatsInimigo;

    [SerializeField] int posicaoAtual;

    [SerializeField] bool inimgoEscolhido;

    //Ataques
    [SerializeField] GameObject botoesAtaqueInterface;

    [SerializeField] AtaqueFisico ataqueFisicoGuardado;
    [SerializeField] AtaqueMagico ataqueM�gicoGuardado;
    [SerializeField] GameObject grid;
    [SerializeField] AtaqueMagicoInterface ataqueMagicoPrefab;
    [SerializeField] BaseStats personagemAtual;

    List<AtaqueMagico> listaDeAtaquesMagicos;
    [SerializeField] AtaqueMagicoInterface ataquesInstaciados;

    //P�s Combate
    [SerializeField] UIManager ui;
    [SerializeField] bool gameOverReal;
    [SerializeField] GameObject telaGameOver;
    [SerializeField] GameObject[] coisasAparecerVitoria;
    [SerializeField] GameObject[] coisasAparecerDerrota;
    [SerializeField] GameObject[] coisasParaDesaparecer;


    #region Propriedades
    internal EstadoBatalha Estado { get => estado; set => estado = value; }
    public AtaqueMagico AtaqueM�gicoGuardado { get => ataqueM�gicoGuardado; set => ataqueM�gicoGuardado = value; }
    public TextMeshProUGUI Descri�aoCombate { get => descri�aoCombate; set => descri�aoCombate = value; }
    public BaseStats PersonagemAtual { get => personagemAtual; set => personagemAtual = value; }
    public BaseStats[] BaseStatsInimigo { get => baseStatsInimigo; set => baseStatsInimigo = value; }
    public AtaqueFisico AtaqueFisicoGuardado { get => ataqueFisicoGuardado; set => ataqueFisicoGuardado = value; }
    public bool InimgoEscolhido { get => inimgoEscolhido; set => inimgoEscolhido = value; }
    #endregion


    void Start()
    {
        botoesAtaqueInterface.gameObject.SetActive(false);
        ui = ui.GetComponent<UIManager>();
        Estado = EstadoBatalha.Come�oBatalha;
        Descri�aoCombate.text = ("The battle will begin!");
        StartCoroutine(ComecarCombate());
    }

    /// Vez dos Jogadores

    #region Come�o de Combate //Aqui t� tudo bem

    IEnumerator ComecarCombate()
    {
        Estado = EstadoBatalha.EquipaJogadorTurn;
        PersonagemAtual = baseStatsPersonagem[0];
        posicaoAtual = 1;

        yield return new WaitForSeconds(3f);

        Descri�aoCombate.text = ("It's " + PersonagemAtual.Nome + " turn.");
        botoesAtaqueInterface.gameObject.SetActive(true);

        personagemAtual.Seta.gameObject.SetActive(true);
        Estado = EstadoBatalha.EscolherA�ao;
        Debug.Log("Escolher A��o");
    }

    #endregion 

    //Ataque Normal

    #region Bot�o de ataque jogador, Guardar Ataque
    public void Bot�oAtaque()
    {
        if (Estado != EstadoBatalha.EscolherA�ao && Estado != EstadoBatalha.EspiritoJogador)
            return;
        else
        {
            AtaqueFisicoGuardado = null;
            AtaqueM�gicoGuardado = null;

            if (AtaqueFisicoGuardado == null)
            {
                AtaqueFisicoGuardado = PersonagemAtual.AtaqFisico;
                Descri�aoCombate.text = ("Choose the enemy to attack.");
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
                Estado = EstadoBatalha.Vit�ria;
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
            if (Estado != EstadoBatalha.EscolherA�ao && Estado != EstadoBatalha.AtaqueJogador)
                return;
            else
            {
                AtaqueFisicoGuardado = null;
                AtaqueM�gicoGuardado = null;

                Estado = EstadoBatalha.EspiritoJogador;
                Descri�aoCombate.text = ("Choose the spirit power");

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
            if (ataqueM�gicoGuardado.TipoConsumo == Consumo.SP)
                PersonagemAtual.ConsumoSP(AtaqueM�gicoGuardado.Custo);

            else if (ataqueM�gicoGuardado.TipoConsumo == Consumo.HP)
                PersonagemAtual.ConsumoHP(AtaqueM�gicoGuardado.Custo);

            baseStats.ReceberDano(AtaqueM�gicoGuardado.Dano);

            if (baseStats.CheckMorte() == true && CheckMorteInimigos() == true)
            {
                Estado = EstadoBatalha.Vit�ria;
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
        if (ataqueM�gicoGuardado.TipoConsumo == Consumo.SP)
            PersonagemAtual.ConsumoSP(AtaqueM�gicoGuardado.Custo);

        else if (ataqueM�gicoGuardado.TipoConsumo == Consumo.HP)
            PersonagemAtual.ConsumoHP(AtaqueM�gicoGuardado.Custo);

        foreach (var item in BaseStatsInimigo)
        {
            if (item != null)
            {
                item.ReceberDano(AtaqueM�gicoGuardado.Dano);
                item.CheckMorte();
            }
            else
                yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        if (CheckMorteInimigos() == true)
        {
            Estado = EstadoBatalha.Vit�ria;
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

    //Restantes Bot�es

    #region Botao de cura jogador (futuro ser� consumo de item)
    public void Bot�oItemCura()
    {
        if (Estado != EstadoBatalha.EscolherA�ao && Estado != EstadoBatalha.AtaqueJogador && Estado != EstadoBatalha.EspiritoJogador)
            return;
        else
        {
            AtaqueFisicoGuardado = null;
            AtaqueM�gicoGuardado = null;
            PersonagemAtual.RegenerarVida(personagemAtual.VidaMax / 5);
            Descri�aoCombate.text = (PersonagemAtual.Nome + " used healing item.");
            botoesAtaqueInterface.gameObject.SetActive(false);
            StartCoroutine(CoroutineProximoJogador());
            //ProximoPersonagemPlayer();
        }
    }
    #endregion

    #region Botao de defesa jogador
    public void BotaoDefesa()
    {
        if (Estado != EstadoBatalha.EscolherA�ao && Estado != EstadoBatalha.AtaqueJogador && Estado != EstadoBatalha.EspiritoJogador)
            return;
        else
        {
            AtaqueFisicoGuardado = null;
            AtaqueM�gicoGuardado = null;

            PersonagemAtual.DefesaAtiva = true;
            Descri�aoCombate.text = (PersonagemAtual.Nome + " is in defensive pose.");
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
                AtaqueM�gicoGuardado = null;//<-
                Descri�aoCombate.text = ("It's " + PersonagemAtual.Nome + " turn!");
                personagemAtual.Seta.gameObject.SetActive(true);
                Estado = EstadoBatalha.EscolherA�ao;
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
            AtaqueM�gicoGuardado = null;//<-
            Descri�aoCombate.text = ("Enemy turn!");
            botoesAtaqueInterface.gameObject.SetActive(false);
            //AtaqueInimigoNaF�(); //inicio do tuno do inimigo~//<-
            Pr�ximoInimigo();
        }
    }
    #endregion

    IEnumerator CoroutineProximoJogador()
    {
        grid.gameObject.SetActive(false);
        personagemAtual.Seta.gameObject.SetActive(false);

        AtaqueFisicoGuardado = null;
        AtaqueM�gicoGuardado = null;

        yield return new WaitForSeconds(1.5f);
        ProximoPersonagemPlayer();
    }

    ///Vez dos Inimigos
    
    #region Guardar Ataque Inimigo
    void AtaqueInimigoNaF�()
    {
        Estado = EstadoBatalha.AtaqueInimigo;

        Debug.Log("Inimigo atacou player");

        if (AtaqueM�gicoGuardado == null)
        {
            int ataqueRandom = Random.Range(0, PersonagemAtual.AtaqMagicos.Length);
            AtaqueMagico ataque = PersonagemAtual.AtaqMagicos[ataqueRandom];
            AtaqueM�gicoGuardado = ataque;
            Debug.Log(ataque.name);

            if(ataqueM�gicoGuardado.TipoRange == Range.One)
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
                baseStats.ReceberDano(ataqueM�gicoGuardado.Dano / 4);
                Descri�aoCombate.text = (baseStats.Nome + " defended");

                yield return new WaitForSeconds(1f);

                baseStats.DefesaAtiva = false;
                Debug.Log("Defesa desativada");
            }
            else
            {
                baseStats.ReceberDano(ataqueM�gicoGuardado.Dano);
                Descri�aoCombate.text = (personagemAtual + " attacked " + baseStats.Nome);
            }

            yield return new WaitForSeconds(2f);

            if (baseStats.CheckMorte() == true && CheckMorteJogadores() == true)
            {
                Estado = EstadoBatalha.Derrota;
                StartCoroutine(FimCombate());
            }
            else
                Pr�ximoInimigo();
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
                    item.ReceberDano(AtaqueM�gicoGuardado.Dano / 4);
                    item.DefesaAtiva = false;
                }
                else
                {
                    item.ReceberDano(AtaqueM�gicoGuardado.Dano);
                }
                item.CheckMorte(); 
            }
            else
                yield return null;
        }

        yield return new WaitForSeconds(1f);

        descri�aoCombate.text = (personagemAtual + " attacked all party members");

        if (CheckMorteJogadores() == true)
        {
            Estado = EstadoBatalha.Derrota;
            StartCoroutine(FimCombate());
        }
        else
        {
            Pr�ximoInimigo();
        }
    }
    #endregion

    #region Proximo inimigo a jogar
    void Pr�ximoInimigo()
    {
        personagemAtual.Seta.gameObject.SetActive(false);

        if (posicaoAtual < BaseStatsInimigo.Length )
        {
            PersonagemAtual = BaseStatsInimigo[posicaoAtual];
            posicaoAtual++;

            AtaqueM�gicoGuardado = null;

            if(PersonagemAtual.Vida > 0)
            {
                Descri�aoCombate.text = ("It's " + PersonagemAtual.Nome + " turn!");
                personagemAtual.Seta.gameObject.SetActive(true);
                AtaqueInimigoNaF�();
            }
            else
                Pr�ximoInimigo();
        }

        else
        {
            Estado = EstadoBatalha.EquipaJogadorTurn;

            posicaoAtual = 0;
            PersonagemAtual = baseStatsPersonagem[posicaoAtual];

            AtaqueM�gicoGuardado = null;
            botoesAtaqueInterface.gameObject.SetActive(true);
            Descri�aoCombate.text = ("Player's turn!");
            Estado = EstadoBatalha.EscolherA�ao;
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
        if (Estado == EstadoBatalha.Vit�ria)
        {
            Descri�aoCombate.text = ("Victory!");
            personagemAtual.Seta.gameObject.SetActive(false);

            yield return new WaitForSeconds(1.5f);

            foreach (var item in coisasAparecerVitoria)
                item.gameObject.SetActive(true);
            foreach (var item in coisasParaDesaparecer)
                item.gameObject.SetActive(false);
        }
        else if (Estado == EstadoBatalha.Derrota)
        {
            Descri�aoCombate.text = ("Defeat!");
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

        Estado = EstadoBatalha.Come�oBatalha;
        Descri�aoCombate.text = ("The battle will begin!");
        StartCoroutine(ComecarCombate());
    }
    #endregion
}

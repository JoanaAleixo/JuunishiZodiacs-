using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AtaqueMagicoInterface : MonoBehaviour
{
    [SerializeField] CombatManager combatManager;
    [SerializeField] Transform grid;
    [SerializeField] Image iconTipoAtaque;
    [SerializeField] TextMeshProUGUI nomeAtaque;
    AtaqueMagico ataqueGuardado;

    private void Start()
    {
        combatManager = GameObject.Find("CombatManager").GetComponent<CombatManager>();
    }

    public void MudarIconENome(AtaqueMagico ataqueMagico)
    {
        iconTipoAtaque.sprite = ataqueMagico.Icon;
        nomeAtaque.text = ataqueMagico.Nome;
        ataqueGuardado = ataqueMagico;
    }

    public void GuardarAtaqueBotao()
    {
        if (combatManager.Estado == EstadoBatalha.EspiritoJogador)
        {
            if (combatManager.AtaqueM�gicoGuardado == null)
            {
                combatManager.AtaqueM�gicoGuardado = ataqueGuardado; //guardar o ataque

                #region Ataque consome SP

                if (combatManager.AtaqueM�gicoGuardado.TipoConsumo == Consumo.SP)
                {
                    if (combatManager.AtaqueM�gicoGuardado.Custo <= combatManager.PersonagemAtual.Sp) //verificar se SP � suficiente
                    {
                        CheckRangeAtaque();
                    }
                    else
                    {
                        combatManager.Descri�aoCombate.text = ("SP insuficiente");
                        RetrocederAtaque();
                    }
                }
                #endregion

                #region Ataque consome HP

                else if (combatManager.AtaqueM�gicoGuardado.TipoConsumo == Consumo.HP)
                {
                    if (combatManager.AtaqueM�gicoGuardado.Custo <= combatManager.PersonagemAtual.Vida)
                    {
                        CheckRangeAtaque();
                    }
                    else
                    {
                        combatManager.Descri�aoCombate.text = ("HP insuficiente");
                        RetrocederAtaque();
                    }
                }
                #endregion

            }
        }
    }

    void RetrocederAtaque()
    {
        combatManager.AtaqueM�gicoGuardado = null;
    }

    void CheckRangeAtaque()
    {
        if (combatManager.AtaqueM�gicoGuardado.TipoRange == Range.One) //verificar se o ataque � s� para 1 inimigo
            combatManager.Descri�aoCombate.text = ("Escolha o inimigo a atacar:");
        else
            StartCoroutine(combatManager.AtaqueEspiritoATodosInimigos());
    }

    //instanciar ataques do perosnagem na grid
    //aceder ao combate manager para guardar ataque ao clicar
    //analisar o custo se de SP se de HP (inclusive colocar esta info no nome do ataque)
    //analisar se o ataque afeta 1 inimigo ou todos
    //executar o ataque


    //adaptar algo deste script para o inimigo para ele conseguir tamb�m atacar todos os players ou s� 1
    //mudar ordem do ataque do inimigo - escolher atacar antes de escolher jogador morrer~~~ 


    //Necessario chamar uma ambulancia, e marcar um eternamento para o final do curso, obrigado
        //by: Rafael
}



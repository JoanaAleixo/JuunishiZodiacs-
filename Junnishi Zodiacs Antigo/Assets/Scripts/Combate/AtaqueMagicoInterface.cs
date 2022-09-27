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
            if (combatManager.AtaqueMágicoGuardado == null)
            {
                combatManager.AtaqueMágicoGuardado = ataqueGuardado; //guardar o ataque

                #region Ataque consome SP

                if (combatManager.AtaqueMágicoGuardado.TipoConsumo == Consumo.SP)
                {
                    if (combatManager.AtaqueMágicoGuardado.Custo <= combatManager.PersonagemAtual.Sp) //verificar se SP é suficiente
                    {
                        CheckRangeAtaque();
                    }
                    else
                    {
                        combatManager.DescriçaoCombate.text = ("SP insuficiente");
                        RetrocederAtaque();
                    }
                }
                #endregion

                #region Ataque consome HP

                else if (combatManager.AtaqueMágicoGuardado.TipoConsumo == Consumo.HP)
                {
                    if (combatManager.AtaqueMágicoGuardado.Custo <= combatManager.PersonagemAtual.Vida)
                    {
                        CheckRangeAtaque();
                    }
                    else
                    {
                        combatManager.DescriçaoCombate.text = ("HP insuficiente");
                        RetrocederAtaque();
                    }
                }
                #endregion

            }
        }
    }

    void RetrocederAtaque()
    {
        combatManager.AtaqueMágicoGuardado = null;
    }

    void CheckRangeAtaque()
    {
        if (combatManager.AtaqueMágicoGuardado.TipoRange == Range.One) //verificar se o ataque é só para 1 inimigo
            combatManager.DescriçaoCombate.text = ("Escolha o inimigo a atacar:");
        else
            StartCoroutine(combatManager.AtaqueEspiritoATodosInimigos());
    }

    //instanciar ataques do perosnagem na grid
    //aceder ao combate manager para guardar ataque ao clicar
    //analisar o custo se de SP se de HP (inclusive colocar esta info no nome do ataque)
    //analisar se o ataque afeta 1 inimigo ou todos
    //executar o ataque


    //adaptar algo deste script para o inimigo para ele conseguir também atacar todos os players ou só 1
    //mudar ordem do ataque do inimigo - escolher atacar antes de escolher jogador morrer~~~ 


    //Necessario chamar uma ambulancia, e marcar um eternamento para o final do curso, obrigado
        //by: Rafael
}



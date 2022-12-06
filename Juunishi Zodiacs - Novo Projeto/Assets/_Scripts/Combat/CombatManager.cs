using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

enum BATTLESTATE
{
    StartBattle,
    PlayerTurn, 
    SelectingTarget,
    ExecuteAbility,
    EnemyTurn, 
    Victory, 
    Defeat
}

[System.Serializable]
public struct SelectedModifiers
{
    [SerializeReference] Modifiers modifierToExecute;
    [SerializeField] GameObject[] targets;

    public SelectedModifiers(Modifiers newMod, GameObject[] newTargets)
    {
        modifierToExecute = newMod;
        targets = new GameObject[newTargets.Length];
        for (int i = 0; i < newTargets.Length; i++)
        {
            targets[i] = newTargets[i];
        }
    }

    public Modifiers ModifierToExecute { get => modifierToExecute;}
    public GameObject[] Targets { get => targets;}
}

public class CombatManager : MonoBehaviour
{
    public static CombatManager combatInstance;
    [SerializeField] CombatUiManager uIManager;
    [SerializeField] BATTLESTATE _curState;
    [Header("Players")]
    [SerializeField] PlayableCaracter[] _caracters = new PlayableCaracter[3];
    [SerializeField] Enemy[] _enemies;
    [SerializeField] List<Ability> _actions;
    [SerializeField] List<SelectedModifiers> _modifiers;
    [SerializeField] int _selectedCaracter;
    [SerializeField] GameObject _actionMenu;
    [Header("TemporaryStuff")]
    [SerializeField] int tempIndex;
    [SerializeReference] Modifiers[] temporaryMods;
    [SerializeField] GameObject[] temporaryTargets;
    

    public int SelectedCaracter { get => _selectedCaracter; set { ChangeSelectedCaracter(_selectedCaracter, value);} }

    public PlayableCaracter[] Caracters { get => _caracters; set => _caracters = value; }
    internal BATTLESTATE CurState { get => _curState; set => _curState = value; }

    #region Awake + Start

    private void Awake()
    {
        if(combatInstance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            combatInstance = this;
        }
    }
    void Start()
    {
        uIManager = CombatUiManager.uiInstance;
        CurState = BATTLESTATE.StartBattle;
        temporaryTargets = null;
    }

    #endregion

    #region Update + ChangeState

    void Update()
    {
        switch (CurState)
        {
            case BATTLESTATE.StartBattle:
                ChangeState(BATTLESTATE.PlayerTurn);
                break;
            case BATTLESTATE.PlayerTurn:
                CheckForAbilities();
                break;
            case BATTLESTATE.SelectingTarget:
                TargetAbility();
                break;
            case BATTLESTATE.ExecuteAbility:
                break;
            case BATTLESTATE.EnemyTurn:
                break;
            case BATTLESTATE.Victory:
                break;
            case BATTLESTATE.Defeat:
                break;
        }
    }

    void ChangeState(BATTLESTATE _newState)
    {
        CurState = _newState;
        switch (CurState)
        {
            case BATTLESTATE.StartBattle:
                ChangeState(BATTLESTATE.PlayerTurn);
                break;
            case BATTLESTATE.PlayerTurn:
                uIManager.EnableCaracterSelection();
                uIManager.UnlockCaracterSelection();
                break;
            case BATTLESTATE.SelectingTarget:
                uIManager.LockCaracterSelection();
                uIManager.DisableCaracterSelection();
                break;
            case BATTLESTATE.ExecuteAbility:
                ExecuteModifiers();
                break;
            case BATTLESTATE.EnemyTurn:
                uIManager.LockCaracterSelection();
                break;
            case BATTLESTATE.Victory:
                break;
            case BATTLESTATE.Defeat:
                break;
        }
    }
    #endregion

    #region CaracterSelection

    public void SelectCaracter(int caracterNum)
    {
        SelectedCaracter = caracterNum;
    }

    private void ChangeSelectedCaracter(int oldValue, int newValue)
    {
        _selectedCaracter = newValue;
        uIManager.OpenActionMenu();
    }

    #endregion

    #region AbilitySelection

    public void SelectAbility(int _abilityNumb)   // Quando um botão de habilidade é clicado
    {

        if (_abilityNumb >= 0 && _abilityNumb <= 2)
        {
            _actions.Add(_caracters[SelectedCaracter].MyCaracter.Abilities[_abilityNumb]);
            temporaryMods = _caracters[SelectedCaracter].MyCaracter.Abilities[_abilityNumb].Mods.ToArray();
            ChangeState(BATTLESTATE.SelectingTarget);
        }
        else
        {
            _actions.Add(_caracters[SelectedCaracter].MyCaracter.PhysicalAbility);
        }

        uIManager.CloseActionMenu();
        uIManager.LockSelectionButton(SelectedCaracter);
    }

    private void TargetAbility()    // Toda a lógica de seleção de targets das habilidades
    {
        if (tempIndex < temporaryMods.Length)
        {
            switch (temporaryMods[tempIndex].TargetType)
            {
                case TARGETING.self:
                        temporaryTargets = new GameObject[] { _caracters[SelectedCaracter].gameObject };
                        AddToMods(temporaryMods[tempIndex], temporaryTargets);
                    break;
                case TARGETING.multipleAlly:
                        temporaryTargets = new GameObject[_caracters.Length];
                        for (int u = 0; u < _caracters.Length; u++)
                        {
                            temporaryTargets[u] = _caracters[u].gameObject;
                        }
                        AddToMods(temporaryMods[tempIndex], temporaryTargets);
                    break;
                case TARGETING.multipleEnemy:
                        temporaryTargets = new GameObject[_enemies.Length];
                        for (int u = 0; u < _enemies.Length; u++)
                        {
                            temporaryTargets[u] = _enemies[u].gameObject;
                        }
                        AddToMods(temporaryMods[tempIndex], temporaryTargets);
                    break;
                case TARGETING.singleEnemy:
                    if (temporaryTargets != null)
                    {
                        AddToMods(temporaryMods[tempIndex], temporaryTargets);
                    }
                    else if(uIManager.TemporarySelectedTarget == null)
                    {
                        uIManager.TemporarySelectedTarget = _enemies[0];
                        uIManager.OpenEnemyTargetSelection();
                    }
                    break;
                case TARGETING.singleAlly:
                    if (temporaryTargets != null)
                    {
                        AddToMods(temporaryMods[tempIndex], temporaryTargets);
                    }else if (uIManager.TemporarySelectedTarget == null)
                    {
                        uIManager.TemporarySelectedTarget = _caracters[0];
                        uIManager.OpenAllyTargetSelection();
                    }
                    break;
            }
        }

        if(tempIndex >= temporaryMods.Length)
        {
            temporaryTargets = null;
            temporaryMods = null;
            tempIndex = 0;
            ChangeState(BATTLESTATE.ExecuteAbility);
        }
    }

    public void RecieveTarget(GameObject lockedTarget)      // Recebe qual é o target da habilidade do CombatUiManager
    {
        temporaryTargets = new GameObject[1] { lockedTarget };
    }

    private void AddToMods(Modifiers mod1, GameObject[] targets1)   // Adiciona à lista de mods os que irão ser utilizados. 
    {
        SelectedModifiers modToAdd = new SelectedModifiers(mod1, targets1);
        _modifiers.Add(modToAdd);
        temporaryTargets = null;
        tempIndex++;
    }

    private void CheckForAbilities()  //Conta quantas habilidades foram utilizadas para passar o turno.
    {
        if(_actions.Count >= 3)
        {
            ChangeState(BATTLESTATE.EnemyTurn);
        }
    }

    #endregion

    #region ExecuteAbilities

    private void ExecuteModifiers()     // Executa todos os modifiers na lista de modifiers
    {
        for (int i = 0; i < _modifiers.Count; i++)
        {
            Debug.Log("ExecuteMod");
            _modifiers[i].ModifierToExecute.ExecuteMod(_modifiers[i].Targets);
        }
        _modifiers.Clear();
        ChangeState(BATTLESTATE.PlayerTurn);
    }

    #endregion

    public void SelectTargetButton(GameObject gm)
    {
        temporaryTargets = new GameObject[1] { gm };
    }

    
}

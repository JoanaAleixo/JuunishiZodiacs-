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
    EnemyExecuteAbility,
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
    [SerializeField] int tempEnemy = 0;

    public int SelectedCaracter { get => _selectedCaracter; set { ChangeSelectedCaracter(_selectedCaracter, value);} }

    public PlayableCaracter[] Caracters { get => _caracters; set => _caracters = value; }
    internal BATTLESTATE CurState { get => _curState; set => _curState = value; }
    public Enemy[] Enemies { get => _enemies; set => _enemies = value; }

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
                ChangeState(BATTLESTATE.EnemyTurn);
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
            case BATTLESTATE.EnemyExecuteAbility:
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
                if (CheckIfRoundDone())
                {
                    _actions.Clear();
                    tempEnemy = 0;
                    ChangeState(BATTLESTATE.PlayerTurn);
                }
                else
                {
                    uIManager.LockCaracterSelection();
                    uIManager.CloseActionMenu();
                    uIManager.LockSelectionButton(SelectedCaracter);
                    Debug.Log("enemyturn");
                    Enemies[tempEnemy].ChoseAbility();
                }
                break;
            case BATTLESTATE.EnemyExecuteAbility:
                ExecuteEnemyModifiers();
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
            temporaryMods = _caracters[SelectedCaracter].MyCaracter.PhysicalAbility.Mods.ToArray();
            ChangeState(BATTLESTATE.SelectingTarget);
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
                        temporaryTargets = new GameObject[Enemies.Length];
                        for (int u = 0; u < Enemies.Length; u++)
                        {
                            temporaryTargets[u] = Enemies[u].gameObject;
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
                        uIManager.TemporarySelectedTarget = Enemies[0];
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
        if(_actions.Count >= Caracters.Length)
        {
            _actions.Clear();
            ChangeState(BATTLESTATE.EnemyTurn);
        }
    }

    #endregion

    #region ExecuteAbilities

    private void ExecuteModifiers()     // Executa todos os modifiers na lista de modifiers
    {
        for (int i = 0; i < _modifiers.Count; i++)
        {
            _modifiers[i].ModifierToExecute.ExecuteMod(_modifiers[i].Targets);
        }
        _modifiers.Clear();
        ChangeState(BATTLESTATE.PlayerTurn);
    }

    private void ExecuteEnemyModifiers()     // Executa todos os modifiers na lista de modifiers dos inimigos
    {
        for (int i = 0; i < _modifiers.Count; i++)
        {
            _modifiers[i].ModifierToExecute.ExecuteMod(_modifiers[i].Targets);
        }
        _modifiers.Clear();
        tempEnemy++;
        ChangeState(BATTLESTATE.EnemyTurn);
    }

    #endregion

    public void SelectTargetButton(GameObject gm)
    {
        temporaryTargets = new GameObject[1] { gm };
    }

    public void EnemyAbility(Ability ab)
    {
        Debug.Log(ab);
        _actions.Add(ab);
        temporaryMods = ab.Mods.ToArray();
        EnemyTargetAbility();
    }

    private void EnemyTargetAbility()    // Toda a lógica de seleção de targets das habilidades dos inimigos
    {
        if (tempIndex < temporaryMods.Length)
        {
            switch (temporaryMods[tempIndex].TargetType)
            {
                case TARGETING.self:
                    temporaryTargets = new GameObject[] { Enemies[tempEnemy].gameObject };
                    AddToMods(temporaryMods[tempIndex], temporaryTargets);
                    break;
                case TARGETING.multipleAlly:
                    temporaryTargets = new GameObject[Enemies.Length];
                    for (int u = 0; u < Enemies.Length; u++)
                    {
                        temporaryTargets[u] = Enemies[u].gameObject;
                    }
                    AddToMods(temporaryMods[tempIndex], temporaryTargets);
                    break;
                case TARGETING.multipleEnemy:
                    temporaryTargets = new GameObject[_caracters.Length];
                    for (int u = 0; u < _caracters.Length; u++)
                    {
                        temporaryTargets[u] = _caracters[u].gameObject;
                    }
                    AddToMods(temporaryMods[tempIndex], temporaryTargets);
                    break;
                case TARGETING.singleEnemy:
                    temporaryTargets = new GameObject[] { _caracters[Enemies[tempEnemy].ChoseEnemyTarget()].gameObject };
                    AddToMods(temporaryMods[tempIndex], temporaryTargets);
                    break;
                case TARGETING.singleAlly:
                    temporaryTargets = new GameObject[] { Enemies[Enemies[tempEnemy].ChoseAllyTarget()].gameObject };
                    break;
            }
        }

        if (tempIndex >= temporaryMods.Length)
        {
            temporaryTargets = null;
            temporaryMods = null;
            tempIndex = 0;
            ChangeState(BATTLESTATE.EnemyExecuteAbility);
        }
    }

    private bool CheckIfRoundDone()
    {
        if(tempEnemy >= _enemies.Length)
        {
            return true;
        }
        return false;
    }

}

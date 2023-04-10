using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using TMPro;

enum BATTLESTATE
{
    StartBattle,
    PlayerTurn, 
    SelectingTarget,
    ExecuteAbility,
    EndOfPlayerTurn,
    EnemyTurn, 
    EnemyExecuteAbility,
    EndOfEnemyTurn,
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
    [SerializeField] List<PlayableCaracter> _caracters;
    [SerializeField] List<Enemy> _enemies;
    [SerializeField] List<Ability> _actions;
    [SerializeField] List<SelectedModifiers> _modifiers;
    [SerializeField] int _selectedCaracter;
    [SerializeField] GameObject _actionMenu;
    [SerializeField] int playersOnRoundStart;
    [Header("TemporaryStuff")]
    [SerializeField] int tempIndex;
    [SerializeReference] Modifiers[] temporaryMods;
    [SerializeField] GameObject[] temporaryTargets;
    [SerializeField] int tempEnemy = 0;
    [SerializeField] GameObject FloatingDamagePre;
    

    public int SelectedCaracter { get => _selectedCaracter; set { ChangeSelectedCaracter(_selectedCaracter, value);} }

    internal BATTLESTATE CurState { get => _curState; set => _curState = value; }
    public List<PlayableCaracter> Caracters { get => _caracters; set => _caracters = value; }
    public List<Enemy> Enemies { get => _enemies; set => _enemies = value; }

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
                CheckPlayers();
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
            case BATTLESTATE.EnemyExecuteAbility:
                break;
            case BATTLESTATE.Victory:
                break;
            case BATTLESTATE.Defeat:
                break;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            LocateEnemies();
        }
    }

    void ChangeState(BATTLESTATE _newState)
    {
        CurState = _newState;
        switch (CurState)
        {
            case BATTLESTATE.StartBattle:
                CheckPlayers();
                ChangeState(BATTLESTATE.PlayerTurn);
                break;
            case BATTLESTATE.PlayerTurn:
                if(_actions.Count == 0)
                {
                    uIManager.UnlockAllSelectionButtons();
                }
                uIManager.EnableCaracterSelection();
                uIManager.UnlockCaracterSelection();
                uIManager.CloseAbilityUsedPrompt();
                break;
            case BATTLESTATE.SelectingTarget:
                uIManager.LockCaracterSelection();
                uIManager.DisableCaracterSelection();
                break;
            case BATTLESTATE.ExecuteAbility:
                uIManager.CloseAbilityInfo();
                uIManager.ShowAbilityUsedPrompt(_actions[_actions.Count-1], Caracters[SelectedCaracter]);
                ExecuteModifiers();
                break;
            case BATTLESTATE.EndOfPlayerTurn:
                CheckPlayerStatus();
                break;
            case BATTLESTATE.EnemyTurn:
                uIManager.CloseAbilityUsedPrompt();
                if (CheckIfRoundDone())
                {
                    _actions.Clear();
                    tempEnemy = 0;
                    ChangeState(BATTLESTATE.EndOfEnemyTurn);
                }
                else
                {
                    uIManager.LockCaracterSelection();
                    uIManager.CloseActionMenu();
                    Enemies[tempEnemy].ChoseAbility();
                }
                break;
            case BATTLESTATE.EnemyExecuteAbility:
                uIManager.ShowAbilityUsedPrompt(_actions[_actions.Count - 1], Enemies[tempEnemy]);
                ExecuteEnemyModifiers();
                break;
            case BATTLESTATE.EndOfEnemyTurn:
                CheckEnemyStatus();
                CheckPlayers();
                break;
            case BATTLESTATE.Victory:
                break;
            case BATTLESTATE.Defeat:
                break;
        }
    }
    #endregion

    #region CaracterSelection

    public void SelectCaracter(PlayableCaracter caracter)
    {
        SelectedCaracter = caracter.CaracterNumber;
    }

    private void ChangeSelectedCaracter(int oldValue, int newValue)
    {
        _selectedCaracter = newValue;
        uIManager.CloseSelectedCaracter(oldValue);
        uIManager.OpenActionMenu();
    }

    #endregion

    #region AbilitySelection

    public void ShowAbilityInfo(int _abilityNumb)
    {
        uIManager.OpenAbilityInfo(Caracters[SelectedCaracter].MyCaracter.Abilities[_abilityNumb]);
    }

    public void SelectAbility(int _abilityNumb)   // Quando um botão de habilidade é clicado
    {
        
        if (_abilityNumb >= 0 && _abilityNumb <= 2)
        {
            _actions.Add(Caracters[SelectedCaracter].MyCaracter.Abilities[_abilityNumb]);
            temporaryMods = Caracters[SelectedCaracter].MyCaracter.Abilities[_abilityNumb].Mods.ToArray();
            ChangeState(BATTLESTATE.SelectingTarget);
        }
        else
        {
            _actions.Add(Caracters[SelectedCaracter].MyCaracter.PhysicalAbility);
            temporaryMods = Caracters[SelectedCaracter].MyCaracter.PhysicalAbility.Mods.ToArray();
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
                        temporaryTargets = new GameObject[] { Caracters[SelectedCaracter].gameObject };
                        AddToMods(temporaryMods[tempIndex], temporaryTargets);
                        uIManager.CloseSelectedCaracter(SelectedCaracter);
                        break;
                case TARGETING.multipleAlly:
                        temporaryTargets = new GameObject[Caracters.Count];
                        for (int u = 0; u < Caracters.Count; u++)
                        {
                            temporaryTargets[u] = Caracters[u].gameObject;
                        }
                        AddToMods(temporaryMods[tempIndex], temporaryTargets);
                        uIManager.CloseSelectedCaracter(SelectedCaracter);
                    break;
                case TARGETING.multipleEnemy:
                        temporaryTargets = new GameObject[Enemies.Count];
                        for (int u = 0; u < Enemies.Count; u++)
                        {
                            temporaryTargets[u] = Enemies[u].gameObject;
                        }
                        AddToMods(temporaryMods[tempIndex], temporaryTargets);
                        uIManager.CloseSelectedCaracter(SelectedCaracter);
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
                        uIManager.TemporarySelectedTarget = Caracters[0];
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
        if(_actions.Count >= playersOnRoundStart)
        {
            _actions.Clear();
            StartCoroutine(ChangeStateWithDelay(BATTLESTATE.EndOfPlayerTurn,0));
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
        StartCoroutine(ChangeStateWithDelay(BATTLESTATE.PlayerTurn,3));
    }

    private void ExecuteEnemyModifiers()     // Executa todos os modifiers na lista de modifiers dos inimigos
    {
        for (int i = 0; i < _modifiers.Count; i++)
        {
            _modifiers[i].ModifierToExecute.ExecuteMod(_modifiers[i].Targets);
        }
        _modifiers.Clear();
        tempEnemy++;
        StartCoroutine(ChangeStateWithDelay(BATTLESTATE.EnemyTurn,3));
    }

    #endregion

    public void SelectTargetButton(GameObject gm)
    {
        temporaryTargets = new GameObject[1] { gm };
    }

    public void EnemyAbility(Ability ab)
    {

        //Lógica de dizer qual é a abilidade que está a ser ultilizada pelo inimigo

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
                    temporaryTargets = new GameObject[Enemies.Count];
                    for (int u = 0; u < Enemies.Count; u++)
                    {
                        temporaryTargets[u] = Enemies[u].gameObject;
                    }
                    AddToMods(temporaryMods[tempIndex], temporaryTargets);
                    break;
                case TARGETING.multipleEnemy:
                    temporaryTargets = new GameObject[Caracters.Count];
                    for (int u = 0; u < Caracters.Count; u++)
                    {
                        temporaryTargets[u] = Caracters[u].gameObject;
                    }
                    AddToMods(temporaryMods[tempIndex], temporaryTargets);
                    break;
                case TARGETING.singleEnemy:
                    temporaryTargets = new GameObject[] { Caracters[Enemies[tempEnemy].ChoseEnemyTarget()].gameObject };
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
            StartCoroutine(ChangeStateWithDelay(BATTLESTATE.EnemyExecuteAbility, 2));
        }
    }

    private bool CheckIfRoundDone()
    {
        if(tempEnemy >= Enemies.Count)
        {
            return true;
        }
        return false;
    }

    public void LocateEnemies()
    {
        GameObject parent = GameObject.FindGameObjectWithTag("EnemySet");
        int enemiesCount = 0;
        //Enemies = new Enemy[parent.transform.GetChild(0).childCount];
        for (int i = 0; i < parent.transform.GetChild(0).childCount; i++)
        {
            if (parent.transform.GetChild(0).GetChild(i).CompareTag("Enemy"))
            {
                enemiesCount++;
                Enemies.Add(parent.transform.GetChild(0).GetChild(i).GetComponent<Enemy>());
            }
        }
    }

    #region Status

    private void CheckPlayerStatus()
    {
        bool isAny = false;
        foreach (PlayableCaracter caracterRef in Caracters)
        {
            foreach(StatusFx effect in caracterRef.currentStatus.Keys)
            {
                isAny = true;
                if (effect.HasEndRoundFx)
                {
                    effect.ApplyEffect(caracterRef);
                }
                if (effect.LoseStackOnEndRound)
                {
                    caracterRef.currentStatus[effect]--;
                }
                if (caracterRef.currentStatus[effect] <= 0)
                {
                    caracterRef.currentStatus.Remove(effect);
                }
            }
        }
        /*if(isAny == true)
        {
            StartCoroutine(ChangeStateWithDelay(BATTLESTATE.EnemyTurn, 3));
        }
        else
        {
            ChangeState(BATTLESTATE.EnemyTurn);
        }*/
        uIManager.ChangeTurnUIPrompt();
        StartCoroutine(ChangeStateWithDelay(BATTLESTATE.EnemyTurn, 3));

    }

    private void CheckEnemyStatus()
    {
        foreach (Enemy enemyRef in Enemies)
        {
            foreach (StatusFx effect in enemyRef.currentStatus.Keys)
            {
                if (effect.HasEndRoundFx)
                {
                    effect.ApplyEffect(enemyRef);
                }
                if (effect.LoseStackOnEndRound)
                {
                    enemyRef.currentStatus[effect]--;
                }
                if (enemyRef.currentStatus[effect] <= 0)
                {
                    enemyRef.currentStatus.Remove(effect);
                }
            }
        }
        uIManager.ChangeTurnUIPrompt();
        StartCoroutine(ChangeStateWithDelay(BATTLESTATE.PlayerTurn, 3));
    }

    #endregion

    public void SpawnFloatingDamage(Vector2 pos, int damage)
    {
        GameObject fD = Instantiate(FloatingDamagePre, pos, Quaternion.identity);
        fD.GetComponent<TextMeshPro>().text = "-" + damage.ToString();
    }

    private IEnumerator ChangeStateWithDelay(BATTLESTATE newState, float secs)
    {
        yield return new WaitForSeconds(secs);
        ChangeState(newState);
    }

    public void AdjustCaracterNumbers(int value)
    {
        
        for (int i = 0; i < _caracters.Count; i++)
        {
            if (_caracters[i].CaracterNumber > value)
            {
                Debug.Log("123");
                _caracters[i].CaracterNumber--;
            }
        }
    }

    private void CheckPlayers()
    {
        playersOnRoundStart = Caracters.Count;
    }
}

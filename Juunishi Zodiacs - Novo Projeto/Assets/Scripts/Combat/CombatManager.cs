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
    ExecutePlayerTurn,
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

    /*[SerializeReference] Modifiers[] targetlessMods;
    [SerializeField] Ability temporaryAbility;*/

    [Header("TemporaryStuff")]
    [SerializeField] int tempIndex;
    [SerializeReference] Modifiers[] temporaryMods;
    [SerializeField] GameObject[] temporaryTargets;
    

    public int SelectedCaracter { get => _selectedCaracter; set { ChangeSelectedCaracter(_selectedCaracter, value);} }

    public PlayableCaracter[] Caracters { get => _caracters; set => _caracters = value; }
    internal BATTLESTATE CurState { get => _curState; set => _curState = value; }

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
            case BATTLESTATE.ExecutePlayerTurn:
                break;
            case BATTLESTATE.EnemyTurn:
                break;
            case BATTLESTATE.Victory:
                break;
            case BATTLESTATE.Defeat:
                break;
        }
    }

    #region ChangeState
    void ChangeState(BATTLESTATE _newState)
    {
        CurState = _newState;
        switch (CurState)
        {
            case BATTLESTATE.StartBattle:
                ChangeState(BATTLESTATE.PlayerTurn);
                break;
            case BATTLESTATE.PlayerTurn:
                break;
            case BATTLESTATE.ExecutePlayerTurn:
                break;
            case BATTLESTATE.SelectingTarget:
                break;
            case BATTLESTATE.EnemyTurn:
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

    private void CheckCaracterSelected()
    {
        if(SelectedCaracter == 0 || SelectedCaracter == 1 || SelectedCaracter == 2)
        {
            //uIManager.OpenAbilitiesMenu(SelectedCaracter);
        }
    }

    private void ChangeSelectedCaracter(int oldValue, int newValue)
    {
        _selectedCaracter = newValue;
        uIManager.OpenActionMenu();
    }
    #endregion

    #region AbilitySelection

    private void TargetAbility()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            temporaryTargets = new GameObject[] { _enemies[0].gameObject };
            Debug.Log("click");
        }

        SelectedModifiers modToAdd;
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
            ChangeState(BATTLESTATE.PlayerTurn);
        }
    }

    public void RecieveTarget(GameObject lockedTarget)
    {
        temporaryTargets = new GameObject[1] { lockedTarget };
    }

    private void AddToMods(Modifiers mod1, GameObject[] targets1)
    {
        SelectedModifiers modToAdd = new SelectedModifiers(mod1, targets1);
        _modifiers.Add(modToAdd);
        temporaryTargets = null;
        tempIndex++;
    }

    public void SelectAbility(int _abilityNumb)
    {

        if(_abilityNumb >= 0 && _abilityNumb <=2)
        {
            temporaryMods = _caracters[SelectedCaracter].Abilities[_abilityNumb].Mods.ToArray();

            ChangeState(BATTLESTATE.SelectingTarget);

            /*Modifiers[] temporaryMods = _caracters[SelectedCaracter].Abilities[_abilityNumb].Mods.ToArray();
            targetlessMods = new Modifiers[temporaryMods.Length];
            Array.Copy(temporaryMods, targetlessMods,temporaryMods.Length);*/

            /*_actions.Add(_caracters[SelectedCaracter].Abilities[_abilityNumb]);
            for (int i = 0; i < _caracters[SelectedCaracter].Abilities[_abilityNumb].Mods.Count && ; i++)
            {
                GameObject[] tempTargets;
                Modifiers mod = _caracters[SelectedCaracter].Abilities[_abilityNumb].Mods[i];
                if(mod.TargetType == TARGETING.self)
                {
                    tempTargets = new GameObject[1];
                    tempTargets[0] = _caracters[SelectedCaracter].gameObject;
                    SelectedModifiers modToAdd = new SelectedModifiers(mod, tempTargets);
                    _modifiers.Add(modToAdd);
                }
                if (mod.TargetType == TARGETING.multipleEnemy)
                {
                    tempTargets = new GameObject[_enemies.Length];
                    for (int u = 0; u < _enemies.Length; u++)
                    {
                        tempTargets[u] = _enemies[u].gameObject;
                    }
                    SelectedModifiers modToAdd = new SelectedModifiers(mod, tempTargets);
                    _modifiers.Add(modToAdd);
                }
                if (mod.TargetType == TARGETING.multipleAlly)
                {
                    tempTargets = new GameObject[_caracters.Length];
                    for (int u = 0; u < _caracters.Length; u++)
                    {
                        tempTargets[u] = _caracters[u].gameObject;
                    }
                    SelectedModifiers modToAdd = new SelectedModifiers(mod, tempTargets);
                    _modifiers.Add(modToAdd);
                }
                if (mod.TargetType == TARGETING.singleAlly)
                {
                    tempTargets = new GameObject[1];
                    tempTargets[0] = this.gameObject;
                    SelectedModifiers modToAdd = new SelectedModifiers(mod, tempTargets);
                    _modifiers.Add(modToAdd);
                }
                if (mod.TargetType == TARGETING.singleEnemy)
                {
                    TargetSelection(mod);
                    SelectedModifiers modToAdd = new SelectedModifiers(mod, tempTargets);
                    _modifiers.Add(modToAdd);
                }
            }*/
        }
        else
        {
            _actions.Add(_caracters[SelectedCaracter].PhysicalAbility);
        }

        //ChangeState(BATTLESTATE.SelectingTarget);

        uIManager.CloseActionMenu();
        uIManager.LockSelectionButton(SelectedCaracter);
    }

    private void CheckForAbilities()
    {
        if(_actions.Count >= 3)
        {
            ChangeState(BATTLESTATE.ExecutePlayerTurn);
        }
    }

    #endregion

    public void SelectTargetButton(GameObject gm)
    {
        temporaryTargets = new GameObject[1] { gm };
    }
}

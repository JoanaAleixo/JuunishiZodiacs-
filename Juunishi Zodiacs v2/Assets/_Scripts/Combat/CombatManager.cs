using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using TMPro;
using System.Linq;

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
    [SerializeField] GameObject background;
    [Header("Players")]
    [SerializeField] List<PlayableCaracter> _caracters;
    [SerializeField] List<Enemy> _enemies;
    [SerializeField] List<Ability> _actions;
    [SerializeField] List<SelectedModifiers> _modifiers;
    [SerializeField] int _selectedCaracter;
    [SerializeField] GameObject _actionMenu;
    [SerializeField] int playersOnRoundStart;
    [SerializeField] Ability emptyAbility;
    [Header("TemporaryStuff")]
    [SerializeField] int tempIndex;
    [SerializeReference] Modifiers[] temporaryMods;
    [SerializeField] GameObject[] temporaryTargets;
    [SerializeField] int tempEnemy = 0;
    [SerializeField] GameObject floatingDamagePre;
    [SerializeField] bool oneCaracter;
    

    public int SelectedCaracter { get => _selectedCaracter; set { ChangeSelectedCaracter(_selectedCaracter, value);} }

    internal BATTLESTATE CurState { get => _curState; set => _curState = value; }
    public List<PlayableCaracter> Caracters { get => _caracters; set => _caracters = value; }
    public List<Enemy> Enemies { get => _enemies; set => _enemies = value; }
    public Ability EmptyAbility { get => emptyAbility; set => emptyAbility = value; }

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
        ChangeState(BATTLESTATE.StartBattle);
        temporaryTargets = null;

        if (oneCaracter)
        {
            StartCoroutine(DiePls());
        }
    }

    public IEnumerator DiePls()
    {
        oneCaracter = true;
        yield return new WaitForSeconds(0.3f);
        foreach (var car in Caracters.ToList())
        {
            if (car.name != "Akira")
            {
                car.TakeDamage(1000, DAMAGETYPE.None);
                car.gameObject.SetActive(false);
                uIManager.OnOneCaracter();
            }
        }
        uIManager.TurnOff2Caracters();
    }

    #endregion

    #region Update + ChangeState

    void Update()
    {
        switch (CurState)
        {
            case BATTLESTATE.StartBattle:

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

        if(_curState != BATTLESTATE.StartBattle && _curState != BATTLESTATE.Defeat && _curState != BATTLESTATE.Victory)
        {
            CombatEndVerification();
        }
        

        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeState(BATTLESTATE.Defeat);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            ChangeState(BATTLESTATE.Victory);
        }

        
    }

    

    void ChangeState(BATTLESTATE _newState)
    {
        CurState = _newState;
        switch (CurState)
        {
            case BATTLESTATE.StartBattle:
                CheckPlayers();
                
                StartCoroutine(WaitTwoSeconds());
                break;
            case BATTLESTATE.PlayerTurn:
                if(_actions.Count == 0)
                {
                    GiveSpPls();
                    uIManager.UnlockAllSelectionButtons();
                    PreRoundStatusCheck();
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
                    if (EnemyStatusCheck(tempEnemy))
                    {
                        Enemies[tempEnemy].ChoseAbility();
                    }
                    else
                    {
                        uIManager.ShowTextPrompt(Enemies[tempEnemy].MyCaracter.Name + " is paralized!");
                        tempEnemy++;
                        StartCoroutine(ChangeStateWithDelay(BATTLESTATE.EnemyTurn, 2));
                    }
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
                StopAllCoroutines();
                PlayersReset();
                WinCombat();
                break;
            case BATTLESTATE.Defeat:
                StopAllCoroutines();
                PlayersReset();
                uIManager.OpenLoseMenu();
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
        if (_abilityNumb == 3)
        {
            uIManager.OpenAbilityInfo(Caracters[SelectedCaracter].MyCaracter.PhysicalAbility);
        }
        else
        {
            uIManager.OpenAbilityInfo(Caracters[SelectedCaracter].MyCaracter.Abilities[_abilityNumb]);
        }
    }

    public void SelectAbility(int _abilityNumb)   // Quando um botão de habilidade é clicado
    {
        bool rollDrowsy = false;
        bool passedRoll = true;
        int chance = 0;
        foreach (var status in Caracters[SelectedCaracter].currentStatus)
        {
            if(status.Key is DrowsyFx)
            {
                rollDrowsy = true;
                switch (status.Value) 
                {
                    case 1:
                        chance = 90;
                        break;
                    case 2:
                        chance = 80;
                        break;
                    case 3:
                        chance = 0;
                        break;
                    case 4:
                        chance = 60;
                        break;
                    case 5:
                        chance = 50;
                        break;
                    default:
                        chance = 100;
                        break;
                }
            }
        }
        if (rollDrowsy)
        {
            int rand = UnityEngine.Random.Range(1,101);
            if(rand <= chance)
            {
                Debug.Log("Passed chance of " + chance + "% with a " + rand);
                passedRoll = true;
            }
            else
            {
                Debug.Log("Did not pass chance of " + chance + "% with a " + rand);
                passedRoll = false;
            }
        }
        if (passedRoll)
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
        }
        else
        {
            uIManager.ShowTextPrompt("Your attack failed!");
            uIManager.CloseAbilityInfo();
            _actions.Add(EmptyAbility);
            uIManager.CloseSelectedCaracter(SelectedCaracter);
            StartCoroutine(ChangeStateWithDelay(BATTLESTATE.PlayerTurn, 2));
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
        if (oneCaracter)
            playersOnRoundStart = 1;
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
        /*if (tempIndex < temporaryMods.Length)*/
        foreach (var ab in temporaryMods) 
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
                    AddToMods(temporaryMods[tempIndex], temporaryTargets);
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
        if(parent.name == "PrefEnemySet1(Clone)")
        {
            Debug.Log("1");
            foreach (var car in Caracters.ToList())
            {
                if (car.name == "Akira")
                {
                    Debug.Log("2");
                    uIManager.ChangeAkira(car);
                }
            }
        }
        int enemiesCount = 0;
        //Enemies = new Enemy[parent.transform.GetChild(0).childCount];
        for (int i = 0; i < parent.transform.GetChild(0).childCount; i++)
        {
            if (parent.transform.GetChild(0).GetChild(i).GetChild(0).CompareTag("Enemy"))
            {
                enemiesCount++;
                Enemy enem = parent.transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<Enemy>();
                Enemies.Add(enem);
                enem.MyCaracter.HpMax.value = enem.MyCaracter.HpMax.resetValue;
            }
        }
    }

    #region Status

    private void CheckPlayerStatus()
    {
        if(Caracters != null)
        {
            foreach (PlayableCaracter caracterRef in Caracters)
            {
                foreach (StatusFx effect in caracterRef.currentStatus.Keys.ToList())
                {
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
                uIManager.RepresentStatusFx(caracterRef);
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
        uIManager.RepaintSpritesBackToNormal();
        uIManager.ChangeTurnUIPrompt();
        StartCoroutine(ChangeStateWithDelay(BATTLESTATE.EnemyTurn, 3));

    }

    private void CheckEnemyStatus()
    {
        if(Enemies != null)
        {
            foreach (Enemy enemyRef in Enemies.ToList())
            {
                foreach (StatusFx effect in enemyRef.currentStatus.Keys.ToList())
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
                enemyRef.EnemyStatusFx();
            }
            uIManager.ChangeTurnUIPrompt();
            StartCoroutine(ChangeStateWithDelay(BATTLESTATE.PlayerTurn, 3));
        }
        else
        {

        }
    }

    #endregion

    public void SpawnFloatingDamage(Vector2 pos, int damage)
    {
        GameObject fD = Instantiate(floatingDamagePre, pos, Quaternion.identity);
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
                _caracters[i].CaracterNumber--;
                _caracters[i].NumberRetracted++;
            }
        }
    }

    private void CheckPlayers()
    {
        playersOnRoundStart = Caracters.Count;
    }

    private void CombatEndVerification()
    {
        if(Enemies.Count <= 0)
        {
            StartCoroutine(ChangeStateWithDelay(BATTLESTATE.Victory, 2));
            //ChangeState(BATTLESTATE.Victory);
        }
        else if(Caracters.Count <= 0)
        {
            //ChangeState(BATTLESTATE.Defeat);
            StartCoroutine(ChangeStateWithDelay(BATTLESTATE.Defeat, 2));
        }
    }

    public void LoseRetryButton()
    {
        LoadingSceneManager.sceneInstance.ReloadCombatScene();
    }

    public void LoseTitleScreenButton()
    {
        LoadingSceneManager.sceneInstance.LoadScene("Title Screen");
    }

    private void WinCombat()
    {
        LoadingSceneManager.sceneInstance.LoadSceneAfterCombat();
    }

    IEnumerator WaitTwoSeconds()
    {
        yield return new WaitForSeconds(2);
        PlayersReset();
        ChangeState(BATTLESTATE.PlayerTurn);
    }

    void PreRoundStatusCheck()
    {
        foreach (var carac in Caracters)
        {
            foreach (var status in carac.currentStatus.ToList())
            {
                if (status.Key is ParalizeFx)
                {
                    uIManager.ShowTextPrompt(Caracters[carac.CaracterNumber].MyCaracter.Name + " is paralized!");
                    _actions.Add(EmptyAbility);
                    uIManager.LockSelectionButton(carac.CaracterNumber);
                    carac.currentStatus.Remove(status.Key);
                    
                }
            }
            uIManager.RepresentStatusFx(carac);
        }
    }

    bool EnemyStatusCheck(int enemyId)
    {
        foreach(var status in Enemies[enemyId].currentStatus.ToList())
        {
            if (status.Key is ParalizeFx)
            {
                _actions.Add(EmptyAbility);
                print("He is paralized");
                Enemies[enemyId].currentStatus.Remove(status.Key);
                Enemies[enemyId].EnemyStatusFx();
                return false;
            }
        }
        return true;
    }

    private void PlayersReset()
    {
        foreach (var item in Caracters)
        {
            PlayableCaracterScptObj carac = (PlayableCaracterScptObj)item.MyCaracter;
            carac.HpMax.value = carac.HpMax.resetValue;
            carac.SpMax.value = carac.SpMax.resetValue;
        }
    }

    private void GiveSpPls()
    {
        foreach (var item in Caracters)
        {
            item.UpdateSp(15);
        }
    }

    public void ChangeBackground(Sprite back)
    {
        background.GetComponent<SpriteRenderer>().sprite = back;
    }
}

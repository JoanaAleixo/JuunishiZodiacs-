using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BATTLESTATE
{
    StartBattle,
    PlayerTurn, 
    SelectCaracter,
    EnemyTurn, 
    Victory, 
    Defeat
}

public class CombatManager : MonoBehaviour
{
    public static CombatManager combatInstance;
    [SerializeField] CombatUiManager uIManager;
    [SerializeField] BATTLESTATE _curState;
    [Header("Players")]
    [SerializeField] PlayableCaracter[] _caracters = new PlayableCaracter[3];

    [SerializeField] List<Ability> _actions;
    [SerializeField] int _selectedCaracter;

    [SerializeField] GameObject _actionMenu;

    public int SelectedCaracter { get => _selectedCaracter; set { ChangeSelectedCaracter(_selectedCaracter, value);} }

    public PlayableCaracter[] Caracters { get => _caracters; set => _caracters = value; }

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
        _curState = BATTLESTATE.StartBattle;
    }

    void Update()
    {
        switch (_curState)
        {
            case BATTLESTATE.StartBattle:
                ChangeState(BATTLESTATE.PlayerTurn);
                break;
            case BATTLESTATE.PlayerTurn:

                ChangeState(BATTLESTATE.SelectCaracter);
                
                /*if(_action != null)
                {
                    ChangeState(BATTLESTATE.EnemyTurn);
                }*/
                break;
            case BATTLESTATE.SelectCaracter:
                //CheckCaracterSelected();
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
        _curState = _newState;
        switch (_curState)
        {
            case BATTLESTATE.StartBattle:
                ChangeState(BATTLESTATE.PlayerTurn);
                break;
            case BATTLESTATE.PlayerTurn:
                break;
            case BATTLESTATE.SelectCaracter:
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
}

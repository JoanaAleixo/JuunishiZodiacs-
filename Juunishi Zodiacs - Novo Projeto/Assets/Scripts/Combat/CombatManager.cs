using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BattleState
{
    StartBattle, PlayerTurn, EnemyTurn, Victory, Defeat
}

public class CombatManager : MonoBehaviour
{
    public static CombatManager combatInstance;

    [SerializeField] BattleState _curState;
    [SerializeField] AtaqueMagico _action;

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
        _curState = BattleState.StartBattle;
    }

    void Update()
    {
        switch (_curState)
        {
            case BattleState.StartBattle:
                
                break;
            case BattleState.PlayerTurn:
                if(_action != null)
                {
                    ChangeState(BattleState.EnemyTurn);
                }
                break;
            case BattleState.EnemyTurn:
                break;
            case BattleState.Victory:
                break;
            case BattleState.Defeat:
                break;
            
        }
    }

    void ChangeState(BattleState _newState)
    {
        _curState = _newState;
        switch (_curState)
        {
            case BattleState.StartBattle:
                ChangeState(BattleState.PlayerTurn);
                break;
            case BattleState.PlayerTurn:
                break;
            case BattleState.EnemyTurn:
                break;
            case BattleState.Victory:
                break;
            case BattleState.Defeat:
                break;
        }
    }
}

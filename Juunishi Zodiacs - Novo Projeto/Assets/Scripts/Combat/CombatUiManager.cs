using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatUiManager : MonoBehaviour
{
    public static CombatUiManager uiInstance;
    CombatManager combatMg;
    [Header("ActionMenu")]
    [SerializeField] GameObject _actionMenu;
    [SerializeField] GameObject _attackBut;
    [SerializeField] GameObject _spellsBut;
    [SerializeField] GameObject _abilitiesMenu;
    [SerializeField] GameObject[] _magicalAttack = new GameObject[3];
    [SerializeField] GameObject[] _selectionButtons;

    [SerializeField] BaseStats _temporarySelectedTarget;
    float _targetTimer;
    bool _enemyTargetSelecting = false;
    bool _allyTargetSelecting = false;

    public BaseStats TemporarySelectedTarget { get => _temporarySelectedTarget; set => _temporarySelectedTarget = value; }
    public bool EnemyTargetSelecting { get => _enemyTargetSelecting; set => _enemyTargetSelecting = value; }
    public bool AllyTargetSelecting { get => _allyTargetSelecting; set => _allyTargetSelecting = value; }

    private void Awake()
    {
        if (uiInstance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            uiInstance = this;
        }
    }

    void Start()
    {
        combatMg = CombatManager.combatInstance;
        _temporarySelectedTarget = null;
    }

    void Update()
    {
        if (EnemyTargetSelecting == true)
        {
            _targetTimer += Time.deltaTime;
            if (_targetTimer > 1)
            {
                _temporarySelectedTarget.transform.GetChild(0).gameObject.SetActive(!_temporarySelectedTarget.transform.GetChild(0).gameObject.activeSelf);
                _targetTimer = 0;
            }   
        }
    }

    public void OpenAbilitiesMenu()
    {
        _abilitiesMenu.SetActive(true);
        for (int i = 0; i < _magicalAttack.Length; i++)
        {
            Debug.Log("ability");
            _magicalAttack[i].GetComponentInChildren<TextMeshProUGUI>().text = combatMg.Caracters[combatMg.SelectedCaracter].Abilities[i].name;
        }
    }

    public void OpenActionMenu()
    {
        _actionMenu.SetActive(true);
        _abilitiesMenu.SetActive(false);
        _attackBut.GetComponentInChildren<TextMeshProUGUI>().text = combatMg.Caracters[combatMg.SelectedCaracter].PhysicalAbility.name;
    }

    public void CloseActionMenu()
    {
        _actionMenu.SetActive(false);
    }

    public void LockSelectionButton(int _buttonInt)
    {
        _selectionButtons[_buttonInt].SetActive(false);
    }

    public void OpenEnemyTargetSelection()
    {
        _temporarySelectedTarget.transform.GetChild(0).gameObject.SetActive(true);
        EnemyTargetSelecting = true;
    }

    public void OpenAllyTargetSelection()
    {
        _temporarySelectedTarget.transform.GetChild(0).gameObject.SetActive(true);
        AllyTargetSelecting = true;
    }

    public void ChangeTarget(BaseStats baseStat)
    {
        _temporarySelectedTarget.transform.GetChild(0).gameObject.SetActive(false);
        TemporarySelectedTarget = baseStat;
        _temporarySelectedTarget.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void LockTarget(BaseStats baseStat)
    {
        EnemyTargetSelecting = false;
        AllyTargetSelecting = false;
        _temporarySelectedTarget.transform.GetChild(0).gameObject.SetActive(false);
        TemporarySelectedTarget = baseStat;
        _temporarySelectedTarget.transform.GetChild(0).gameObject.SetActive(false);
        combatMg.RecieveTarget(_temporarySelectedTarget.gameObject);
        _temporarySelectedTarget = null;
    }

}

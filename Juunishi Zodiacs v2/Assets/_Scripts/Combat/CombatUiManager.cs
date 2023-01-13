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
    [SerializeField] GameObject _caracterSelection;
    [SerializeField] GameObject[] _magicalAttack = new GameObject[3];
    [SerializeField] GameObject[] _selectionButtons;
    [Header("TemporaryTargetSelection")]
    [SerializeField] BaseStats _temporarySelectedTarget;
    [SerializeField] float _targetTimer;
    [SerializeField] bool _enemyTargetSelecting = false;
    [SerializeField] bool _allyTargetSelecting = false;
    //[Header("Other")]

    public BaseStats TemporarySelectedTarget { get => _temporarySelectedTarget; set => _temporarySelectedTarget = value; }
    public bool EnemyTargetSelecting { get => _enemyTargetSelecting; set => _enemyTargetSelecting = value; }
    public bool AllyTargetSelecting { get => _allyTargetSelecting; set => _allyTargetSelecting = value; }

    #region Awake + Start + Update

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

    #endregion

    #region Abilities Tab

    public void OpenAbilitiesMenu()
    {
        _abilitiesMenu.SetActive(true);
        for (int i = 0; i < _magicalAttack.Length; i++)
        {
            _magicalAttack[i].GetComponentInChildren<TextMeshProUGUI>().text = combatMg.Caracters[combatMg.SelectedCaracter].MyCaracter.Abilities[i].name;
        }
    }

    public void OpenActionMenu()
    {
        _actionMenu.SetActive(true);
        _abilitiesMenu.SetActive(false);
        _attackBut.GetComponentInChildren<TextMeshProUGUI>().text = combatMg.Caracters[combatMg.SelectedCaracter].MyCaracter.PhysicalAbility.name;
    }

    public void CloseActionMenu()
    {
        _actionMenu.SetActive(false);
    }

    #endregion

    #region TargetSelection

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

    #endregion

    #region CaracterSelection

    public void LockCaracterSelection()
    {
        for (int i = 0; i < _selectionButtons.Length; i++)
        {
            _selectionButtons[i].GetComponent<Button>().enabled = false;
        }
    }

    public void UnlockCaracterSelection()
    {
        for (int i = 0; i < _selectionButtons.Length; i++)
        {
            _selectionButtons[i].GetComponent<Button>().enabled = true;
        }
    }

    public void LockSelectionButton(int _buttonInt)
    {
        _selectionButtons[_buttonInt].SetActive(false);
    }

    public void UnlockAllSelectionButtons()
    {
        for (int i = 0; i < _selectionButtons.Length; i++)
        {
            _selectionButtons[i].SetActive(true);
        }
    }

    public void DisableCaracterSelection()
    {
        _caracterSelection.SetActive(false);
    }

    public void EnableCaracterSelection()
    {
        _caracterSelection.SetActive(true);
    }

    #endregion
}

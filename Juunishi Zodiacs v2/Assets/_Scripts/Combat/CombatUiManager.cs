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
    [Header("AbilityInformation")]
    [SerializeField] GameObject _abilitiInfo;
    [SerializeField] TextMeshProUGUI _abilitiName;
    [SerializeField] TextMeshProUGUI _abilitiDesc;
    [SerializeField] Image[] _abilitiImage;
    [Header("IconSprites")]
    [SerializeField] Sprite[] _abilitySprites;
    [Header("AbilityUsed")]
    [SerializeField] GameObject _abilityUsed;
    [SerializeField] TextMeshProUGUI _abilityUsedText;
    [Header("ChangeTurn")]
    [SerializeField] GameObject _changeTurnObj;

    //[Header("Other")]

    public BaseStats TemporarySelectedTarget { get => _temporarySelectedTarget; set { _temporarySelectedTarget = value; _targetTimer = 0; } }
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
        if (EnemyTargetSelecting == true || AllyTargetSelecting == true)
        {
            _targetTimer += Time.deltaTime;
            if (_targetTimer > 1)
            {
                _temporarySelectedTarget.transform.GetChild(1).gameObject.SetActive(!_temporarySelectedTarget.transform.GetChild(1).gameObject.activeSelf);
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
            _magicalAttack[i].GetComponentInChildren<TextMeshProUGUI>().text = combatMg.Caracters[combatMg.SelectedCaracter].MyCaracter.Abilities[i].AbilityName;

            if(combatMg.Caracters[combatMg.SelectedCaracter].MyCaracter.Abilities[i].Mods[0] is DamageModifier)
            {
                DamageModifier dm = (DamageModifier)combatMg.Caracters[combatMg.SelectedCaracter].MyCaracter.Abilities[i].Mods[0];
                switch (dm.DamageType)
                {
                    case DAMAGETYPE.Physical:
                        _abilitiImage[i].sprite = _abilitySprites[0];
                        break;
                    case DAMAGETYPE.Fire:
                        _abilitiImage[i].sprite = _abilitySprites[1];
                        break;
                    case DAMAGETYPE.Water:
                        _abilitiImage[i].sprite = _abilitySprites[2];
                        break;
                    case DAMAGETYPE.Rock:
                        _abilitiImage[i].sprite = _abilitySprites[3];
                        break;
                    case DAMAGETYPE.Nature:
                        _abilitiImage[i].sprite = _abilitySprites[4];
                        break;
                    case DAMAGETYPE.Metal:
                        _abilitiImage[i].sprite = _abilitySprites[5];
                        break;
                    case DAMAGETYPE.None:
                        _abilitiImage[i].sprite = null;
                        break;
                }
            }
            if(combatMg.Caracters[combatMg.SelectedCaracter].MyCaracter.Abilities[i].Mods[0] is HealModifier)
            {
                _abilitiImage[i].sprite = _abilitySprites[6];
            }
            if(combatMg.Caracters[combatMg.SelectedCaracter].MyCaracter.Abilities[i].Mods[0] is StatusModifier)
            {
                StatusModifier sM = (StatusModifier)combatMg.Caracters[combatMg.SelectedCaracter].MyCaracter.Abilities[i].Mods[0];
                if (sM.IsBuff)
                {
                    _abilitiImage[i].sprite = _abilitySprites[7];
                }
                else
                {
                    _abilitiImage[i].sprite = _abilitySprites[8];
                }
            }
        }
    }

    public void OpenAbilityInfo(Ability ab)
    {
        _abilitiInfo.SetActive(true);
        _abilitiName.text = ab.AbilityName;
        _abilitiDesc.text = ab.Description;
    }

    public void CloseAbilityInfo()
    {
        _abilitiInfo.SetActive(false);
    }

    public void OpenActionMenu()
    {
        Debug.Log(combatMg.SelectedCaracter);
        combatMg.Caracters[combatMg.SelectedCaracter].transform.GetChild(0).gameObject.SetActive(true);
        _actionMenu.SetActive(true);
        _abilitiesMenu.SetActive(false);
        _attackBut.GetComponentInChildren<TextMeshProUGUI>().text = combatMg.Caracters[combatMg.SelectedCaracter].MyCaracter.PhysicalAbility.AbilityName;
    }

    public void CloseSelectedCaracter(int value)
    {
        if (combatMg.Caracters[value] != null)
        {
            combatMg.Caracters[value].transform.GetChild(0).gameObject.SetActive(false);
        }
        
    }

    public void CloseActionMenu()
    {
        _actionMenu.SetActive(false);
    }

    public void ShowAbilityUsedPrompt(Ability ab, BaseStats caracter)
    {
        _abilityUsed.SetActive(true);
        _abilityUsedText.text = caracter.name + " used\n" + ab.AbilityName;
    }

    public void CloseAbilityUsedPrompt()
    {
        _abilityUsed.SetActive(false);
    }

    #endregion

    #region TargetSelection

    public void OpenEnemyTargetSelection()
    {
        for (int i = 0; i < combatMg.Enemies.Count; i++)
        {
            combatMg.Enemies[i].transform.GetChild(0).gameObject.SetActive(true);
        }
        _temporarySelectedTarget.transform.GetChild(1).gameObject.SetActive(true);
        EnemyTargetSelecting = true;
    }

    public void OpenAllyTargetSelection()
    {
        for (int i = 0; i < combatMg.Caracters.Count; i++)
        {
            combatMg.Caracters[i].transform.GetChild(0).gameObject.SetActive(true);
        }
        _temporarySelectedTarget.transform.GetChild(1).gameObject.SetActive(true);
        AllyTargetSelecting = true;
    }

    public void ChangeTarget(BaseStats baseStat)
    {
        _temporarySelectedTarget.transform.GetChild(1).gameObject.SetActive(false);
        TemporarySelectedTarget = baseStat;
        _temporarySelectedTarget.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void LockTarget(BaseStats baseStat)
    {
        EnemyTargetSelecting = false;
        AllyTargetSelecting = false;
        for (int i = 0; i < combatMg.Enemies.Count; i++)
        {
            combatMg.Enemies[i].transform.GetChild(0).gameObject.SetActive(false);
        }
        for (int i = 0; i < combatMg.Caracters.Count; i++)
        {
            combatMg.Caracters[i].transform.GetChild(0).gameObject.SetActive(false);
        }
        _temporarySelectedTarget.transform.GetChild(1).gameObject.SetActive(false);
        TemporarySelectedTarget = baseStat;
        _temporarySelectedTarget.transform.GetChild(1).gameObject.SetActive(false);
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

    public void ChangeTurnUIPrompt()
    {
        _changeTurnObj.GetComponent<ChangeTurn>().ChangeTurnPrompt();
    }
}

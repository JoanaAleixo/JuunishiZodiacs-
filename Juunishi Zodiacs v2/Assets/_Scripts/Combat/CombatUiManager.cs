using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.TextCore.Text;

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
    [SerializeField] GameObject _caractersInfo;
    [SerializeField] GameObject _pauseMenu;
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
    [Header("LoseMenu")]
    [SerializeField] GameObject _loseMenu;
    [Header("StatusFx")]
    [SerializeField] GameObject[] _player0StatusFx;
    [SerializeField] GameObject[] _player1StatusFx;
    [SerializeField] GameObject[] _player2StatusFx;
    [SerializeField] Sprite boundFxSprite;
    [SerializeField] Sprite paralizeFxSprite;
    [SerializeField] Sprite drowsyFxSprite;
    [SerializeField] Sprite shieldFxSprite;
    [SerializeField] Sprite rageFxSprite;
    [SerializeField] Sprite cureDebuffFxSprite;
    

    //[Header("Other")]

    public BaseStats TemporarySelectedTarget { get => _temporarySelectedTarget; set { _temporarySelectedTarget = value; _targetTimer = 0; } }
    public bool EnemyTargetSelecting { get => _enemyTargetSelecting; set => _enemyTargetSelecting = value; }
    public bool AllyTargetSelecting { get => _allyTargetSelecting; set => _allyTargetSelecting = value; }
    public Sprite BoundFxSprite { get => boundFxSprite;}
    public Sprite ParalizeFxSprite { get => paralizeFxSprite;}
    public Sprite DrowsyFxSprite { get => drowsyFxSprite; }
    public Sprite ShieldFxSprite { get => shieldFxSprite; }
    public Sprite CureDebuffFxSprite { get => cureDebuffFxSprite; }

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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenClosePauseMenu();
        }
    }

    public void OpenClosePauseMenu()
    {
        if (_pauseMenu.activeSelf == true)
        {
            _pauseMenu.SetActive(false);
        }
        else
        {
            _pauseMenu.SetActive(true);
        }
    }

    #endregion

    #region Abilities Tab

    public void OpenAbilitiesMenu()
    {
        foreach (var but in _magicalAttack)
        {
            but.GetComponent<Button>().interactable = true;
        }

        for (int i = 0; i < _magicalAttack.Length; i++)
        {

            PlayableCaracterScptObj car = (PlayableCaracterScptObj)combatMg.Caracters[combatMg.SelectedCaracter].MyCaracter;
            
            foreach (var mod in combatMg.Caracters[combatMg.SelectedCaracter].MyCaracter.Abilities[i].Mods)
            {
                if(mod is CostModifier)
                {
                    CostModifier costModifier = (CostModifier)mod;
                    if(costModifier.CostType == COSTTYPE.Sp)
                    {
                        if(car.SpMax.value < costModifier.Quantity)
                        {
                            _magicalAttack[i].GetComponent<Button>().interactable = false;
                        }
                    }
                }
            } 

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
        _abilitiesMenu.SetActive(true);
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

    public void ShowTextPrompt(string textToUse)
    {
        _abilityUsed.SetActive(true);
        _abilityUsedText.text = textToUse;
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
        combatMg.Caracters[_buttonInt].GetComponent<SpriteRenderer>().color = new Color32(255/2, 255/2, 255/2, 255);
        Debug.Log("Lokou");
    }

    public void RepaintSpritesBackToNormal()
    {
        for (int i = 0; i < _selectionButtons.Length; i++)
        {
            combatMg.Caracters[i].GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }
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

    public void OpenLoseMenu()
    {
        _loseMenu.SetActive(true);
    }

    public void AddStatusFxUi()
    {

    }

    public void RepresentStatusFx(BaseStats car)
    {
        int cont = 0;

        if(car is PlayableCaracter)
        {
            PlayableCaracter carec = (PlayableCaracter)car;
            switch (car.CaracterNumber+carec.NumberRetracted)
            {
                case 0:
                    foreach (var status in car.currentStatus)
                    {
                        Debug.Log(status);
                        _player0StatusFx[cont].SetActive(true);
                        if (status.Key is BoundFx)
                        {
                            _player0StatusFx[cont].GetComponent<Image>().sprite = BoundFxSprite;
                        }
                        else if (status.Key is ParalizeFx)
                        {
                            _player0StatusFx[cont].GetComponent<Image>().sprite = ParalizeFxSprite;
                        }
                        else if (status.Key is DrowsyFx)
                        {
                            Debug.Log("Vi um drowsy");
                            _player0StatusFx[cont].GetComponent<Image>().sprite = DrowsyFxSprite;
                        }
                        else if (status.Key is ShieldFx)
                        {
                            Debug.Log("Vi um shield");
                            _player0StatusFx[cont].GetComponent<Image>().sprite = ShieldFxSprite;
                        }
                        else if (status.Key is CureDebuffFx)
                        {
                            _player0StatusFx[cont].GetComponent<Image>().sprite = CureDebuffFxSprite;
                        }
                        /*else if (status.Key is RageFx)
                        {

                        }
                        else if (status.Key is OutroFx)
                        {
                        
                        }*/
                        _player0StatusFx[cont].GetComponentInChildren<TextMeshProUGUI>().text = status.Value.ToString();
                        cont++;
                    }
                    for (int i = cont; i < 5; i++)
                    {
                        _player0StatusFx[i].SetActive(false);
                    }
                    break;
                case 1:
                    foreach (var status in car.currentStatus)
                    {
                        _player1StatusFx[cont].SetActive(true);
                        if (status.Key is BoundFx)
                        {
                            _player1StatusFx[cont].GetComponent<Image>().sprite = BoundFxSprite;
                        }
                        else if (status.Key is ParalizeFx)
                        {
                            _player1StatusFx[cont].GetComponent<Image>().sprite = ParalizeFxSprite;
                        }
                        else if (status.Key is DrowsyFx)
                        {
                            _player1StatusFx[cont].GetComponent<Image>().sprite = DrowsyFxSprite;
                        }
                        else if (status.Key is ShieldFx)
                        {
                            _player1StatusFx[cont].GetComponent<Image>().sprite = shieldFxSprite;
                        }
                        else if (status.Key is CureDebuffFx)
                        {
                            _player1StatusFx[cont].GetComponent<Image>().sprite = CureDebuffFxSprite;
                        }
                        /*else if (status.Key is RageFx)
                        {

                        }
                        else if (status.Key is OutroFx)
                        {
                        
                        }*/
                        _player1StatusFx[cont].GetComponentInChildren<TextMeshProUGUI>().text = status.Value.ToString();
                        cont++;
                    }
                    for (int i = cont + 1; i < 5; i++)
                    {
                        _player1StatusFx[i].SetActive(false);
                    }
                    break;
                case 2:
                    foreach (var status in car.currentStatus)
                    {
                        _player2StatusFx[cont].SetActive(true);
                        if (status.Key is BoundFx)
                        {
                            _player2StatusFx[cont].GetComponent<Image>().sprite = BoundFxSprite;
                        }
                        else if (status.Key is ParalizeFx)
                        {
                            _player2StatusFx[cont].GetComponent<Image>().sprite = ParalizeFxSprite;
                        }
                        else if (status.Key is DrowsyFx)
                        {
                            _player2StatusFx[cont].GetComponent<Image>().sprite = DrowsyFxSprite;
                        }
                        else if (status.Key is ShieldFx)
                        {
                            _player2StatusFx[cont].GetComponent<Image>().sprite = shieldFxSprite;
                        }
                        else if (status.Key is CureDebuffFx)
                        {
                            _player2StatusFx[cont].GetComponent<Image>().sprite = CureDebuffFxSprite;
                        }
                        /*else if (status.Key is OutroFx)
                        {
                        
                        }*/
                        _player2StatusFx[cont].GetComponentInChildren<TextMeshProUGUI>().text = status.Value.ToString();
                        cont++;
                    }
                    for (int i = cont + 1; i < 5; i++)
                    {
                        _player2StatusFx[i].SetActive(false);
                    }
                    break;
            }
        }
    }

    public void OnOneCaracter()
    {
        _caractersInfo.transform.GetChild(0).gameObject.SetActive(false);
        _caractersInfo.transform.GetChild(2).gameObject.SetActive(false);
        _selectionButtons[0].GetComponent<Button>().interactable = false;
        _selectionButtons[2].GetComponent<Button>().interactable = false;
    }
}

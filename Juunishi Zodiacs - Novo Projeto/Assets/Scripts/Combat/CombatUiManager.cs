using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatUiManager : MonoBehaviour
{
    CombatManager combatMg;
    [Header("ActionMenu")]
    [SerializeField] GameObject _actionMenu;
    [SerializeField] GameObject _attackBut;
    [SerializeField] GameObject _spellsBut;
    [SerializeField] GameObject _abilitiesMenu;
    [SerializeField] GameObject[] _magicalAttack = new GameObject[3];
    [SerializeField] GameObject[] _selectionButtons;
    
    void Start()
    {
        combatMg = CombatManager.combatInstance;
    }


    void Update()
    {
        
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
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class AbilityInformation : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _name;
    [SerializeField] Image _icon;
    Ability currentAbility;
    int abilityToChange;

    public Ability CurrentAbility { get => currentAbility; set => currentAbility = value; }

    public void UpdateInfo(PlayableCaracterScptObj carac, int index)
    {
        CurrentAbility = carac.Abilities[index];
        _name.text = carac.Abilities[index].AbilityName;

        if (carac.Abilities[index].Mods[0] is DamageModifier)
        {
            DamageModifier dm = (DamageModifier)carac.Abilities[index].Mods[0];
            switch (dm.DamageType)
            {
                case DAMAGETYPE.Physical:
                    _icon.sprite = MenuManager.instance.AbilitySprites[0];
                    break;
                case DAMAGETYPE.Fire:
                    _icon.sprite = MenuManager.instance.AbilitySprites[1];
                    break;
                case DAMAGETYPE.Water:
                    _icon.sprite = MenuManager.instance.AbilitySprites[2];
                    break;
                case DAMAGETYPE.Rock:
                    _icon.sprite = MenuManager.instance.AbilitySprites[3];
                    break;
                case DAMAGETYPE.Nature:
                    _icon.sprite = MenuManager.instance.AbilitySprites[4];
                    break;
                case DAMAGETYPE.Metal:
                    _icon.sprite = MenuManager.instance.AbilitySprites[5];
                    break;
                case DAMAGETYPE.None:
                    _icon.sprite = null;
                    break;
            }
        }
        if (carac.Abilities[index].Mods[0] is HealModifier)
        {
            _icon.sprite = MenuManager.instance.AbilitySprites[6];
        }
        if (carac.Abilities[index].Mods[0] is StatusModifier)
        {
            StatusModifier sM = (StatusModifier)carac.Abilities[index].Mods[0];
            if (sM.IsBuff)
            {
                _icon.sprite = MenuManager.instance.AbilitySprites[7];
            }
            else
            {
                _icon.sprite = MenuManager.instance.AbilitySprites[8];
            }
        }

    }

    public void UpdateInfoExtra(PlayableCaracterScptObj carac, int index)
    {

        CurrentAbility = carac.AllCaracterAbilities[index];
        _name.text = carac.AllCaracterAbilities[index].AbilityName;

        if (carac.AllCaracterAbilities[index].Mods[0] is DamageModifier)
        {
            DamageModifier dm = (DamageModifier)carac.AllCaracterAbilities[index].Mods[0];
            switch (dm.DamageType)
            {
                case DAMAGETYPE.Physical:
                    _icon.sprite = MenuManager.instance.AbilitySprites[0];
                    break;
                case DAMAGETYPE.Fire:
                    _icon.sprite = MenuManager.instance.AbilitySprites[1];
                    break;
                case DAMAGETYPE.Water:
                    _icon.sprite = MenuManager.instance.AbilitySprites[2];
                    break;
                case DAMAGETYPE.Rock:
                    _icon.sprite = MenuManager.instance.AbilitySprites[3];
                    break;
                case DAMAGETYPE.Nature:
                    _icon.sprite = MenuManager.instance.AbilitySprites[4];
                    break;
                case DAMAGETYPE.Metal:
                    _icon.sprite = MenuManager.instance.AbilitySprites[5];
                    break;
                case DAMAGETYPE.None:
                    _icon.sprite = null;
                    break;
            }
        }
        if (carac.AllCaracterAbilities[index].Mods[0] is HealModifier)
        {
            _icon.sprite = MenuManager.instance.AbilitySprites[6];
        }
        if (carac.AllCaracterAbilities[index].Mods[0] is StatusModifier)
        {
            StatusModifier sM = (StatusModifier)carac.AllCaracterAbilities[index].Mods[0];
            if (sM.IsBuff)
            {
                _icon.sprite = MenuManager.instance.AbilitySprites[7];
            }
            else
            {
                _icon.sprite = MenuManager.instance.AbilitySprites[8];
            }
        }
    }

    public void ChangeAbilityButton()
    {
        MenuManager.instance.ChangeAbility(currentAbility);
    }

    public void ShowAbilityInformationExtra()
    {
        MenuManager.instance.ShowAbilityInfo(currentAbility);
    }
}

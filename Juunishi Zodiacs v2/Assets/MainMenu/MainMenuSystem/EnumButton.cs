using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumButton : MonoBehaviour
{

    [SerializeField] GameObject _appBlock;

    public Options myOptions;
    public enum Options
    {
        MainMenu,
        Inventory,
        Contacts,
        Messages,
        Notes,
        Photos,
        Reminders,
        Options,
        Map

    }
    public void OptionsSwitch()
    {

        
            switch (myOptions)
            {
                case Options.MainMenu:
                    MenuManager.instance.Option((int)Options.MainMenu);
                _appBlock.SetActive(false);
                    break;

                case Options.Inventory:
                    MenuManager.instance.Option((int)Options.Inventory);
                _appBlock.SetActive(true);
                break;

                case Options.Contacts:
                    MenuManager.instance.Option((int)Options.Contacts);
                _appBlock.SetActive(true);
                break;

                 case Options.Messages:
                      MenuManager.instance.Option((int)Options.Messages);
                _appBlock.SetActive(true);
                break;

            case Options.Notes:
                MenuManager.instance.Option((int)Options.Notes);
                _appBlock.SetActive(true);
                break;

            case Options.Photos:
                MenuManager.instance.Option((int)Options.Photos);
                _appBlock.SetActive(true);
                break;

            case Options.Reminders:
                MenuManager.instance.Option((int)Options.Reminders);
                _appBlock.SetActive(true);
                break;

            case Options.Options:
                MenuManager.instance.Option((int)Options.Options);
                _appBlock.SetActive(true);
                break;

            case Options.Map:
                MenuManager.instance.Option((int)Options.Map);
                _appBlock.SetActive(true);
                break;


            default:
                    break;
            }

        if (myOptions == Options.MainMenu)
        {
            MenuManager.instance.CloseInventory();
            
        }

        if (myOptions == Options.Inventory)
        {
            MenuManager.instance.InventorySystem();
        }

      
       
    }
}

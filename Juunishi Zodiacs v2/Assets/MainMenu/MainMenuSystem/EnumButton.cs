using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumButton : MonoBehaviour
{

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
                    break;

                case Options.Inventory:
                    MenuManager.instance.Option((int)Options.Inventory);
                    break;

                case Options.Contacts:
                    MenuManager.instance.Option((int)Options.Contacts);
                    break;

                 case Options.Messages:
                      MenuManager.instance.Option((int)Options.Messages);
                      break;

            case Options.Notes:
                MenuManager.instance.Option((int)Options.Notes);
                break;

            case Options.Photos:
                MenuManager.instance.Option((int)Options.Photos);
                break;

            case Options.Reminders:
                MenuManager.instance.Option((int)Options.Reminders);
                break;

            case Options.Options:
                MenuManager.instance.Option((int)Options.Options);
                break;

            case Options.Map:
                MenuManager.instance.Option((int)Options.Map);
                break;


            default:
                    break;
            }
        
       
    }
}

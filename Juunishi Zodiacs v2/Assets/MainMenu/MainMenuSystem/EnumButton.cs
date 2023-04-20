using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumButton : MonoBehaviour
{

    public Options myOptions;
    public enum Options
    {
        option1,
        option2,
        option3

    }
    public void OptionsSwitch()
    {

        
            switch (myOptions)
            {
                case Options.option1:
                    MenuManager.instance.Option((int)Options.option1);
                    break;

                case Options.option2:
                    MenuManager.instance.Option((int)Options.option2);
                    break;

                case Options.option3:
                    MenuManager.instance.Option((int)Options.option3);
                    break;

                default:
                    break;
            }
        
       
    }
}

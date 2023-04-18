using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Dialogue;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject[] _mainMenuElements;
    [SerializeField] int test;

   public static MenuManager instance;

    private void Awake()
    {
        //singletone
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

        }
    }
   public void Option(int switchNumb)
    {
        test = switchNumb;
        foreach (var item in _mainMenuElements)
        {
            item.SetActive(false);
        }
        _mainMenuElements[switchNumb].SetActive(true);
    }
}

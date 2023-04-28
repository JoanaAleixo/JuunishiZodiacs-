using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Dialogue;

public class MenuManager : MonoBehaviour
{
    [Header("MainMenu")]
    [SerializeField] GameObject[] _mainMenuElements;

    [Header("Inventory")]
    Dictionary<ScriptableItem, int> _inventory = new Dictionary<ScriptableItem, int>();


    public static MenuManager instance;

    public Dictionary<ScriptableItem, int> Inventory { get => _inventory; set => _inventory = value; }

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
       
        foreach (var item in _mainMenuElements)
        {
            item.SetActive(false);
        }
        _mainMenuElements[switchNumb].SetActive(true);
    }

  public void InventorySystem()
    {

    }
}

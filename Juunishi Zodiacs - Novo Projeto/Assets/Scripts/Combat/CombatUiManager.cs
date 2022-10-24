using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUiManager : MonoBehaviour
{
    [SerializeField] GameObject _abilitiesUi;
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void OpenAbilitiesMenu()
    {
        _abilitiesUi.SetActive(true);
    }
}

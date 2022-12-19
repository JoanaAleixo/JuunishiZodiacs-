using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CaractersInformation : MonoBehaviour
{
    [SerializeField] ActiveCaracters _allCaracters;
    [SerializeField] int caracterNumber;
    [SerializeField] private CaracterCreation myCaracter;
    [Header("References")]
    [SerializeField] GameObject _nameTag;

    public CaracterCreation MyCaracter { get => myCaracter; set => myCaracter = value; }

    void Start()
    {
        MyCaracter = _allCaracters.ActiveCaractersInGame[caracterNumber];
        _nameTag.GetComponentInChildren<TextMeshProUGUI>().text = MyCaracter.Name;
    }


    void Update()
    {
        
    }
}

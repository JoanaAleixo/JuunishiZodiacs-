using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HpUpdate : MonoBehaviour
{
    [SerializeField] CaracterCreation thisCaracter;
    [SerializeField] Image img;
    [SerializeField] TextMeshProUGUI text;
    void Start()
    {
        //thisCaracter = gameObject.GetComponentInParent<CaractersInformation>().MyCaracter;
    }

    void Update()
    {
        
    }

    public void UpdateHpUI()
    {
        thisCaracter = gameObject.GetComponentInParent<CaractersInformation>().MyCaracter;
        //gameObject.GetComponentInChildren<Image>().fillAmount = thisCaracter.HpMax.value / 100;
        img.fillAmount = thisCaracter.HpMax.value / 100;
        text.text = thisCaracter.HpMax.value.ToString();
    }

}

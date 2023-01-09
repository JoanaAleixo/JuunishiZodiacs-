using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HpUpdate : MonoBehaviour
{
    [SerializeField] CaracterCreation thisCaracter;
    [SerializeField] Image hpImg;
    [SerializeField] Image spImg;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI spText;
    void Start()
    {
        //thisCaracter = gameObject.GetComponentInParent<CaractersInformation>().MyCaracter;
    }

    void Update()
    {
        
    }

    public void EnemyUpdateUI()
    {
        hpImg.fillAmount = thisCaracter.HpMax.value / 100;
    }

    public void UpdateUI()
    {
        if(thisCaracter == null)
        {
            thisCaracter = gameObject.GetComponent<CaractersInformation>().MyCaracter;
        }
        //gameObject.GetComponentInChildren<Image>().fillAmount = thisCaracter.HpMax.value / 100;
        hpImg.fillAmount = thisCaracter.HpMax.value / 100;
        spImg.fillAmount = thisCaracter.HpMax.value / 100;
        hpText.text = thisCaracter.HpMax.value.ToString();
        spText.text = thisCaracter.HpMax.value.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HpUpdate : MonoBehaviour
{
    [SerializeField] CaracterCreation thisCaracter;
    [SerializeField] PlayableCaracterScptObj caracterIns;
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
        if (thisCaracter == null && caracterIns == null)
        {
            thisCaracter = gameObject.GetComponent<CaractersInformation>().MyCaracter;
            caracterIns = (PlayableCaracterScptObj)thisCaracter;
        }
        //gameObject.GetComponentInChildren<Image>().fillAmount = thisCaracter.HpMax.value / 100;
        hpImg.fillAmount = caracterIns.HpMax.value / 100;
        spImg.fillAmount = caracterIns.SpMax.value / 100;
        hpText.text = caracterIns.HpMax.value.ToString();
        spText.text = caracterIns.SpMax.value.ToString();
    }
}

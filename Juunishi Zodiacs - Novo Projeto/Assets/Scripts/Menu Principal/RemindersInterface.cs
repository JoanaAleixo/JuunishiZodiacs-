using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RemindersInterface : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI mensagemInformativa;
    public void Missao(string missao)
    {
        mensagemInformativa.text = missao;
    }
}

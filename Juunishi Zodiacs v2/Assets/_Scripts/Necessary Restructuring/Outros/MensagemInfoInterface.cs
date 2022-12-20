using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MensagemInfoInterface : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI mensagemInformativa;

    public void Mensagem(Item infoItem)
    {
        mensagemInformativa.text = infoItem.MensagemInformativa;
        Destroy(gameObject, 3f);
    }

    public void Missao(string missao)
    {
        mensagemInformativa.text = missao;
        MainMenuManager.Instance.Reminders(missao);
        //Destroy(gameObject, 3f);
    }

}

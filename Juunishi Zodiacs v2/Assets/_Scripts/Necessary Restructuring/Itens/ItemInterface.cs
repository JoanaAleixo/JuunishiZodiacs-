using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInterface : MonoBehaviour
{
    [SerializeField] Image iconItem;
    [SerializeField] TextMeshProUGUI nomeItem;
    Item item;
    [SerializeField] TextMeshProUGUI descricaoItem;

    private void Start()
    {
        //melhor linha de código da minha vida:
        descricaoItem = GameObject.Find("/Canvas/Interface Menu Principal/Telemóvel/Interfaces Apps/Bag Interface/Caixa/Text (TMP)").GetComponent<TextMeshProUGUI>();
    }

    public void MudarIconENome(Item infoItem)
    {
        item = infoItem;
        iconItem.sprite = infoItem.Icon;
        nomeItem.text = infoItem.NomeItem;
    }

    public void DescricaoItemBotao ()
    {
        descricaoItem.text = item.Descricao;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemInfo")]

public class Item : ScriptableObject
{
    [SerializeField] int itemID;
    [SerializeField] ItemCategoria categoria;
    [SerializeField] Sprite icon;
    [SerializeField] string nomeItem;
    [SerializeField] string descricao;

    [SerializeField] string mensagemInformativa; 

    internal ItemCategoria Categoria { get => categoria; set => categoria = value; }
    public Sprite Icon { get => icon; set => icon = value; }
    public string NomeItem { get => nomeItem; set => nomeItem = value; }
    public string MensagemInformativa { get => mensagemInformativa; set => mensagemInformativa = value; }
    public string Descricao { get => descricao; set => descricao = value; }
    public int ItemID { get => itemID; set => itemID = value; }
}

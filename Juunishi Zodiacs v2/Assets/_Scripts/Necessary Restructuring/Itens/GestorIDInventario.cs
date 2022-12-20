using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GestorIDInventario
{
    //guarda os IDS de todos os itens

    [SerializeField] List<int> listaItemsIDs = new List<int>();

    public List<int> ListaItemsIDs { get => listaItemsIDs; set => listaItemsIDs = value; }

    public GestorIDInventario(List<int> items)
    {
        ListaItemsIDs = items;
    }
}

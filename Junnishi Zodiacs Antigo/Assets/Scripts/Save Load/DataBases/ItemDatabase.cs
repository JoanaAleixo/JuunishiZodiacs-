using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class ItemDatabase : ScriptableObject
{
    [SerializeField] List<Item> listaItems = new List<Item>();

    //criar um scriptable object onde se vao por todos os itens do jogo

    public List<Item> ListaItems { get => listaItems; set => listaItems = value; }
}

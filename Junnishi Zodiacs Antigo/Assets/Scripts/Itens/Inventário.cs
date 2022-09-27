using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventário : MonoBehaviour
{
    [SerializeField] List <Item> listaCoisasInventario = new List<Item>();
    

    public List<Item> ListaCoisasInventario { get => listaCoisasInventario; set => listaCoisasInventario = value; }


    void Start()
    {
        
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            LimparInventario();
        }
    }

    public void AdicionarItemInventario(Item itemAdcionar)
    {
        listaCoisasInventario.Add(itemAdcionar);
        MainMenuManager.Instance.AdicionarItemInventario(itemAdcionar);
    }

    public void LimparInventario()
    {
        listaCoisasInventario.Clear(); //tira as coisas da lista do inventario
        foreach (Transform child in transform) //por cada criança deste transform
        {
            Destroy(child.gameObject);
        }
    }
}

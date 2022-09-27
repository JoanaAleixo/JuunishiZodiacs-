using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    static DatabaseManager instance;

    [SerializeField] ItemDatabase itemDatabase;
    [SerializeField] DialogueDatabase dialogoDatabase;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }

    public Item BuscaItemPorID (int IDaProcurar)
    {
        foreach (var item in itemDatabase.ListaItems)
        {
            if (item.ItemID == IDaProcurar)
                return item;
        }
        return null;
    }

    public DialogueTree BuscaDialogoPorID (int IDaProcurar)
    {
        foreach (var dialogo in dialogoDatabase.ListaDialogos)
        {
            if(dialogo.DialogoID == IDaProcurar) 
            {
                return dialogo;
            }
        }
        return null;
    }

}

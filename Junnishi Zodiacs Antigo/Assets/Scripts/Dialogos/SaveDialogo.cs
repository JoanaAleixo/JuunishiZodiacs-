using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveDialogo
{
    //salva data do dialogo 
    [SerializeField] int dialogoGuardadoID;
    [SerializeField] int posiçãoNoDialogoGuardado;
    [SerializeField] string nomeDialogueManagerGuardado;

    public SaveDialogo(DialogueTree arvoreDialogo, int dialogoID, int posicaoDialogo, string nomeDialogueManager)
    {
        DialogoGuardadoID = dialogoID;
        PosiçãoNoDialogoGuardado = posicaoDialogo;
        NomeDialogueManagerGuardado = nomeDialogueManager;
    }

    public int DialogoGuardadoID { get => dialogoGuardadoID; set => dialogoGuardadoID = value; }
    public int PosiçãoNoDialogoGuardado { get => posiçãoNoDialogoGuardado; set => posiçãoNoDialogoGuardado = value; }
    public string NomeDialogueManagerGuardado { get => nomeDialogueManagerGuardado; set => nomeDialogueManagerGuardado = value; }
    
}

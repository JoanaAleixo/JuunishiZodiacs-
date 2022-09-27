using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveDialogo
{
    //salva data do dialogo 
    [SerializeField] int dialogoGuardadoID;
    [SerializeField] int posi��oNoDialogoGuardado;
    [SerializeField] string nomeDialogueManagerGuardado;

    public SaveDialogo(DialogueTree arvoreDialogo, int dialogoID, int posicaoDialogo, string nomeDialogueManager)
    {
        DialogoGuardadoID = dialogoID;
        Posi��oNoDialogoGuardado = posicaoDialogo;
        NomeDialogueManagerGuardado = nomeDialogueManager;
    }

    public int DialogoGuardadoID { get => dialogoGuardadoID; set => dialogoGuardadoID = value; }
    public int Posi��oNoDialogoGuardado { get => posi��oNoDialogoGuardado; set => posi��oNoDialogoGuardado = value; }
    public string NomeDialogueManagerGuardado { get => nomeDialogueManagerGuardado; set => nomeDialogueManagerGuardado = value; }
    
}

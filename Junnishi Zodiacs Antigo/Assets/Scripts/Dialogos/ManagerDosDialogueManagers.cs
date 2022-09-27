using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerDosDialogueManagers : MonoBehaviour
{
    [SerializeField] DialogueManager dialogueManagerGuardado;
    [SerializeField] string dialogueManagerNome;

    [SerializeField] List<DialogueManager> listaDeTodosOsDialogManagers = new List<DialogueManager>();
    [SerializeField] GameObject interfaceTextoPart1;
    [SerializeField] GameObject interfaceTextoPart2;
    [SerializeField] GameObject canvasPart1;
    [SerializeField] GameObject canvasPart2;

    public DialogueManager DialogueManagerGuardado { get => dialogueManagerGuardado; set => dialogueManagerGuardado = value; }

    //por cada elemento da lista, verificar se é igual ao guardado
    //o que for igual é para ativar;
    //o que não for igual é para desativar
    //consoante o numero do elemento guardado no index da Lista
    //ligar a respetiva interface

    public void GuardarABagacaDoDialogueManager(DialogueManager dialogueManagerGuardado)
    {
        DialogueManagerGuardado = dialogueManagerGuardado;
    }


    public void AtivarDialogo(string dialogueManagerGuardadoNome)
    {
        foreach (var item in listaDeTodosOsDialogManagers)
        {
            if(item.gameObject.name == dialogueManagerGuardadoNome)
            {
                item.gameObject.SetActive(true);
                item.ComeçarDialogo();
                //item.ContinuarDialogo();
            }
            else
            {
                item.gameObject.SetActive(false);
            }
        }
    }
}

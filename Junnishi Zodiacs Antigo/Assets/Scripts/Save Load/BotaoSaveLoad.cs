using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoSaveLoad : MonoBehaviour
{
    //responsavel por comandar quando fazer save ou load

    [SerializeField] DatabaseManager dataManager;
    [SerializeField] ManagerDosDialogueManagers dialogManagersManager;

    [SerializeField] List<Item> listaItems = new List<Item>();
    [SerializeField] GameObject content;
    [SerializeField] Inventário inventario;

    [SerializeField] DialogueTree dialogoGuardado;
    [SerializeField] int dialogoGuardadoID;
    [SerializeField] int posiçãoNoDialogoGuardado;
    [SerializeField] DialogueManager dialogueManagerGuardado;
    [SerializeField] GameObject[] arrayDialogueManagers;
    
    public void SaveBotao()
    {
        #region Save Item
        List<int> listaItemsIDs = new List<int>();
        foreach (Item item in inventario.ListaCoisasInventario)
        {
            listaItemsIDs.Add(item.ItemID);
        }
        GestorIDInventario novoInventario = new GestorIDInventario(listaItemsIDs);
        SaveLoadManager.SaveInventario(novoInventario);
        #endregion

        #region Save Dialogo
        //Para guardar dialogos:
        //-aceder ao dialogue manager que está ativo no momento
        //-enviar a dialogue tree que está ativa no dialogue manager e guardar
        //-aceder à sua posição atual do dialogo
        //- salvar a posição

        arrayDialogueManagers = GameObject.FindGameObjectsWithTag("Dialogue");
        //obrigada Unity Doc por esta linha de código abençoada pelos deuses da programação *-*

        foreach (var item in arrayDialogueManagers)
        {
            if (item.activeSelf)
            {
                dialogueManagerGuardado = item.GetComponent<DialogueManager>(); //guarda o Dialogue Manager
                dialogoGuardado = dialogueManagerGuardado.ArvoreDialogo; //guarda o Dialogue Tree
                dialogoGuardadoID = dialogoGuardado.DialogoID; //guarda o ID do Dialogue Tree
                posiçãoNoDialogoGuardado = dialogueManagerGuardado.PosicaoNoDialogo;
                //guarda a posição do dialogo do Dialogue Tree

                //sinceramente nem sei como isto funciona mas importa é funcionar XD
            }
        }

        SaveDialogo novoSaveDialogo = new SaveDialogo(dialogoGuardado,dialogoGuardadoID,posiçãoNoDialogoGuardado, dialogueManagerGuardado.gameObject.name);

        dialogManagersManager.GuardarABagacaDoDialogueManager(dialogueManagerGuardado);

        SaveLoadManager.SalvarDialogo(novoSaveDialogo);
        #endregion

    }

    public void LoadBotao()
    {
        #region Load Item
        inventario.LimparInventario();
        GestorIDInventario inventarioLoad = SaveLoadManager.LoadInventario();
        Item itemAdicionar = null;
        foreach(int itemID in inventarioLoad.ListaItemsIDs)
        {
            itemAdicionar = dataManager.BuscaItemPorID(itemID);
            inventario.AdicionarItemInventario(itemAdicionar);
        }
        #endregion

        #region Load Dialogo
        SaveDialogo dialogoLoad = SaveLoadManager.LoadDialogo();
        //dialogoGuardado = dialogoLoad.DialogoGuardado; //<-
        Debug.Log(dialogoLoad.NomeDialogueManagerGuardado);
        dialogoGuardadoID = dialogoLoad.DialogoGuardadoID;
        dialogoGuardado = dataManager.BuscaDialogoPorID(dialogoGuardadoID);
        posiçãoNoDialogoGuardado = dialogoLoad.PosiçãoNoDialogoGuardado;

        //Debug.Log(dialogueManagerGuardado);

        //dialogueManagerGuardado = dialogoLoad.DialogueManagerGuardado; //<-
        dialogueManagerGuardado = dialogManagersManager.DialogueManagerGuardado;
        dialogManagersManager.AtivarDialogo(dialogoLoad.NomeDialogueManagerGuardado);

        #endregion 

    }
}

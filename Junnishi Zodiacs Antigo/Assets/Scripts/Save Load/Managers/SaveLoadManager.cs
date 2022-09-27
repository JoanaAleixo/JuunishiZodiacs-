using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoadManager 
{
    
    #region Inventario
    public static void SaveInventario (GestorIDInventario inventarioParaSalvar)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter(); //formatador do binário
        string localizacao = Application.persistentDataPath + "/InventarioSalvo.cs"; //localização onde a informação ficará guardada
        FileStream fileStream = new FileStream(localizacao, FileMode.Create); //criar a localização
        Debug.Log("Inventario salvo");
        binaryFormatter.Serialize(fileStream, inventarioParaSalvar);
        fileStream.Close(); //<--- nunca esquecer desta bagaça
    }

    public static GestorIDInventario LoadInventario()
    {
        string localizacao = Application.persistentDataPath + "/InventarioSalvo.cs";

        if(File.Exists(localizacao))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(localizacao, FileMode.Open); //abrir o ficheiro criado
            GestorIDInventario itemDataToLoad = (GestorIDInventario)binaryFormatter.Deserialize(fileStream);
            Debug.Log("Inventario Carregado");
            fileStream.Close();
            return itemDataToLoad;
        }
        else
        {
            Debug.LogError("Este ficheiro não existe");
            return null;
        }
    }
    #endregion

    #region Dialogo
    public static void SalvarDialogo (SaveDialogo dialogoASalvar)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string localizacao = Application.persistentDataPath + "/DialogoSalvo.cs";
        FileStream fileStream = new FileStream(localizacao, FileMode.Create);
        binaryFormatter.Serialize(fileStream, dialogoASalvar);
        Debug.Log("Dialogo Salvo");
        fileStream.Close();
    }

    public static SaveDialogo LoadDialogo()
    {
        string localizacao = Application.persistentDataPath + "/DialogoSalvo.cs";
        if(File.Exists(localizacao))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(localizacao, FileMode.Open);

            SaveDialogo dialogoLoad = (SaveDialogo)binaryFormatter.Deserialize(fileStream);
            Debug.Log("Dialogo Carregado");
            fileStream.Close();

            return dialogoLoad;
        }
        else
        {
            Debug.LogError("Este ficheiro nao existe");
            return null;
        }
    }


    #endregion
}

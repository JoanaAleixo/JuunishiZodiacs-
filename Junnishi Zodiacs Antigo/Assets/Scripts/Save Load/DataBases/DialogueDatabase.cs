using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DialogueDatabase : ScriptableObject
{
    [SerializeField] List<DialogueTree> listaDialogos = new List<DialogueTree>();

    public List<DialogueTree> ListaDialogos { get => listaDialogos; set => listaDialogos = value; }
}

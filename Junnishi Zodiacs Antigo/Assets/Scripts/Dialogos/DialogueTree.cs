using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Dialogos")]

public class DialogueTree : ScriptableObject
{
    [SerializeField] int dialogoID;
    public int DialogoID { get => dialogoID; set => dialogoID = value; }


    //Struct
    [SerializeField] private DialogoInfo[] dialogue;
    public DialogoInfo[] Dialogue { get => dialogue; set => dialogue = value; }

    [System.Serializable]
    public struct DialogoInfo
    {
        [SerializeField] string speakerNome;
        [SerializeField] string fala;
        [SerializeField] Sprite personagemImagem;
        [SerializeField] Sprite caixaDialogo;

        public string SpeakerNome { get => speakerNome; set => speakerNome = value; }
        public string Fala { get => fala; set => fala = value; }
        public Sprite PersonagemImagem { get => personagemImagem; set => personagemImagem = value; }
        public Sprite CaixaDialogo { get => caixaDialogo; set => caixaDialogo = value; }

    }

}


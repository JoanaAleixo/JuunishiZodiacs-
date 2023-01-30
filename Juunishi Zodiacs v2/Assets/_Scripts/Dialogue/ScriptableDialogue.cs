using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Dialogues")]
public class ScriptableDialogue : ScriptableObject
{

    #region Variaveis

    [SerializeField] Sprite _background;
    [SerializeField] Sprite _backgroundName;

    [SerializeField] Dialogue[] _dialogueStr;

    [Header("Create A Brench")]
    [SerializeField] bool _changeBrench = false;

    [Header("Brench1")]
    [SerializeField] int _choiseDialogueToChange1;
    [SerializeField] string _question1;
    [SerializeField] int _TrustValueToIncrese1;

    [Header("Brench2")]
    [SerializeField] int _choiseDialogueToChange2;
    [SerializeField] string _question2;
    [SerializeField] int _TrustValueToIncrese2;

    [Header("Brench3")]
    [SerializeField] int _choiseDialogueToChange3;
    [SerializeField] string _question3;
    [SerializeField] int _TrustValueToIncrese3;

    [SerializeField] bool _isEndDialogue;

    #endregion

    #region Propriedades
    public Dialogue[] DialogueStr { get => _dialogueStr; set => _dialogueStr = value; }
    public Sprite Background { get => _background; set => _background = value; }
    public Sprite BackgroundName { get => _backgroundName; set => _backgroundName = value; }
    public bool ChangeBrench { get => _changeBrench; set => _changeBrench = value; }
    public int ChoiseDialogueToChange1 { get => _choiseDialogueToChange1; set => _choiseDialogueToChange1 = value; }
    public int ChoiseDialogueToChange2 { get => _choiseDialogueToChange2; set => _choiseDialogueToChange2 = value; }
    public int ChoiseDialogueToChange3 { get => _choiseDialogueToChange3; set => _choiseDialogueToChange3 = value; }
    public string Question1 { get => _question1; set => _question1 = value; }
    public string Question2 { get => _question2; set => _question2 = value; }
    public string Question3 { get => _question3; set => _question3 = value; }
    public int TrustValueToIncrese1 { get => _TrustValueToIncrese1; set => _TrustValueToIncrese1 = value; }
    public int TrustValueToIncrese2 { get => _TrustValueToIncrese2; set => _TrustValueToIncrese2 = value; }
    public int TrustValueToIncrese3 { get => _TrustValueToIncrese3; set => _TrustValueToIncrese3 = value; }
    public bool IsEndDialogue { get => _isEndDialogue; set => _isEndDialogue = value; }

    #endregion
}

#region Struct com a criação dos dialogos
[Serializable]
public struct Dialogue
{

    [Header("Character Name")]
    [SerializeField] CharacterScriptbleObject myCharacter;

    [Header("Dialogue Text")]
    [SerializeField] string[] _dialogueMessages;

    
    int _toPositionCharacter;

    public CharacterDisplayExpression myExpressions;
    public CharacterFullBodyExpression myFullbody;
    public FullBodyPositions myPositions;

    #region Enums de Expressoes
    public enum CharacterDisplayExpression
    {
        Happy,
        Surprise,
        Nervous,
        Sad,
        Crying,
        Hangry,
        Serious,
        VeryHappy,
        Extra

    }

    public enum CharacterFullBodyExpression
    {
        Happy,
        Surprise,
        Nervous,
        Sad,
        Crying,
        Hangry,
        Serious,
        VeryHappy,
        Extra
    }

    public enum FullBodyPositions
    {
        Position1,
        Position2,
        Position3,
        Position4,
        Position5
    }

    #endregion

    public CharacterScriptbleObject MyCharacter { get => myCharacter; set => myCharacter = value; }
    public string[] DialogueMessages { get => _dialogueMessages; set => _dialogueMessages = value; }

    public int ToPositionCharacter { get => _toPositionCharacter; set => _toPositionCharacter = value; }
    
}

#endregion
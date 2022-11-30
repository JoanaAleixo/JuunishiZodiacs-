using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Dialogs")]
public class ScriptableDialog : ScriptableObject
{
    [SerializeField] Sprite _background;

    [SerializeField] Dialogue[] _dialogueGroup;
  
    public Dialogue[] DialogueStr { get => _dialogueGroup; set => _dialogueGroup = value; }
    public Sprite Background { get => _background; set => _background = value; }

    //Arrey de dialogos, dialogos vão ser escritos neste arrey

}


[Serializable]
public struct Dialogue
{
   

    [SerializeField] CharacterScriptbleObject myCharacter;
    [SerializeField] string[] _dialogueMessages;
   
    [SerializeField] int _characterExpressionNumber;
    [SerializeField] int _characterFullBodyNumber;
    [SerializeField] int _toPositionCharacter;

   
    public CharacterScriptbleObject MyCharacter { get => myCharacter; set => myCharacter = value; }
    public string[] DialogueMessages { get => _dialogueMessages; set => _dialogueMessages = value; }
    public int CharacterExpressionNumber { get => _characterExpressionNumber; set => _characterExpressionNumber = value; }
    public int CharacterFullBodyNumber { get => _characterFullBodyNumber; set => _characterFullBodyNumber = value; }
    public int ToPositionCharacter { get => _toPositionCharacter; set => _toPositionCharacter = value; }
}
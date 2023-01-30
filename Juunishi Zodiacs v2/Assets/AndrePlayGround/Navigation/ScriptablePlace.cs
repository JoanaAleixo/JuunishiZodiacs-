using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Places")]
public class ScriptablePlace : ScriptableObject
{
    #region Variaveis
    [SerializeField] Sprite _background;
    [SerializeField] Sprite _namePlace;

    [SerializeField] DislocationButtons[] _dislocationStr;

    #endregion

    #region Propriedades
    public Sprite Background { get => _background; set => _background = value; }
    public Sprite NamePlace { get => _namePlace; set => _namePlace = value; }
    public DislocationButtons[] DislocationStr { get => _dislocationStr; set => _dislocationStr = value; }
    #endregion
}

#region Struct
[Serializable]
public struct DislocationButtons
{
    
    [SerializeField] string _buttonDirectionName;
    
    [SerializeField] int _nextPlaceValue;

    [SerializeField] Color _textColor;

    [SerializeField] Vector3 _buttonPosition;

    [SerializeField] bool _hasDialogue;

    [SerializeField] ScriptableDialogue _dialogueToPlace;

    public string ButtonDirectionName { get => _buttonDirectionName; set => _buttonDirectionName = value; }
    public int NextPlaceValue { get => _nextPlaceValue; set => _nextPlaceValue = value; }
    public Color TextColor { get => _textColor; set => _textColor = value; }
    public Vector3 ButtonPosition { get => _buttonPosition; set => _buttonPosition = value; }
    public bool HasDialogue { get => _hasDialogue; set => _hasDialogue = value; }
    public ScriptableDialogue DialogueToPlace { get => _dialogueToPlace; set => _dialogueToPlace = value; }
}
#endregion
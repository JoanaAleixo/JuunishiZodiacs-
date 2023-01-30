using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character")]
public class CharacterScriptbleObject : ScriptableObject
{
    #region Variaveis

    [SerializeField] Font _font; //fonte do texto
    [SerializeField] Font _characterFont; //fonte do texto do nome do personagem
    [SerializeField] Color _myNameColor; //cor do nome
    [SerializeField] Color _myDialogColor; //cor do dialogo
    [SerializeField] string _characterName; //Nome do Personagem
    [SerializeField] Sprite _backGround; //background do texto 
    [SerializeField] Sprite[] _characterDisplayExpressions; //Arrey das expressoes do personagem no texto
    [SerializeField] Sprite[] _fullBodyPoses; //Arrey de Fullbodys

    #endregion

    #region Propriedades

    public string CharacterName { get => _characterName; set => _characterName = value; }
    public Font Font { get => _font; set => _font = value; }
    public Color MyNameColor { get => _myNameColor; set => _myNameColor = value; }
    public Color MyDialogColor { get => _myDialogColor; set => _myDialogColor = value; }
    public Sprite BackGround { get => _backGround; set => _backGround = value; }
    public Sprite[] CharacterDisplayExpressions { get => _characterDisplayExpressions; set => _characterDisplayExpressions = value; }
    public Sprite[] FullBodyPoses { get => _fullBodyPoses; set => _fullBodyPoses = value; }
    public Font CharacterFont { get => _characterFont; set => _characterFont = value; }

    #endregion
}

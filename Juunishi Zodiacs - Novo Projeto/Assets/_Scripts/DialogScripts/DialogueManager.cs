using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] ScriptableDialog[] myDialogTree;
  
     int _dialogNumber;
     int _dialogTreeNumber;
     int _positionInDialog;

    string DialogToDisplay;
    string CharacterName;
    Font font;
    Color NameColor;
    Color DialogColor;
    Sprite SpriteBackground;
    Sprite ExpressionsToDisplay;
    Sprite FullBodyToDisplay;
    Sprite Background;
    int FullBodyPosition;

    private void Awake()
    {
        _positionInDialog = 0;
        _dialogNumber = 0;
        _dialogTreeNumber = 0;



        CharacterName = myDialogTree[_dialogTreeNumber].DialogueStr[_dialogNumber].MyCharacter.CharacterName;
        font = myDialogTree[_dialogTreeNumber].DialogueStr[_dialogNumber].MyCharacter.Font;
        NameColor = myDialogTree[_dialogTreeNumber].DialogueStr[_dialogNumber].MyCharacter.MyNameColor;
        DialogColor = myDialogTree[_dialogTreeNumber].DialogueStr[_dialogNumber].MyCharacter.MyDialogColor;
        SpriteBackground = myDialogTree[_dialogTreeNumber].DialogueStr[_dialogNumber].MyCharacter.BackGround;

        ExpressionsToDisplay = myDialogTree[_dialogTreeNumber].DialogueStr[_dialogNumber].MyCharacter.CharacterDisplayExpressions[myDialogTree[_dialogTreeNumber].DialogueStr[_dialogNumber].CharacterExpressionNumber];

        Background = myDialogTree[_dialogTreeNumber].Background;

        UIManager.instance.DialogOnScrene(CharacterName, font, NameColor, DialogColor, SpriteBackground, ExpressionsToDisplay, Background);
       

        DialogToDisplay = myDialogTree[_dialogTreeNumber].DialogueStr[_dialogNumber].DialogueMessages[_positionInDialog];

        UIManager.instance.PlayCoroutine(DialogToDisplay);

        FullBodyToDisplay = myDialogTree[_dialogTreeNumber].DialogueStr[_dialogNumber].MyCharacter.FullBodyPoses[myDialogTree[_dialogTreeNumber].DialogueStr[_dialogNumber].CharacterFullBodyNumber];

        FullBodyPosition = myDialogTree[_dialogTreeNumber].DialogueStr[_dialogNumber].ToPositionCharacter;

        UIManager.instance.CharactersOnDisplay(FullBodyToDisplay, FullBodyPosition);
    }

    private void Update()
    { 
       
    }


  public  void ChangeDialog()
    {
       
            _positionInDialog++;


            if (_positionInDialog >= myDialogTree[_dialogTreeNumber].DialogueStr[_dialogNumber].DialogueMessages.Length)
            {
                _positionInDialog = 0;

                _dialogNumber++;

                if (_dialogNumber >= myDialogTree[_dialogTreeNumber].DialogueStr.Length)
                {
                    _dialogNumber = 0;

                    _dialogTreeNumber++;

                    if (_dialogTreeNumber >= myDialogTree.Length)
                    {
                        _dialogTreeNumber = 0;

                    }
                }
            }


            CharacterName = myDialogTree[_dialogTreeNumber].DialogueStr[_dialogNumber].MyCharacter.CharacterName;
            font = myDialogTree[_dialogTreeNumber].DialogueStr[_dialogNumber].MyCharacter.Font;
            NameColor = myDialogTree[_dialogTreeNumber].DialogueStr[_dialogNumber].MyCharacter.MyNameColor;
            DialogColor = myDialogTree[_dialogTreeNumber].DialogueStr[_dialogNumber].MyCharacter.MyDialogColor;
            SpriteBackground = myDialogTree[_dialogTreeNumber].DialogueStr[_dialogNumber].MyCharacter.BackGround;

        ExpressionsToDisplay = myDialogTree[_dialogTreeNumber].DialogueStr[_dialogNumber].MyCharacter.CharacterDisplayExpressions[myDialogTree[_dialogTreeNumber].DialogueStr[_dialogNumber].CharacterExpressionNumber];

        DialogToDisplay = myDialogTree[_dialogTreeNumber].DialogueStr[_dialogNumber].DialogueMessages[_positionInDialog];

        Background = myDialogTree[_dialogTreeNumber].Background;


        FullBodyToDisplay = myDialogTree[_dialogTreeNumber].DialogueStr[_dialogNumber].MyCharacter.FullBodyPoses[myDialogTree[_dialogTreeNumber].DialogueStr[_dialogNumber].CharacterFullBodyNumber];

        FullBodyPosition = myDialogTree[_dialogTreeNumber].DialogueStr[_dialogNumber].ToPositionCharacter;

        UIManager.instance.CharactersOnDisplay(FullBodyToDisplay, FullBodyPosition);

        UIManager.instance.DialogOnScrene(CharacterName, font, NameColor, DialogColor, SpriteBackground, ExpressionsToDisplay, Background);
            UIManager.instance.EndCoroutine();
            UIManager.instance.PlayCoroutine(DialogToDisplay);
        
    }
}

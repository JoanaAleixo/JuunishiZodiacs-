using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using static Dialogue;

public class DialogueManager : MonoBehaviour
{
    #region Variaveis
    //Arrey com os conjuntos de dialogos que sao feitos com Scriptable Objects
    [SerializeField] ScriptableDialogue[] myDialogTree;

    //boolan que dita caso haja ramificação no dialogo ou nao
    [SerializeField] bool _dialogueCanChange = true;
    [SerializeField] GameObject _chosesButtons;

    //Dialogue Position 
    [SerializeField] int _dialogNumber;
    [SerializeField] int _positionInDialog;
    [SerializeField] int _dialogTreeNumber;
    
    //Encapsuladores de Informação dos Scriptable Objects
    string DialogToDisplay;
    string CharacterName;
    Font font;
    Color NameColor;
    Color DialogColor;
    Sprite SpriteBackground;
    Sprite Background;
    Sprite NameBackground;

    //Variaveis de Expressoes
    Sprite ExpressionsToDisplay;
    int _expressionNumber;

    //Variaveis do Fullbody
    Sprite FullBodyToDisplay;
    int _fullbodyExpressionNumber;

    //Variaveis das Positions do Fullbody
     int FullBodyPosition;
     int _fullBodyPositionsNumber;

    //Referencia ao Scriptable Object com os dialogos
    ScriptableDialogue DialogueTree;

    //questoes para ui

    string _questionToUi1;
    string _questionToUi2;
    string _questionToUi3;

    //nivel de confiança do vilao
    [SerializeField] int _trustValue;

<<<<<<< HEAD:Juunishi Zodiacs ver 2/Assets/_Scripts/Dialogue/DialogueManager.cs
=======
    [SerializeField] DialogUIManager _diaUiManager;

>>>>>>> ToAFicarDoidaComOSourcetree:Juunishi Zodiacs - Novo Projeto/Assets/_Scripts/Dialogue/DialogueManager.cs
    public int TrustValue { get => _trustValue; set => _trustValue = value; }
    

    #endregion

    #region Awake
    private void Awake()
    {
        _positionInDialog = 0;
        _dialogNumber = 0;
        _dialogTreeNumber = 0;

<<<<<<< HEAD:Juunishi Zodiacs ver 2/Assets/_Scripts/Dialogue/DialogueManager.cs
=======

>>>>>>> ToAFicarDoidaComOSourcetree:Juunishi Zodiacs - Novo Projeto/Assets/_Scripts/Dialogue/DialogueManager.cs
        UpdateOnUI();

    }

    #endregion

    #region Enums das Expressoes
    void ExpressionsSwitch()
    {
       
        switch (myDialogTree[_dialogTreeNumber].DialogueStr[_dialogNumber].myExpressions)
        {
            case CharacterDisplayExpression.Happy:

<<<<<<< HEAD:Juunishi Zodiacs ver 2/Assets/_Scripts/Dialogue/DialogueManager.cs
                _expressionNumber = (int)CharacterDisplayExpression.Happy;
=======
                _fullbodyExpressionNumber = (int)CharacterDisplayExpression.Happy;
>>>>>>> ToAFicarDoidaComOSourcetree:Juunishi Zodiacs - Novo Projeto/Assets/_Scripts/Dialogue/DialogueManager.cs
                break;

            case CharacterDisplayExpression.Surprise:

<<<<<<< HEAD:Juunishi Zodiacs ver 2/Assets/_Scripts/Dialogue/DialogueManager.cs
                _expressionNumber = (int)CharacterDisplayExpression.Surprise;
=======
                _fullbodyExpressionNumber = (int)CharacterDisplayExpression.Surprise;
>>>>>>> ToAFicarDoidaComOSourcetree:Juunishi Zodiacs - Novo Projeto/Assets/_Scripts/Dialogue/DialogueManager.cs
                break;

            case CharacterDisplayExpression.Nervous:

<<<<<<< HEAD:Juunishi Zodiacs ver 2/Assets/_Scripts/Dialogue/DialogueManager.cs
                _expressionNumber = (int)CharacterDisplayExpression.Nervous;
=======
                _fullbodyExpressionNumber = (int)CharacterDisplayExpression.Nervous;
>>>>>>> ToAFicarDoidaComOSourcetree:Juunishi Zodiacs - Novo Projeto/Assets/_Scripts/Dialogue/DialogueManager.cs
                break;

            case CharacterDisplayExpression.Sad:

<<<<<<< HEAD:Juunishi Zodiacs ver 2/Assets/_Scripts/Dialogue/DialogueManager.cs
                _expressionNumber = (int)CharacterDisplayExpression.Sad;
=======
                _fullbodyExpressionNumber = (int)CharacterDisplayExpression.Sad;
>>>>>>> ToAFicarDoidaComOSourcetree:Juunishi Zodiacs - Novo Projeto/Assets/_Scripts/Dialogue/DialogueManager.cs
                break;

            case CharacterDisplayExpression.Crying:

<<<<<<< HEAD:Juunishi Zodiacs ver 2/Assets/_Scripts/Dialogue/DialogueManager.cs
                _expressionNumber = (int)CharacterDisplayExpression.Crying;
=======
                _fullbodyExpressionNumber = (int)CharacterDisplayExpression.Crying;
>>>>>>> ToAFicarDoidaComOSourcetree:Juunishi Zodiacs - Novo Projeto/Assets/_Scripts/Dialogue/DialogueManager.cs
                break;

            case CharacterDisplayExpression.Hangry:

<<<<<<< HEAD:Juunishi Zodiacs ver 2/Assets/_Scripts/Dialogue/DialogueManager.cs
                _expressionNumber = (int)CharacterDisplayExpression.Hangry;
=======
                _fullbodyExpressionNumber = (int)CharacterDisplayExpression.Hangry;
>>>>>>> ToAFicarDoidaComOSourcetree:Juunishi Zodiacs - Novo Projeto/Assets/_Scripts/Dialogue/DialogueManager.cs
                break;

            case CharacterDisplayExpression.Serious:

<<<<<<< HEAD:Juunishi Zodiacs ver 2/Assets/_Scripts/Dialogue/DialogueManager.cs
                _expressionNumber = (int)CharacterDisplayExpression.Serious;
=======
                _fullbodyExpressionNumber = (int)CharacterDisplayExpression.Serious;
>>>>>>> ToAFicarDoidaComOSourcetree:Juunishi Zodiacs - Novo Projeto/Assets/_Scripts/Dialogue/DialogueManager.cs
                break;
        }

        ExpressionsToDisplay = myDialogTree[_dialogTreeNumber].DialogueStr[_dialogNumber].MyCharacter.CharacterDisplayExpressions[_expressionNumber];
    }

    void FullbodySwitch()
    {
        switch (myDialogTree[_dialogTreeNumber].DialogueStr[_dialogNumber].myFullbody)
        {
            case CharacterFullBodyExpression.Happy:

<<<<<<< HEAD:Juunishi Zodiacs ver 2/Assets/_Scripts/Dialogue/DialogueManager.cs
                _fullbodyExpressionNumber = (int)CharacterFullBodyExpression.Happy;
=======
                _expressionNumber = (int)CharacterFullBodyExpression.Happy;
>>>>>>> ToAFicarDoidaComOSourcetree:Juunishi Zodiacs - Novo Projeto/Assets/_Scripts/Dialogue/DialogueManager.cs
                break;

            case CharacterFullBodyExpression.Surprise:

<<<<<<< HEAD:Juunishi Zodiacs ver 2/Assets/_Scripts/Dialogue/DialogueManager.cs
                _fullbodyExpressionNumber = (int)CharacterFullBodyExpression.Surprise;
=======
                _expressionNumber = (int)CharacterFullBodyExpression.Surprise;
>>>>>>> ToAFicarDoidaComOSourcetree:Juunishi Zodiacs - Novo Projeto/Assets/_Scripts/Dialogue/DialogueManager.cs
                break;

            case CharacterFullBodyExpression.Nervous:

<<<<<<< HEAD:Juunishi Zodiacs ver 2/Assets/_Scripts/Dialogue/DialogueManager.cs
                _fullbodyExpressionNumber = (int)CharacterFullBodyExpression.Nervous;
=======
                _expressionNumber = (int)CharacterFullBodyExpression.Nervous;
>>>>>>> ToAFicarDoidaComOSourcetree:Juunishi Zodiacs - Novo Projeto/Assets/_Scripts/Dialogue/DialogueManager.cs
                break;

            case CharacterFullBodyExpression.Sad:

<<<<<<< HEAD:Juunishi Zodiacs ver 2/Assets/_Scripts/Dialogue/DialogueManager.cs
                _fullbodyExpressionNumber = (int)CharacterFullBodyExpression.Sad;
=======
                _expressionNumber = (int)CharacterFullBodyExpression.Sad;
>>>>>>> ToAFicarDoidaComOSourcetree:Juunishi Zodiacs - Novo Projeto/Assets/_Scripts/Dialogue/DialogueManager.cs
                break;

            case CharacterFullBodyExpression.Crying:

<<<<<<< HEAD:Juunishi Zodiacs ver 2/Assets/_Scripts/Dialogue/DialogueManager.cs
                _fullbodyExpressionNumber = (int)CharacterFullBodyExpression.Crying;
=======
                _expressionNumber = (int)CharacterFullBodyExpression.Crying;
>>>>>>> ToAFicarDoidaComOSourcetree:Juunishi Zodiacs - Novo Projeto/Assets/_Scripts/Dialogue/DialogueManager.cs
                break;

            case CharacterFullBodyExpression.Hangry:

<<<<<<< HEAD:Juunishi Zodiacs ver 2/Assets/_Scripts/Dialogue/DialogueManager.cs
                _fullbodyExpressionNumber = (int)CharacterFullBodyExpression.Hangry;
=======
                _expressionNumber = (int)CharacterFullBodyExpression.Hangry;
>>>>>>> ToAFicarDoidaComOSourcetree:Juunishi Zodiacs - Novo Projeto/Assets/_Scripts/Dialogue/DialogueManager.cs
                break;

            case CharacterFullBodyExpression.Serious:

<<<<<<< HEAD:Juunishi Zodiacs ver 2/Assets/_Scripts/Dialogue/DialogueManager.cs
                _fullbodyExpressionNumber = (int)CharacterFullBodyExpression.Serious;
=======
                _expressionNumber = (int)CharacterFullBodyExpression.Serious;
>>>>>>> ToAFicarDoidaComOSourcetree:Juunishi Zodiacs - Novo Projeto/Assets/_Scripts/Dialogue/DialogueManager.cs
                break;
        }

        FullBodyToDisplay = myDialogTree[_dialogTreeNumber].DialogueStr[_dialogNumber].MyCharacter.FullBodyPoses[_fullbodyExpressionNumber];
    }

    void PisitonSwitch()
    {
        switch (myDialogTree[_dialogTreeNumber].DialogueStr[_dialogNumber].myPositions)
        {
            case FullBodyPositions.Position1:

                _fullBodyPositionsNumber = (int)FullBodyPositions.Position1;
                break;

            case FullBodyPositions.Position2:

                _fullBodyPositionsNumber = (int)FullBodyPositions.Position2;
                break;

            case FullBodyPositions.Position3:

                _fullBodyPositionsNumber = (int)FullBodyPositions.Position3;
                break;

            case FullBodyPositions.Position4:

                _fullBodyPositionsNumber = (int)FullBodyPositions.Position4;
                break;

            case FullBodyPositions.Position5:

                _fullBodyPositionsNumber = (int)FullBodyPositions.Position5;
                break;

          
        }

        FullBodyPosition = _fullBodyPositionsNumber; 
    }

    #endregion

    #region Mandar informação para o UI
    private void UpdateOnUI()
    {

<<<<<<< HEAD:Juunishi Zodiacs ver 2/Assets/_Scripts/Dialogue/DialogueManager.cs
=======
        
>>>>>>> ToAFicarDoidaComOSourcetree:Juunishi Zodiacs - Novo Projeto/Assets/_Scripts/Dialogue/DialogueManager.cs
        DialogueTree = myDialogTree[_dialogTreeNumber];

        //Encapsulamento de informação do Scriptable Dialogue e Character Scriptable Object
        CharacterName = DialogueTree.DialogueStr[_dialogNumber].MyCharacter.CharacterName;
        font = DialogueTree.DialogueStr[_dialogNumber].MyCharacter.Font;
        NameColor = DialogueTree.DialogueStr[_dialogNumber].MyCharacter.MyNameColor;
        DialogColor = DialogueTree.DialogueStr[_dialogNumber].MyCharacter.MyDialogColor;
        SpriteBackground = DialogueTree.DialogueStr[_dialogNumber].MyCharacter.BackGround;
        Background = DialogueTree.Background;
        NameBackground = DialogueTree.BackgroundName;

        DialogToDisplay = DialogueTree.DialogueStr[_dialogNumber].DialogueMessages[_positionInDialog]; //Referencia ao texto dos dialgos

        //encapsulamento das questoes
        _questionToUi1 = (DialogueTree.Question1);
        _questionToUi2 = (DialogueTree.Question2);
        _questionToUi3 = (DialogueTree.Question3);

        //Iformação do Enum das Expressoes.
        ExpressionsSwitch();


        //Atualização de informação no UI: Nome do personagem, Fonte do texto, Cor do nome, cor do Dialogo, Background, Expressoes de texto, background do texto do personagem.
<<<<<<< HEAD:Juunishi Zodiacs ver 2/Assets/_Scripts/Dialogue/DialogueManager.cs
        DialogUIManager.instance.DialogOnScrene(CharacterName, font, NameColor, DialogColor, SpriteBackground, ExpressionsToDisplay, Background, NameBackground);


        //Atualização de informação no UI: Introdução do Texto dos dialogos
        DialogUIManager.instance.PlayCoroutine(DialogToDisplay);
=======
        _diaUiManager.DialogOnScrene(CharacterName, font, NameColor, DialogColor, SpriteBackground, ExpressionsToDisplay, Background, NameBackground);


        //Atualização de informação no UI: Introdução do Texto dos dialogos
        _diaUiManager.PlayCoroutine(DialogToDisplay);
>>>>>>> ToAFicarDoidaComOSourcetree:Juunishi Zodiacs - Novo Projeto/Assets/_Scripts/Dialogue/DialogueManager.cs


        //Iformação dos Enums dos Fullbody.
        FullbodySwitch();
        PisitonSwitch();

        //Atualização de informação no UI: Posição dos FullBody e Sprite dos Fullbody nas posições.
<<<<<<< HEAD:Juunishi Zodiacs ver 2/Assets/_Scripts/Dialogue/DialogueManager.cs
        DialogUIManager.instance.CharactersOnDisplay(FullBodyToDisplay, FullBodyPosition);

        //questoes para ui
        DialogUIManager.instance.QuestionsToUi(_questionToUi1, _questionToUi2, _questionToUi3);
=======
        _diaUiManager.CharactersOnDisplay(FullBodyToDisplay, FullBodyPosition);

        //questoes para ui
        _diaUiManager.QuestionsToUi(_questionToUi1, _questionToUi2, _questionToUi3);

      
>>>>>>> ToAFicarDoidaComOSourcetree:Juunishi Zodiacs - Novo Projeto/Assets/_Scripts/Dialogue/DialogueManager.cs
  
    }

    #endregion

    #region Escolhas e Ramificações

    private void ChoseNextBrench()
    {
        if (DialogueTree.ChangeBrench == false)
        {
            _dialogNumber = 0;

            _dialogTreeNumber++;

          
        }
        else
        {
            _dialogueCanChange = false;
            _dialogNumber = DialogueTree.DialogueStr.Length;
           
                _chosesButtons.SetActive(true);
        }
    }

    public void ChoiseChangeDialogue()
    {
        ChangeDialogue();

        if (_dialogTreeNumber <= myDialogTree.Length)
        {
            _dialogNumber = 0;
            _positionInDialog = -1;
            _dialogueCanChange = true;


        }
        _chosesButtons.SetActive(false);
    }

   
    public void ChoiseChange1()
    {
        if( _dialogueCanChange == false)
        {
            _dialogTreeNumber = DialogueTree.ChoiseDialogueToChange1;
             TrustValue += DialogueTree.TrustValueToIncrese1;
        }
    }
    public void ChoiseChange2()
    {
        if (_dialogueCanChange == false)
        {
            _dialogTreeNumber = DialogueTree.ChoiseDialogueToChange2;
            TrustValue += DialogueTree.TrustValueToIncrese2;
        }
    }
    public void ChoiseChange3()
    {
        if (_dialogueCanChange == false)
        {
            _dialogTreeNumber = DialogueTree.ChoiseDialogueToChange3;
            TrustValue += DialogueTree.TrustValueToIncrese3;
        }
    }

    #endregion

    #region Controlador dos Dialogos
    public void ChangeDialogue()
    {
        if (_dialogueCanChange == true)
        {

<<<<<<< HEAD:Juunishi Zodiacs ver 2/Assets/_Scripts/Dialogue/DialogueManager.cs
            _positionInDialog++;
=======
            if (_diaUiManager.typingeffectCoroutine == null)
            {
                _positionInDialog++;
            }
>>>>>>> ToAFicarDoidaComOSourcetree:Juunishi Zodiacs - Novo Projeto/Assets/_Scripts/Dialogue/DialogueManager.cs

            if (_positionInDialog >= DialogueTree.DialogueStr[_dialogNumber].DialogueMessages.Length)
            {
                _positionInDialog = 0;

                _dialogNumber++;

                if (_dialogNumber >= DialogueTree.DialogueStr.Length)
                {
                    ChoseNextBrench();                   
                }
            }

            UpdateOnUI();

        }
    }
    #endregion
}

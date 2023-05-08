using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Dialogue;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class DialogueManager : MonoBehaviour
{
    #region Variaveis
    //Arrey com os conjuntos de dialogos que sao feitos com Scriptable Objects
    [SerializeField] ScriptableDialogue[] myDialogTree;
    [SerializeField] GameObject _dialogueGameObject;

    //boolan que dita caso haja ramificação no dialogo ou nao
    [SerializeField] bool _dialogueCanChange = true;
    [SerializeField] GameObject _choisesButtons;

    //Dialogue Position 
    [SerializeField] int _dialogNumber;
    [SerializeField] int _positionInDialog;
    [SerializeField] int _dialogTreeNumber;
    
    //Encapsuladores de Informação dos Scriptable Objects
    string DialogToDisplay;
    string CharacterName;
    Font font;
    Font fontOfCharacter;
    Color NameColor;
    Color DialogColor;
    Sprite SpriteBackground;
    Sprite Background;
    Sprite NamePlace;

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

    bool _canChangePlace = true;
    [SerializeField] NextScenarioButton _placesButtons1;
    [SerializeField] NextScenarioButton _placesButtons2;
    [SerializeField] NextScenarioButton _placesButtons3;
    [SerializeField] NextScenarioButton _placesButtons4;

    [SerializeField] GameObject _phoneButton;

    [Header("SceneCombate")]
    [SerializeField] GameObject _prefabCombat;

  

    public int TrustValue { get => _trustValue; set => _trustValue = value; }
    public ScriptableDialogue[] MyDialogTree { get => myDialogTree; set => myDialogTree = value; }
    public ScriptableDialogue DialogueTree1 { get => DialogueTree; set => DialogueTree = value; }
    public int DialogNumber { get => _dialogNumber; set => _dialogNumber = value; }
    public bool CanChangePlace { get => _canChangePlace; set => _canChangePlace = value; }


    #endregion

    #region Awake
    private void Start()
    {
        _positionInDialog = 0;
        DialogNumber = 0;
        _dialogTreeNumber = 0;

        UpdateOnUI();

    }

    #endregion

    #region Enums das Expressoes
    void ExpressionsSwitch()
    {
       
        switch (MyDialogTree[_dialogTreeNumber].DialogueStr[DialogNumber].myExpressions)
        {
            case CharacterDisplayExpression.Happy:

                _expressionNumber = (int)CharacterDisplayExpression.Happy;
                break;

            case CharacterDisplayExpression.Surprise:

                _expressionNumber = (int)CharacterDisplayExpression.Surprise;
                break;

            case CharacterDisplayExpression.Nervous:

                _expressionNumber = (int)CharacterDisplayExpression.Nervous;
                break;

            case CharacterDisplayExpression.Sad:

                _expressionNumber = (int)CharacterDisplayExpression.Sad;
                break;

            case CharacterDisplayExpression.Crying:

                _expressionNumber = (int)CharacterDisplayExpression.Crying;
                break;

            case CharacterDisplayExpression.Hangry:

                _expressionNumber = (int)CharacterDisplayExpression.Hangry;
                break;

            case CharacterDisplayExpression.Serious:

                _expressionNumber = (int)CharacterDisplayExpression.Serious;
                break;

            case CharacterDisplayExpression.VeryHappy:

                _expressionNumber = (int)CharacterDisplayExpression.VeryHappy;
                break;

            case CharacterDisplayExpression.Extra:

                _expressionNumber = (int)CharacterDisplayExpression.Extra;
                break;
        }

        ExpressionsToDisplay = MyDialogTree[_dialogTreeNumber].DialogueStr[DialogNumber].MyCharacter.CharacterDisplayExpressions[_expressionNumber];
    }

    void FullbodySwitch()
    {
        switch (MyDialogTree[_dialogTreeNumber].DialogueStr[DialogNumber].myFullbody)
        {
            case CharacterFullBodyExpression.Happy:

                _fullbodyExpressionNumber = (int)CharacterFullBodyExpression.Happy;
                break;

            case CharacterFullBodyExpression.Surprise:

                _fullbodyExpressionNumber = (int)CharacterFullBodyExpression.Surprise;
                break;

            case CharacterFullBodyExpression.Nervous:

                _fullbodyExpressionNumber = (int)CharacterFullBodyExpression.Nervous;
                break;

            case CharacterFullBodyExpression.Sad:

                _fullbodyExpressionNumber = (int)CharacterFullBodyExpression.Sad;
                break;

            case CharacterFullBodyExpression.Crying:

                _fullbodyExpressionNumber = (int)CharacterFullBodyExpression.Crying;
                break;

            case CharacterFullBodyExpression.Hangry:

                _fullbodyExpressionNumber = (int)CharacterFullBodyExpression.Hangry;
                break;

            case CharacterFullBodyExpression.Serious:

                _fullbodyExpressionNumber = (int)CharacterFullBodyExpression.Serious;
                break;
            case CharacterFullBodyExpression.VeryHappy:

                _fullbodyExpressionNumber = (int)CharacterFullBodyExpression.VeryHappy;
                break;
            case CharacterFullBodyExpression.Extra:

                _fullbodyExpressionNumber = (int)CharacterFullBodyExpression.Extra;
                break;
        }

        FullBodyToDisplay = MyDialogTree[_dialogTreeNumber].DialogueStr[DialogNumber].MyCharacter.FullBodyPoses[_fullbodyExpressionNumber];
    }

    void PositonSwitch()
    {
        switch (MyDialogTree[_dialogTreeNumber].DialogueStr[DialogNumber].myPositions)
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
    public void UpdateOnUI()
    {
        if (MyDialogTree[_dialogTreeNumber] == null)
            return;
        DialogueTree1 = MyDialogTree[_dialogTreeNumber];

        //Encapsulamento de informação do Scriptable Dialogue e Character Scriptable Object
        CharacterName = DialogueTree1.DialogueStr[DialogNumber].MyCharacter.CharacterName;
        font = DialogueTree1.DialogueStr[DialogNumber].MyCharacter.Font;
        fontOfCharacter = DialogueTree1.DialogueStr[DialogNumber].MyCharacter.CharacterFont;
        NameColor = DialogueTree1.DialogueStr[DialogNumber].MyCharacter.MyNameColor;
        DialogColor = DialogueTree1.DialogueStr[DialogNumber].MyCharacter.MyDialogColor;
        SpriteBackground = DialogueTree1.DialogueStr[DialogNumber].MyCharacter.BackGround;
        Background = DialogueTree1.Background;
        DialogToDisplay = DialogueTree1.DialogueStr[DialogNumber].DialogueMessages[_positionInDialog]; //Referencia ao texto dos dialgos
        NamePlace = DialogueTree1.BackgroundName;


        //encapsulamento das questoes
        _questionToUi1 = (DialogueTree1.Question1);
        _questionToUi2 = (DialogueTree1.Question2);
        _questionToUi3 = (DialogueTree1.Question3);

        //Iformação do Enum das Expressoes.
        ExpressionsSwitch();


        //Atualização de informação no UI: Nome do personagem, Fonte do texto, Cor do nome, cor do Dialogo, Background, Expressoes de texto, background do texto do personagem.
        DialogUIManager.instance.DialogOnScrene(CharacterName, font, NameColor, DialogColor, SpriteBackground, ExpressionsToDisplay, Background, fontOfCharacter, NamePlace);


        //Atualização de informação no UI: Introdução do Texto dos dialogos
        
            DialogUIManager.instance.PlayCoroutine(DialogToDisplay);
        
          
        
       


        //Iformação dos Enums dos Fullbody.
        FullbodySwitch();
        PositonSwitch();

        //Atualização de informação no UI: Posição dos FullBody e Sprite dos Fullbody nas posições.
        if(FullBodyToDisplay != null)
        {
            DialogUIManager.instance.CharactersOnDisplay(FullBodyToDisplay, FullBodyPosition);

        }

        //questoes para ui
        DialogUIManager.instance.QuestionsToUi(_questionToUi1, _questionToUi2, _questionToUi3);
    }

    #endregion

    #region Escolhas e Ramificações

    private void ChoseNextBrench()
    {
        
       
            _dialogueCanChange = false;
            DialogNumber = DialogueTree1.DialogueStr.Length;
           
                _choisesButtons.SetActive(true);
        
    }

    public void ChoiseChangeDialogue()
    {
        ChangeDialogue();

        if (_dialogTreeNumber <= MyDialogTree.Length)
        {
            DialogNumber = 0;
            _positionInDialog = -1;
            _dialogueCanChange = true;


        }
        _choisesButtons.SetActive(false);
    }

   
    public void ChoiseChange1()
    {
        if( _dialogueCanChange == false)
        {
            _dialogTreeNumber = DialogueTree1.ChoiseDialogueToChange1;
             TrustValue += DialogueTree1.TrustValueToIncrese1;
        }
    }
    public void ChoiseChange2()
    {
        if (_dialogueCanChange == false)
        {
            _dialogTreeNumber = DialogueTree1.ChoiseDialogueToChange2;
            TrustValue += DialogueTree1.TrustValueToIncrese2;
        }
    }
    public void ChoiseChange3()
    {
        if (_dialogueCanChange == false)
        {
            _dialogTreeNumber = DialogueTree1.ChoiseDialogueToChange3;
            TrustValue += DialogueTree1.TrustValueToIncrese3;
        }
    }

    #endregion

    #region Controlador dos Dialogos
    public void ChangeDialogue()
    {
        if (_dialogueCanChange == true)
        {
            if(DialogUIManager.instance.typingeffectCoroutine == null)
            {
                _positionInDialog++;
            }
           

            if (_positionInDialog >= DialogueTree1.DialogueStr[DialogNumber].DialogueMessages.Length)
            {
                _positionInDialog = 0;

                DialogNumber++;

                if (DialogNumber >= DialogueTree1.DialogueStr.Length && DialogueTree1.IsEndDialogue == false && DialogueTree1.SceneTransition == false)
                {
                    DialogUIManager.instance.CanOpenPhone = false;
                    if (DialogueTree1.ChangeBrench == false)
                    {
                        DialogNumber = 0;

                        if (_dialogTreeNumber < MyDialogTree.Length) //avança a posição nos dialogos principais
                        {
                            _dialogTreeNumber++;
                        }                      

                    }
                    else
                    {
                        ChoseNextBrench();

                    }
                }
                else if(DialogNumber >= DialogueTree1.DialogueStr.Length && DialogueTree1.IsEndDialogue == true)
                {
                 
                    //Debug.Log("AAA");
                    DisableDialgue();
                    CanChangePlace = true;
                    _placesButtons1.ChangeButtonColorVisible();
                    _placesButtons2.ChangeButtonColorVisible();
                    _placesButtons3.ChangeButtonColorVisible();
                    _placesButtons4.ChangeButtonColorVisible();
                   
                }
                else if(DialogNumber >= DialogueTree1.DialogueStr.Length && DialogueTree1.SceneTransition == true)
                {
                    if(DialogueTree1.LoadingScene)
                    {
                        if(_prefabCombat != null)
                        {
                            //Debug.Log("sda");
                            LoadingSceneManager.sceneInstance.LoadScene(DialogueTree1.NextScene, _prefabCombat);
                        }
                        else
                        {
                            LoadingSceneManager.sceneInstance.LoadScene(DialogueTree1.NextScene);
                        }

                    }
                    else
                    {
                        SceneManager.LoadScene(DialogueTree1.NextScene);
                    }
                }
            }
            if(DialogNumber < DialogueTree1.DialogueStr.Length)
           {
                UpdateOnUI();

            }
        }     
    }
    #endregion

    public void DisableDialgue()
    {
        _phoneButton.SetActive(true);
        DialogUIManager.instance.CanOpenPhone = true;
        _positionInDialog = 0;
        DialogNumber = 0;
        _dialogTreeNumber = 0;
        _dialogueGameObject.SetActive(false);
    }

    public void ButtonDisable()
    {
        _dialogueCanChange = true;
        CanChangePlace = true;
        _choisesButtons.SetActive(false);
        _placesButtons1.ChangeButtonColorVisible();
        DisableDialgue();
      
    }
}

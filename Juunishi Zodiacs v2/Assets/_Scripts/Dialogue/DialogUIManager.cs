using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogUIManager : MonoBehaviour
{
    #region Variaveis

    public static DialogUIManager instance;

    [Header("Dialogue")]
    [SerializeField] private Text _charDialog;
    [SerializeField] private Text _charName;
    [SerializeField] private Image _diaBackgroundUI;
    [SerializeField] private Image _characterUI;
    [SerializeField] private Image _backgroundUI;
    [SerializeField] private Image _backgroundNameUI;

    [SerializeField] private Image[] _charactersPositions;

    [SerializeField] DialogueManager _diaManager;

    float _textVelocity = 0.05f;

    public IEnumerator typingeffectCoroutine;

    [Header("Questions")]
    [SerializeField] Text _questionText1;
    [SerializeField] Text _questionText2;
    [SerializeField] Text _questionText3;

    string _currentMensage;

    [Header("Places")]
    [SerializeField] Image _placeBackgroundUI;
    [SerializeField] Image _namePlace;


    [Header("Phone")]
    [SerializeField] Image _phoneUI;
    bool _phoneIsOpen = false;
    bool _canOpenPhone = true;
    [SerializeField] GameObject _fakeBackground;

    bool _itemBackgroundRemove = false;

    #endregion

    #region Propriedades
    public Image[] CharactersPositions { get => _charactersPositions; set => _charactersPositions = value; }
    public bool CanOpenPhone { get => _canOpenPhone; set => _canOpenPhone = value; }
    public Text CharDialog { get => _charDialog; set => _charDialog = value; }

    #endregion

    #region Awake com Sigletone
    private void Awake()
    {
        
        //singletone
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            
        } 
    }
    #endregion

    #region Informação para UI
    //Passa a informação do dialogo para o UI
    public void DialogOnScrene(string characterName, Font font, Color NameColor, Color DialogColor, Sprite diaBackground, Sprite character, Sprite background, Font characterFont, Sprite namePlace)
    {
     
            _diaBackgroundUI.sprite = diaBackground;

            _charName.text = characterName; //Passa o nome do Personagem para o UI

            _characterUI.sprite = character;
            CharDialog.font = font;
            _charName.font = characterFont;
            _backgroundUI.sprite = background;
            _backgroundNameUI.sprite = namePlace;


            CharDialog.color = DialogColor;
            _charName.color = NameColor;
        
    
    }

    public void CharactersOnDisplay(Sprite characterSprite, int characterPositionOnDisplay)
    {
        
        CharactersPositions[characterPositionOnDisplay].gameObject.SetActive(true);
        CharactersPositions[characterPositionOnDisplay].sprite = characterSprite;
    }

    public void PlaceOnScrene(Sprite background, Sprite nameSpace)
    {
        _placeBackgroundUI.sprite = background;
        _namePlace.sprite = nameSpace;
    }
    #endregion

    #region Exposição do Dialogo
    public void PlayCoroutine(string dialogToDisplay)
    {

        if (typingeffectCoroutine != null)
        {
            EndCoroutine(dialogToDisplay);
        }
        else
        {
            typingeffectCoroutine = Typing(dialogToDisplay);
            StartCoroutine(typingeffectCoroutine);
        }
     

    }
    IEnumerator Typing(string dialog)
    {
        CharDialog.text = "";
        _currentMensage = dialog;

        foreach (char letra in dialog.ToCharArray())
        {
            CharDialog.text += letra;
            yield return new WaitForSeconds(_textVelocity);
        }
        typingeffectCoroutine = null;

    }


    public void EndCoroutine(string dialogue)
    {
        StopCoroutine(typingeffectCoroutine);

        CharDialog.text = _currentMensage;

        typingeffectCoroutine = null;
        //dar display do texto completo
    }

    public void NextDialogueButton()
    {
        _diaManager.ChangeDialogue();
    }

    #endregion

    #region Encolhas das ramificações
    public void NextDialgueOnChoise1()
    {
        _diaManager.ChoiseChange1();
        _diaManager.ChoiseChangeDialogue();
        _diaManager.ChangeDialogue();
        // _diaManager.TrustValue += _diaManager.DialogueTree1.TrustValueToIncrese1;

    }
    public void NextDialgueOnChoise2()
    {
        _diaManager.ChoiseChange2();
        _diaManager.ChoiseChangeDialogue();
        _diaManager.ChangeDialogue();
        // _diaManager.TrustValue += _diaManager.DialogueTree1.TrustValueToIncrese2;
    }
    public void NextDialgueOnChoise3()
    {
        _diaManager.ChoiseChange3();
        _diaManager.ChoiseChangeDialogue();
        _diaManager.ChangeDialogue();
        // _diaManager.TrustValue += _diaManager.DialogueTree1.TrustValueToIncrese3;
    }

    public void QuestionsToUi(string question1, string question2, string question3)
    {
        _questionText1.text = question1;
        _questionText2.text = question2;
        _questionText3.text = question3;
    }

    #endregion

  public void LoadScene(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }

    public void PhoneButton()
    {
        if (CanOpenPhone == true)
        {
            if (_phoneIsOpen == true)
            {
                _phoneUI.gameObject.SetActive(false);
                _phoneIsOpen = false;
                _fakeBackground.gameObject.SetActive(false);
               
            }
            else
            {
                _phoneUI.gameObject.SetActive(true);
                _phoneIsOpen = true;
                _fakeBackground.gameObject.SetActive(true);

               
            }

        }

    }

}

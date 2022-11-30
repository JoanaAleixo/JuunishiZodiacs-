using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Dialog")]
    [SerializeField] private Text _charDialog;
    [SerializeField] private Text _charName;
    [SerializeField] private Image _diaBackgroundUI;
    [SerializeField] private Image _characterUI;
    [SerializeField] private Image _backgroundUI;

    [SerializeField] private Image[] _charactersPositions;

    [SerializeField] DialogueManager _diaManager;

  

    float _textVelocity = 0.05f;

    Coroutine typingeffectCoroutine;

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

     //Passa a informação do dialogo para o UI
    public void DialogOnScrene(string characterName, Font font, Color NameColor, Color DialogColor, Sprite diaBackground, Sprite character, Sprite background)
    {
        
 
        _charName.text = characterName; //Passa o nome do Personagem para o UI
        _diaBackgroundUI.sprite = diaBackground;
        _characterUI.sprite = character;
        _charDialog.font = font; 
        _charName.font = font;
        _backgroundUI.sprite = background;

        _charDialog.color = DialogColor;
        _charName.color = NameColor;

     


    }

    public void CharactersOnDisplay(Sprite characterSprite, int characterPositionOnDisplay)
    {
        _charactersPositions[characterPositionOnDisplay].gameObject.SetActive(true);
        _charactersPositions[characterPositionOnDisplay].sprite = characterSprite;
    }

    public void PlayCoroutine(string dialogToDisplay)
    {
        if (typingeffectCoroutine != null)
        {
            StopCoroutine(Typing(dialogToDisplay));
        }

        typingeffectCoroutine = StartCoroutine(Typing(dialogToDisplay));
    }
    IEnumerator Typing(string dialog)
    {
        _charDialog.text = "";
     

        foreach (char letra in dialog.ToCharArray())
        {
            _charDialog.text += letra;
            yield return new WaitForSeconds(_textVelocity);
        }
    }

    public void EndCoroutine()
    {
        StopCoroutine(typingeffectCoroutine);
   
    }

    public void NextDialogueButton()
    {
        _diaManager.ChangeDialog();
    }
}

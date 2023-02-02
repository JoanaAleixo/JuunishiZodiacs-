using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NextScenarioButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    #region Variaveis
    [SerializeField] int _nextPlaceInfo;
    [SerializeField] NavigationManager _navManager;
    [SerializeField] DialogueManager _diaManager;

    [SerializeField] GameObject _text;
     Color _textColor;
    string _nextPlaceName;
    Vector3 _newButtonPosition;

  
    #endregion

    #region Informação do Butão

    //Receber informação sobre o butão para implementar e adicionar a função ao butão
    public void SetButtonInfo(int nextPlace, string placeTextName, Color textColor, Vector3 buttonPosition, int positionInButton, ScriptableDialogue dialogue)
    {
        
        _nextPlaceInfo = nextPlace;
        _nextPlaceName= placeTextName;
        _textColor= textColor;
       _newButtonPosition= buttonPosition;

       Button thisButton = GetComponent<Button>();
        thisButton.onClick.RemoveAllListeners();
        thisButton.onClick.AddListener(() => NextPlaceButton(_nextPlaceInfo, positionInButton, dialogue));

        thisButton.transform.position = Camera.main.WorldToScreenPoint(_newButtonPosition);

       
    }

    #endregion

    #region Abrir novo Place
    public void NextPlaceButton(int changePlace, int positionInButton, ScriptableDialogue dialogue)
    {

      if(_diaManager.CanChangePlace == true)
        {
           

            _diaManager.DisableDialgue();

            if (_navManager.PlacesList.DislocationStr[positionInButton].HasDialogue == true)
            {
                ChangeButtonColorInvisible();
                _diaManager.MyDialogTree[0] = dialogue;
                _diaManager.UpdateOnUI();
                _navManager.DialogueCanvas.SetActive(true);
                _diaManager.CanChangePlace = false;
               

            }

            //abrir o novo Place
            _navManager.NewPlace(changePlace);
            
        }


    }
    public void ChangeButtonColorVisible()
    {
        Color newColor = new Color(255, 255, 255, 1);
        Image _buttonImage = GetComponent<Image>();

        _buttonImage.color = newColor;
    }

    public void ChangeButtonColorInvisible()
    {
        Color newColor = new Color(0, 0, 0, 0);
        Image _buttonImage = GetComponent<Image>();

        _buttonImage.color = newColor;
    }

    #endregion

    #region Enter and Exit Butão
    //rato por cima do Butão
    public void OnPointerEnter(PointerEventData eventData)
    {
        _text.SetActive(true);
       Text textRef = _text.GetComponent<Text>();
        textRef.text = _nextPlaceName;
        textRef.color = _textColor;
    }

    //rato sai de cima do butão
    public void OnPointerExit(PointerEventData eventData)
    {
        _text.SetActive(false);
    
    }
    #endregion
}

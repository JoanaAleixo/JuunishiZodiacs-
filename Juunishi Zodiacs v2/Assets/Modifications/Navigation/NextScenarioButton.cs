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

    [SerializeField] GameObject _text;
     Color _textColor;
    string _nextPlaceName;
    Vector3 _newButtonPosition;

    #endregion

    #region Informa��o do But�o

    //Receber informa��o sobre o but�o para implementar e adicionar a fun��o ao but�o
    public void SetButtonInfo(int nextPlace, string placeTextName, Color textColor, Vector3 buttonPosition)
    {
       _nextPlaceInfo= nextPlace;
        _nextPlaceName= placeTextName;
        _textColor= textColor;
        _newButtonPosition= buttonPosition;

       Button thisButton = GetComponent<Button>();
        thisButton.onClick.RemoveAllListeners();
        thisButton.onClick.AddListener(() => NextPlaceButton(_nextPlaceInfo));

        thisButton.transform.position = Camera.main.WorldToScreenPoint(_newButtonPosition);
    }

    #endregion

    #region Abrir novo Place
    public void NextPlaceButton(int changePlace)
    {   
        //abrir o novo Place
        _navManager.NewPlace(changePlace);
    }

    #endregion

    #region Enter and Exit But�o
    //rato por cima do But�o
    public void OnPointerEnter(PointerEventData eventData)
    {
        _text.SetActive(true);
       Text textRef = _text.GetComponent<Text>();
        textRef.text = _nextPlaceName;
        textRef.color = _textColor;
    }

    //rato sai de cima do but�o
    public void OnPointerExit(PointerEventData eventData)
    {
        _text.SetActive(false);
    
    }
    #endregion
}

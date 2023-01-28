using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DislocationButtons;
using UnityEngine.UI;

public class NavigationManager : MonoBehaviour
{

    #region Variaveis

    [Header("References")]
    [SerializeField] ScriptablePlace[] _myPlaces;
    [SerializeField] DialogUIManager _uiManager;
    [SerializeField] GameObject _placesCanvas;

    int _currentPlaceIndex;
    ScriptablePlace _placesList;

    [Header("ElementsToUI")]
     Sprite _background;
     Sprite _namePlace;

    [Header("Buttons")]
    [SerializeField] GameObject[] _buttons;

    #endregion

    #region Propriedades
    public ScriptablePlace PlacesList { get => _placesList; set => _placesList = value; }

    #endregion

    #region Start
    private void Start()
    {
        NewPlace(0);
    }
    #endregion

    #region Informa��o para o UI
    //Implementa��o dos elementos do UI
    void UpdateUI()
    {
        PlacesList = _myPlaces[_currentPlaceIndex];
        _background = PlacesList.Background;
        _namePlace = PlacesList.NamePlace;

        _uiManager.PlaceOnScrene(_background, _namePlace); //Implementa��o dos elementos simples

        //Para cada But�o na lista de but�es do ScriptablePlace
        for (int i = 0; i < PlacesList.DislocationStr.Length; i++)
        {
            _buttons[i].SetActive(true);

            DislocationButtons buttonProperties = PlacesList.DislocationStr[i];
            _buttons[i].GetComponent<NextScenarioButton>().SetButtonInfo(buttonProperties.NextPlaceValue, buttonProperties.ButtonDirectionName, buttonProperties.TextColor, buttonProperties.ButtonPosition);   //Info para o but�o     
        }        
    }

    #endregion

    #region Usar But�o
    //Metudo que troca de Place quando o but�o � percionado
    public void NewPlace(int placeIndex)
    {
        //novo Place
        _currentPlaceIndex = placeIndex;

        //desligar os but�es
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].SetActive(false);
        }
        //Readicionar o novo Place no UI
        UpdateUI();
    }
    #endregion
}

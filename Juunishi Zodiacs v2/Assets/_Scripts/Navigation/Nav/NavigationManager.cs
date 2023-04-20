using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DislocationButtons;
using UnityEngine.UI;
using System.Linq;

public class NavigationManager : MonoBehaviour
{

    #region Variaveis

    [Header("References")]
    [SerializeField] ScriptablePlace[] _myPlaces;
    [SerializeField] DialogUIManager _uiManager;
    [SerializeField] GameObject _placesCanvas;
    [SerializeField] GameObject _DialogueCanvas;

    [SerializeField] int _currentPlaceIndex;
    ScriptablePlace _placesList;

    [Header("ElementsToUI")]
     Sprite _background;
     Sprite _namePlace;

    [Header("Buttons")]
    [SerializeField] GameObject[] _buttons;
    [SerializeField] GameObject _phoneButton;

    [Header("Itens")]
    [SerializeField] GameObject _itemPrefab;
    [SerializeField] List<GameObject> _itensList = new List<GameObject>();

    #endregion

    #region Propriedades
    public ScriptablePlace PlacesList { get => _placesList; set => _placesList = value; }
    public GameObject DialogueCanvas { get => _DialogueCanvas; set => _DialogueCanvas = value; }
  

    #endregion

    #region Start
    private void Start()
    {
        NewPlace(0);
    }
    #endregion

    #region Informação para o UI
    //Implementação dos elementos do UI
   public void UpdateUI()
    {
        PlacesList = _myPlaces[_currentPlaceIndex];
        _background = PlacesList.Background;
        _namePlace = PlacesList.NamePlace;

        _uiManager.PlaceOnScrene(_background, _namePlace); //Implementação dos elementos simples

        //Para cada Butão na lista de butões do ScriptablePlace
        for (int i = 0; i < PlacesList.DislocationStr.Length; i++)
        {
            _buttons[i].SetActive(true);

            if (PlacesList.ThisPlaceHasDialogue == true)
            {
                _buttons[i].GetComponent<NextScenarioButton>().ChangeButtonColorInvisible();
                _phoneButton.SetActive(false);
                //botão desaparecer 
            }
              
            

            DislocationButtons buttonProperties = PlacesList.DislocationStr[i];
            _buttons[i].GetComponent<NextScenarioButton>().SetButtonInfo(buttonProperties.NextPlaceValue, buttonProperties.ButtonDirectionName, buttonProperties.TextColor, buttonProperties.ButtonPosition, i, buttonProperties.DialogueToPlace);   //Info para o butão     
          
        }
    }

    #endregion

    #region Usar Butão
    //Metudo que troca de Place quando o butão é percionado
    public void NewPlace(int placeIndex)
    {
        for (int i = 0; i < _buttons.Length; i++)
       {
           NextScenarioButton button = _buttons[i].GetComponent<NextScenarioButton>();
           button.Text.SetActive(false);
           button.TextBox.SetActive(false);
        }
       
        //novo Place
        _currentPlaceIndex = placeIndex;

        //desligar os butões
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].SetActive(false);
        }
        //Readicionar o novo Place no UI
     
            UpdateUI();
        
       
    }
    #endregion


    public void SpawnItensOnPlace()
    {
       // foreach (var posInIntens in _myPlaces[_currentPlaceIndex].Itens)
      //  {
        //    Image newItemImage = _itemPrefab.GetComponent<Image>();
        //    _myPlaces[_currentPlaceIndex].Itens[posInIntens].Icon = newItemImage.sprite;

       //    Button newItem = _itemPrefab.GetComponent<Button>();
      //  }

        for (int i = 0; i < _myPlaces[_currentPlaceIndex].Itens.Length; i++)
        {
           GameObject Item = Instantiate(_itemPrefab, _myPlaces[_currentPlaceIndex].Itens[i].ItemPositionInNav, _itemPrefab.transform.rotation);
            Item.transform.SetParent(_placesCanvas.transform, true);

            Image newItemImage = _itemPrefab.GetComponent<Image>();
            _myPlaces[_currentPlaceIndex].Itens[i].Icon = newItemImage.sprite;
        }
    }
   
}

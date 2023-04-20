using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] List<GameObject> _itensVerification = new List<GameObject>();
    Dictionary<int,  List<ScriptableItem>> dici = new Dictionary<int, List<ScriptableItem>> ();

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

        SpawnItems();

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


    public void SpawnItems()
    {
        // foreach (var posInIntens in _myPlaces[_currentPlaceIndex].Itens)
        //  {
        //    Image newItemImage = _itemPrefab.GetComponent<Image>();
        //    _myPlaces[_currentPlaceIndex].Itens[posInIntens].Icon = newItemImage.sprite;

        //    Button newItem = _itemPrefab.GetComponent<Button>();
        //  }
        Debug.Log("ee");
   
            for (int i = 0; i <PlacesList.Itens.Length; i++)
            {


                GameObject Item = Instantiate(_itemPrefab, _itemPrefab.transform.position, _itemPrefab.transform.rotation) as GameObject;
                Item.transform.SetParent(_placesCanvas.transform, false);
                Item.transform.position = Camera.main.WorldToScreenPoint(PlacesList.Itens[i].ItemPositionInNav);
           
                _itensList.Add(Item);

            

            //  dici.Add(_currentPlaceIndex, PlacesList.Itens[i]);
           // Debug.Log(dici[_currentPlaceIndex]);

            //Item.SetActive(false);

            Image newItemImage = Item.GetComponent<Image>();
                newItemImage.sprite = PlacesList.Itens[i].Icon;

                

            }
        
    }
   
}

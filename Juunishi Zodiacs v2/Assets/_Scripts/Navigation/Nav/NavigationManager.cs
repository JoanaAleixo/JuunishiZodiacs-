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
    [SerializeField] GameObject _itemDisplay;
    [SerializeField] List<GameObject> _itensList = new List<GameObject>();
    Dictionary<int, int> _itemDic = new Dictionary<int, int> ();

    public static NavigationManager instance;
    #endregion

    #region Propriedades
    public ScriptablePlace PlacesList { get => _placesList; set => _placesList = value; }
    public GameObject DialogueCanvas { get => _DialogueCanvas; set => _DialogueCanvas = value; }
    public Dictionary<int, int> ItemDic { get => _itemDic; set => _itemDic = value; }


    #endregion

    #region Start
    private void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

        }
        SpawnItems();
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

        foreach (var itensToDisable in _itensList)
        {
            itensToDisable.SetActive(false);
        }
        foreach (var item in ItemDic)
        {
           if(item.Value == _currentPlaceIndex)
            {
                foreach (var itensList in _itensList)
                {
                    if(itensList.GetComponent<ItemCollect>().ItemId == item.Key)
                    {
                        itensList.SetActive(true);
                    }
                }
            }
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
        int idCounter = 0;

        for (int e = 0; e < _myPlaces.Length; e++)
        {

            for (int i = 0; i < _myPlaces[e].Itens.Length; i++)
            {

              
                GameObject Item = Instantiate(_itemPrefab, _itemPrefab.transform.position, _itemPrefab.transform.rotation) as GameObject;
                Item.transform.SetParent(_itemDisplay.transform, false);
                Item.transform.position = Camera.main.WorldToScreenPoint(_myPlaces[e].Itens[i].ItemPositionInNav);


                 _itensList.Add(Item);

                idCounter++;

                Item.GetComponent<ItemCollect>().ItemId = idCounter;
                ItemDic.Add(idCounter, e);

                Item.SetActive(false);



                Image newItemImage = Item.GetComponent<Image>();
                newItemImage.sprite = _myPlaces[e].Itens[i].Icon;

                Item.GetComponent<ItemCollect>().ThisItem = _myPlaces[e].Itens[i];

            }
        }

      //  foreach (var item in _itemDic)
     //   {
       //     Debug.Log(item.Key + " " + item.Value);
      //  }
    }
   
}

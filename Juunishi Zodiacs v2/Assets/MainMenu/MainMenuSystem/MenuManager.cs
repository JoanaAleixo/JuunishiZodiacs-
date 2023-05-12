using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;
using static Dialogue;

public class MenuManager : MonoBehaviour
{
    [Header("MainMenu")]
    [SerializeField] GameObject[] _mainMenuElements;

    [Header("Inventory")]
   
    [SerializeField] GameObject _inventoryStorage;
    [SerializeField] GameObject _itemToStorage;
    [SerializeField] Inventory _inventoryInfo;

    [SerializeField] List<GameObject> _itensInStorage = new List<GameObject>();

    [SerializeField] GameObject _itemDescriptionBox;

    [Header("Gallery")]


    [SerializeField] GameObject _galleryStorage;
    [SerializeField] GameObject _photoPrefab;
    [SerializeField] List<GameObject> _itensInGallery = new List<GameObject>();

    [Header("AbilitySwitching")]
    [SerializeField] GameObject[] _abilityButtons;
    [SerializeField] PlayableCaracterScptObj _akira;
    [SerializeField] Sprite[] _abilitySprites;
    [SerializeField] GameObject _abilityInfo;
    [SerializeField] TextMeshProUGUI _abilityDesc;
    [SerializeField] TextMeshProUGUI _abilityName;

    public static MenuManager instance;

    public List<GameObject> ItensInStorage { get => _itensInStorage; set => _itensInStorage = value; }
    public GameObject ItemDescriptionBox { get => _itemDescriptionBox; set => _itemDescriptionBox = value; }
    public Inventory InventoryInfo { get => _inventoryInfo; set => _inventoryInfo = value; }
    public Sprite[] AbilitySprites { get => _abilitySprites; set => _abilitySprites = value; }

    public List<GameObject> ItensInGallery { get => _itensInGallery; set => _itensInGallery = value; }

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))        
        {
            Gallery();
        }
    }
    public void Option(int switchNumb)
    {
       
        foreach (var item in _mainMenuElements)
        {
            item.SetActive(false);
        }
        _mainMenuElements[switchNumb].SetActive(true);
    }

  public void InventorySystem()
    {
        foreach (var item in InventoryInfo.InventoryDic.Keys)
        {
            GameObject Item = Instantiate(_itemToStorage, _inventoryStorage.transform.position, _itemToStorage.transform.rotation) as GameObject;
            Item.transform.SetParent(_inventoryStorage.transform, false);
            Item.transform.position = Camera.main.WorldToScreenPoint(_itemToStorage.transform.position);

            ItensInStorage.Add(Item);

           Image itemIcon = Item.transform.GetChild(0).GetChild(0).GetComponent<Image>();

            if (item is KeyItem)
            {
                KeyItem keyItem = (KeyItem)item;
                itemIcon.sprite = keyItem.IconForBag;
            }
            else if (item is UsableItem)
            {
                UsableItem usableItem = (UsableItem)item;
                itemIcon.sprite = usableItem.IconForBag;
            }

            TextMeshProUGUI itemToName = Item.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();        
            itemToName.text = item.ItemName;

            TextMeshProUGUI itemAmount = Item.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>();
            InventoryInfo.InventoryDic.TryGetValue(item, out int itemValue);
            itemAmount.text = itemValue.ToString();

            OpenItemDescription thisItem = Item.transform.GetChild(1).GetComponent<OpenItemDescription>();
            thisItem.ThisItemOnButton = item;

        }
    }

    public void Gallery()
    {
        foreach (var item in InventoryInfo.InventoryDic.Keys)
        {
            GameObject Item = Instantiate(_photoPrefab, _galleryStorage.transform.position, _photoPrefab.transform.rotation) as GameObject;
            Item.transform.SetParent(_galleryStorage.transform, false);
            Item.transform.position = Camera.main.WorldToScreenPoint(_photoPrefab.transform.position);

            ItensInGallery.Add(Item);

            Image photoImage = Item.GetComponent<Image>();

            if (item is PhotoItem)
            {
                PhotoItem photoItem = (PhotoItem)item;
                photoImage.sprite = photoItem.Icon;
            }
            else 
            {
                return;
            }
        }
    }

    public void CloseInventory()
    {
        foreach (var item in _itensInStorage)
        {
            Destroy(item);
            
        }
        _itensInStorage.Clear();
    }
    public void CloseGallery()
    {
        foreach (var item in _itensInGallery)
        {
            Destroy(item);

        }
        _itensInGallery.Clear();
    }

    #region AbilitiesStuff

    public void OpenCloseAbilities()
    {
        if (_abilityButtons[0].activeSelf == true)
        {
            foreach(var item in _abilityButtons)
            {
                item.SetActive(false); 
            }
        }
        else
        {
            int cont = 0;
            foreach (var item in _abilityButtons)
            {
                item.SetActive(true);
                item.GetComponent<AbilityInformation>().UpdateInfo(_akira, cont);
                cont++;
            }
        }
    }

    public void ShowAbilityInfo(int _abilityNumb)
    {
        _abilityInfo.SetActive(true);
        _abilityName.text = _akira.Abilities[_abilityNumb].AbilityName;
        _abilityDesc.text = _akira.Abilities[_abilityNumb].Description;
    }

    public void CloseAbilityInfo()
    {
        _abilityInfo.SetActive(false);
    }

    #endregion
}


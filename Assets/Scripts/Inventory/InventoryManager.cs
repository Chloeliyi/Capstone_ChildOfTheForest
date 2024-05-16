using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    //public GameObject SmallInventoryMenu;

    [SerializeField] private bool menuActivated;

    public List<Item> Items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    public Transform SmallItemContent;

    public Toggle EnableRemove;

    public InventoryItemController[] InventoryItems;

    public TMP_Text DescName;

    public Image DescIcon;

    public TMP_Text DescText;

    public void Start()
    {
        DescName.text = "Item";
        DescIcon.sprite = null;
        DescText.text = "Item Description";
    }
    public void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.G) && menuActivated)
        {
            Time.timeScale = 1;
            SmallInventoryMenu.gameObject.SetActive(false);
            menuActivated = false;
            EnableRemove.gameObject.SetActive(false);
        }
        else if(Input.GetKeyDown(KeyCode.G) && !menuActivated)
        {
            Time.timeScale = 0;
            SmallInventoryMenu.gameObject.SetActive(true);
            menuActivated = true;
            EnableRemove.gameObject.SetActive(true);
        }*/
    }

    public void Add(Item item)
    {
        Items.Add(item);
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
    }

    private GameObject obj;

    private GameObject desc;

    public void ListItems()
    {
        //Clear content before open
        foreach(Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in Items)
        {
            obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;

            if (EnableRemove.isOn)
            {
                removeButton.gameObject.SetActive(true);
            }
        }

        SetInventoryItems();
    }

    /*public void ListSmallInvenItems()
    {
        foreach(Transform item in SmallItemContent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, SmallItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;

            if (EnableRemove.isOn)
            {
                removeButton.gameObject.SetActive(true);
            }
        }

        SetSmallInventoryItems();
    }*/

    public void EnableItemsRemove()
    {
        if (EnableRemove.isOn)
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(true);
            }

            foreach (Transform item in SmallItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(true);
            }
        }
        else
        {
            foreach(Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }

            foreach(Transform item in SmallItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }
        }       
    }

    public void SetInventoryItems()
    {
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < Items.Count; i++)
        {
            InventoryItems[i].AddItem(Items[i]);
            Debug.Log(Items[i]);
        }
    }

    /*public void SetSmallInventoryItems()
    {
        InventoryItems = SmallItemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < Items.Count; i++)
        {
            InventoryItems[i].AddItem(Items[i]);
        }
    }*/

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Linq;

public class InventoryManager : MonoBehaviour
{

    public InventoryItemController[] itemSlot;

    public Item[] itemSOs;

    public bool UseItem(string itemName)
    {
        for (int i = 0; i < itemSOs.Length; i++) 
        {
            if (itemSOs[i].itemName == itemName)
            {
                bool usable = itemSOs[i].UseItem();
                return usable;
            }
        }
        return false;
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        //Debug.Log("Item name : " + itemName + " quantity : " + quantity + " Item sprite : " + itemSprite);
        for (int i = 0; i < itemSlot.Length; i++) 
        {
            if (itemSlot[i].isFull == false && itemSlot[i].itemName == itemName || itemSlot[i].quantity == 0)
            {
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                if (leftOverItems > 0)
                {
                    leftOverItems = AddItem(itemName, quantity, itemSprite, itemDescription);
                }
                return leftOverItems;
            }
        }
        return quantity;
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)  
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }
    /*public static InventoryManager Instance;

    [SerializeField] private bool menuActivated;

    public List<Item> Items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    public Toggle EnableRemove;

    public InventoryItemController[] InventoryItems;
    public List<int> ItemsQuantity = new List<int>();

    public TMP_Text DescName;

    public Image DescIcon;

    public TMP_Text DescText;

    public int quantity;

    [SerializeField] private int itemQuantity = 1;

    [SerializeField] private int maxitemQuantity = 3;

    public int totalAmountOfItems;

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

    public int counter;

    public void Add(Item item)
    {
        //Items.Add(item);
        //Optionone
        if (item.IsStackable())
        {
            bool ItemAlreadyInInventory = false;
            foreach (Item inventoryitem in Items)
            {
                if (inventoryitem.itemType == item.itemType)
                {
                    inventoryitem.quantity += itemQuantity;
                    ItemAlreadyInInventory = true;

                }
            }
            if (!ItemAlreadyInInventory)
            {
                Items.Add(item);
            }
        }
        else
        {
            Items.Add(item);
        }
        //Optiontwo
        if (Items.Count == 0)
        {
            Items.Add(item);
            ItemsQuantity.Add(itemQuantity);
        }
        
        else
        {
            if (Items[counter] == item)
            {
                if (ItemsQuantity[counter] < maxitemQuantity)
                {
                    Debug.Log("Below max items");
                    ItemsQuantity[counter] ++;
                }  
                else
                {
                    Debug.Log("New Slot : " + item);
                    Items.Add(item);
                    ItemsQuantity.Add(itemQuantity);
                    counter ++;
                }
            }
            else if (Items[counter] != item)
            {
                Debug.Log("Look through list");
                for (int a = 0; a < Items.Count; a++) 
                {
                    if (Items[a] != item)
                    {
                        //Adding each time a item not equals is found
                        Debug.Log("Create new slot");
                        Debug.Log("Index A is not: " + a);
                        Debug.Log("Item : " + item);
                        Items.Add(item);
                        ItemsQuantity.Add(0);
                        //counter = a;
                        //break;
                    }
                    else if (Items[a] == item)
                    {
                        Debug.Log("Found other slot");
                        Debug.Log("Index A :" + a);
                        counter = a;
                        ItemsQuantity[counter]++;
                    }
                }
            }
        }
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
    }

    private GameObject obj;

    private GameObject desc;

    public void ClearContent()
    {
        //Clear content before open
        foreach(Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
    }
    public int test;

    public void ListItems()
    {
        foreach (var item in Items)
        {
            obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var itemCounter = obj.transform.Find("ItemCounter").GetComponent<TMP_Text>();
            var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();

            Debug.Log("Item" + item);
            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;

            if (item.quantity > 1)
            {
                itemCounter.text = item.quantity.ToString();
            }
            else
            {
                itemCounter.text = "";
            }
            if (EnableRemove.isOn)
            {
                removeButton.gameObject.SetActive(true);
            }
        }
        //Option two
        for (int i = 0; i < Items.Count; i++)
        {
            Debug.Log(Items[i].itemType);

            obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var itemCounter = obj.transform.Find("ItemCounter").GetComponent<TMP_Text>();
            var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();

            itemName.text = Items[i].itemName;
            itemIcon.sprite = Items[i].icon;
            //itemCounter.text = Items[i].quantity.ToString();
            itemCounter.text = ItemsQuantity[i].ToString();

            if (EnableRemove.isOn)
            {
                removeButton.gameObject.SetActive(true);
            }
        }

        SetInventoryItems();
    }

    public void EnableItemsRemove()
    {
        if (EnableRemove.isOn)
        {
            foreach (Transform item in ItemContent)
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
    }*/
}

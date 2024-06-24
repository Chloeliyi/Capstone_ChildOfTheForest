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

    //public Transform SmallItemContent;

    public Toggle EnableRemove;

    public InventoryItemController[] InventoryItems;
    public List<int> ItemsQuantity = new List<int>();

    //public InventoryItemController[] SmallInventoryItems;

    public TMP_Text DescName;

    public Image DescIcon;

    public TMP_Text DescText;

    public int quantity;

    [SerializeField] private int itemQuantity = 1;

    [SerializeField] private int maxitemQuantity = 3;

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
        if (item.IsStackable())
        {
            bool ItemAlreadyInInventory = false;
            foreach (Item inventoryitem in Items)
            {
                if (inventoryitem.itemType == item.itemType)
                {
                    inventoryitem.quantity += itemQuantity;
                    //ItemsQuantity.Add(itemQuantity);
                    ItemAlreadyInInventory = true;
                }
            }
            if (!ItemAlreadyInInventory)
            {
                Items.Add(item);
                //ItemsQuantity.Add(itemQuantity);
            }
        }
        else
        {
            Items.Add(item);
            //ItemsQuantity.Add(itemQuantity);
        }
        /*if (Items.Count == 0)
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
            else
            {
                Debug.Log("Look through list");
                for (int a = 0; a < Items.Count; a++)
                {
                    if (Items[a] != item)
                    {
                        Debug.Log("Create new slot");
                        Debug.Log("Index A :" + a);
                        Debug.Log(item);
                        Items.Add(item);
                        ItemsQuantity.Add(0);
                        //break;
                    }
                    else
                    {
                        Debug.Log("Found other slot");
                        for (int b = 0; b < Items.Count; b++)
                        {
                            if (Items[b]  == item)
                            {
                                //Debug.Log("Index B :" + b);
                                if (ItemsQuantity[b] < maxitemQuantity)
                                {
                                    counter = b;
                                    Debug.Log("Index B :" + b);
                                    ItemsQuantity[counter] ++;
                                }
                            }
                            //break;
                        }
                    }
                }
            }
        }*/
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
        /*foreach (var item in Items)
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
        }*/
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
    }

}

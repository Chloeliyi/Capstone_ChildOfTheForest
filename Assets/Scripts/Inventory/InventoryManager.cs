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
        Items.Add(item);
        /*if (item.IsStackable())
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
        }*/
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
                Debug.Log("Create new slot");
                //Items.Add(item);
                //ItemsQuantity.Add(itemQuantity);
                for (int a = 0; a < Items.Count; a++)
                {
                    if (Items[a] != item)
                    {
                        Items.Add(item);
                        ItemsQuantity.Add(itemQuantity);
                    }
                    else
                    {
                        if (ItemsQuantity[a] < maxitemQuantity)
                        counter = a;
                    }
                }
            }*/
            /*else if (Items[counter] != item)
            {
                Debug.Log("Counter is not item");
                for (int i = 0; i < Items.Count; i++)
                {
                    if (Items[i] == item)
                    {
                        Debug.Log("Index : " + i);
                        Debug.Log(Items[i]);
                        Debug.Log(ItemsQuantity[i]);
                        if (ItemsQuantity[i] < maxitemQuantity)
                        {
                            counter = i;
                            Debug.Log("Counter : " + counter);
                            if (ItemsQuantity[counter] < maxitemQuantity)
                            {
                                Debug.Log("Below max items");
                                ItemsQuantity[counter] ++;
                            } 
                            else if (ItemsQuantity[counter] > maxitemQuantity)
                            {
                                Debug.Log("New Item : " + item);
                                Items.Add(item);
                                ItemsQuantity.Add(0);
                            }
                        }
                    }
                    else if (Items[i] != item)
                    {
                        Debug.Log(Items[i]);
                        Debug.Log("New Item : " + item);
                        //Items.Add(item);
                        //ItemsQuantity.Add(0);
                        for (int a = 0; a < Items.Count; a++)
                        {
                            if (Items[a] == item)
                            {
                                counter = a;
                                Debug.Log(counter);
                                if (ItemsQuantity[counter] < maxitemQuantity)
                                {
                                    Debug.Log("Below max items");
                                    ItemsQuantity[counter] ++;
                                } 
                                else if (ItemsQuantity[counter] > maxitemQuantity)
                                {
                                    Debug.Log("New Item : " + item);
                                    Items.Add(item);
                                    ItemsQuantity.Add(0);
                                }
                            }
                            else
                            {
                                Items.Add(item);
                                ItemsQuantity.Add(0);
                            }
                        }
                    }
                }
            }*/
        //}
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

    /*public void ClearSmallContent()
    {
        foreach(Transform item in SmallItemContent)
        {
            Destroy(item.gameObject);
        }

    }

    public void ListSmallInvenItems()
    {
        //ClearSmallContent();
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

            /*foreach (Transform item in SmallItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(true);
            }*/
        }
        else
        {
            foreach(Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }

            /*foreach(Transform item in SmallItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }*/
        }       
    }


    public void SetInventoryItems()
    {
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < Items.Count; i++)
        {
            /*if (Items[i] == )
            {
                counter ++;
            }*/
            InventoryItems[i].AddItem(Items[i]);
            Debug.Log(Items[i]);
        }
    }

    /*public void SetSmallInventoryItems()
    {
        SmallInventoryItems = SmallItemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < Items.Count; i++)
        {
            SmallInventoryItems[i].AddItem(Items[i]);
        }
    }*/
}

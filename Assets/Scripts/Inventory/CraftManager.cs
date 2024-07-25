using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CraftManager : MonoBehaviour
{
    public static CraftManager Instance;

    public List<Item> CraftableItems = new List<Item>();

    public List<string> craftItemName = new List<string>();

    public List<int> craftItemsQuantity = new List<int>();

    //public Transform CraftedContent;

    public CraftItem[] craftSlot;

    public InventoryItemController[] itemData;

    [SerializeField] private int counter;

    public Image CraftedIcon;

    public TMP_Text CraftedName;

    public bool GotItem = false;

    [SerializeField] private InventoryManager inventoryManager;
    

    void Start()
    {
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }

    public void Add(string itemName)
    {
        craftItemName.Add(itemName);
    }

    public void AddQuantity(int itemQuantity)
    {
        craftItemsQuantity.Add(itemQuantity);
        CreateItem();
    }

    public void CreateItem()
    {
        for (int i = 0; i < craftItemName.Count; i++)
        {
            if (craftItemName[i] == "Branch")
            {
                Debug.Log("Branch");
                Debug.Log("Index : " + i);
                if (craftItemsQuantity[i] == 1)
                {
                    for (int j = 0; j < craftItemName.Count; j++)
                    {
                        if (craftItemName[j] == "Crystal")
                        {
                            if (craftItemsQuantity[j] >= 1)
                            {
                                Debug.Log("Can create spear");
                                GotItem = true;

                                foreach (var item in CraftableItems)
                                {
                                    if (item.itemName == "Spear")
                                    {
                                        counter = 1;
                                        CraftedName.text = CraftableItems[counter].itemName;
                                        CraftedIcon.sprite = CraftableItems[counter].icon;
                                        CraftedIcon.enabled = true;
                                    }
                                }
                            }
                        }
                    }
                }
                else if (craftItemsQuantity[i] == 2)
                {
                    for (int a = 0; a < craftItemName.Count; a++)
                    {
                        if (craftItemName[a] == "Crystal")
                        {
                            if (craftItemsQuantity[a] >= 3)
                            {
                                Debug.Log("Can create axe");
                                GotItem = true;

                                foreach (var item in CraftableItems)
                                {
                                    if (item.itemName == "Axe")
                                    {
                                        counter = 2;
                                        CraftedName.text = CraftableItems[counter].itemName;
                                        CraftedIcon.sprite = CraftableItems[counter].icon;
                                        CraftedIcon.enabled = true;
                                    }
                                }
                            }
                        }
                    }
                }
                else if (craftItemsQuantity[i] >= 3)
                {    
                    Debug.Log("Branch can create torch");
                    GotItem = true;
                    foreach (var item in CraftableItems)
                    {
                        if (item.itemName == "Torch")
                        {
                            counter = 0;
                            CraftedName.text = CraftableItems[counter].itemName;
                            CraftedIcon.sprite = CraftableItems[counter].icon;
                            CraftedIcon.enabled = true;
                        }
                    }
                }
                else
                {
                    Debug.Log("Return item to inventory");
                    RemoveFromInventory();
                }
            }
            /*else if (craftItemName[i] == "Crystal")
            {
                if (craftItemsQuantity[i] == 1)
                {
                    for (int b = 0; b < craftItemName.Count; b++)
                    {
                        if (craftItemName[b] == "Branch")
                        {
                            if (craftItemsQuantity[b] == 1)
                            {
                                Debug.Log("Can create spear");
                                GotItem = true;

                                foreach (var item in CraftableItems)
                                {
                                    if (item.itemName == "Spear")
                                    {
                                        counter = 1;
                                        CraftedName.text = CraftableItems[counter].itemName;
                                        CraftedIcon.sprite = CraftableItems[counter].icon;
                                    }
                                }
                            }
                        }
                    }
                }
                else if (craftItemsQuantity[i] == 3)
                {
                    for (int c = 0; c < craftItemName.Count; c++)
                    {
                        if (craftItemName[c] == "Branch")
                        {
                            if (craftItemsQuantity[c] == 2)
                            {
                                Debug.Log("Can create axe");
                                GotItem = true;

                                foreach (var item in CraftableItems)
                                {
                                    if (item.itemName == "Axe")
                                    {
                                        counter = 2;
                                        CraftedName.text = CraftableItems[counter].itemName;
                                        CraftedIcon.sprite = CraftableItems[counter].icon;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Return item to inventory");
                RemoveFromInventory();
            }*/
        }
    }

    public void RemoveFromInventory()
    {
        for (int i = 0; i < craftSlot.Length; i++) 
        {
            if (craftSlot[i].draggableItem != null)
            {
                if (counter == 0)
                {
                    if (craftSlot[i].draggableItem.itemData.itemName == "Branch")
                    Debug.Log("Adjust branch quantity");
                    {
                        craftSlot[i].draggableItem.itemData.quantity -= 3;
                        if (craftSlot[i].draggableItem.itemData.quantity <= 0)
                        {
                            craftSlot[i].draggableItem.itemData.EmptySlot();
                            /*craftSlot[i].draggableItem.itemData.quantity = 0;
                            craftSlot[i].draggableItem.itemData.quantityText.text = craftSlot[i].draggableItem.itemData.quantity.ToString();
                            craftSlot[i].draggableItem.itemData.itemName = "";
                            craftSlot[i].draggableItem.itemData.itemNameText.text = craftSlot[i].draggableItem.itemData.itemName;
                            craftSlot[i].draggableItem.itemData.itemSprite = null;
                            craftSlot[i].draggableItem.itemData.itemImage.sprite = craftSlot[i].draggableItem.itemData.itemSprite;
                            craftSlot[i].draggableItem.itemData.itemDescription = "";
                            craftSlot[i].draggableItem.itemData.ItemDescriptionImage.sprite = craftSlot[i].draggableItem.itemData.itemSprite;
                            craftSlot[i].draggableItem.itemData.ItemDescriptionNameText.text = craftSlot[i].draggableItem.itemData.itemName;
                            craftSlot[i].draggableItem.itemData.ItemDescriptionText.text = craftSlot[i].draggableItem.itemData.itemDescription;*/
                        }
                        else
                        {
                            Debug.Log("Quantity : " + craftSlot[i].draggableItem.itemData.quantity);
                            craftSlot[i].draggableItem.itemData.quantityText.text = craftSlot[i].draggableItem.itemData.quantity.ToString();
                            craftSlot[i].draggableItem.itemData.itemNameText.text = craftSlot[i].draggableItem.itemData.itemName;
                            craftSlot[i].draggableItem.itemData.itemImage.sprite = craftSlot[i].draggableItem.itemData.itemSprite;
                            craftSlot[i].draggableItem.itemData.ItemDescriptionImage.sprite = craftSlot[i].draggableItem.itemData.itemSprite;
                            craftSlot[i].draggableItem.itemData.ItemDescriptionNameText.text = craftSlot[i].draggableItem.itemData.itemName;
                            craftSlot[i].draggableItem.itemData.ItemDescriptionText.text = craftSlot[i].draggableItem.itemData.itemDescription;
                        }
                    }
                    if (craftSlot[i].draggableItem.itemData.itemName != "Branch")
                    {
                        Debug.Log("Item in slot is not branch");
                        craftSlot[i].draggableItem.itemData.quantityText.text = craftSlot[i].draggableItem.itemData.quantity.ToString();
                        craftSlot[i].draggableItem.itemData.itemNameText.text = craftSlot[i].draggableItem.itemData.itemName;
                        craftSlot[i].draggableItem.itemData.itemImage.sprite = craftSlot[i].draggableItem.itemData.itemSprite;
                        craftSlot[i].draggableItem.itemData.ItemDescriptionImage.sprite = craftSlot[i].draggableItem.itemData.itemSprite;
                        craftSlot[i].draggableItem.itemData.ItemDescriptionNameText.text = craftSlot[i].draggableItem.itemData.itemName;
                        craftSlot[i].draggableItem.itemData.ItemDescriptionText.text = craftSlot[i].draggableItem.itemData.itemDescription;
                    }
                }
                else if (counter == 1)
                {
                    if (craftSlot[i].draggableItem.itemData.itemName == "Crystal")
                    {
                        craftSlot[i].draggableItem.itemData.quantity -= 1;
                        if (craftSlot[i].draggableItem.itemData.quantity <= 0)
                        {
                            craftSlot[i].draggableItem.itemData.EmptySlot();
                            Debug.Log("Empty crystal");
                        }
                        else
                        {
                            Debug.Log("Quantity : " + craftSlot[i].draggableItem.itemData.quantity);
                            craftSlot[i].draggableItem.itemData.quantityText.text = craftSlot[i].draggableItem.itemData.quantity.ToString();
                            craftSlot[i].draggableItem.itemData.itemNameText.text = craftSlot[i].draggableItem.itemData.itemName;
                            craftSlot[i].draggableItem.itemData.itemImage.sprite = craftSlot[i].draggableItem.itemData.itemSprite;
                            craftSlot[i].draggableItem.itemData.ItemDescriptionImage.sprite = craftSlot[i].draggableItem.itemData.itemSprite;
                            craftSlot[i].draggableItem.itemData.ItemDescriptionNameText.text = craftSlot[i].draggableItem.itemData.itemName;
                            craftSlot[i].draggableItem.itemData.ItemDescriptionText.text = craftSlot[i].draggableItem.itemData.itemDescription;
                        }
                    }
                    else if (craftSlot[i].draggableItem.itemData.itemName == "Branch")
                    {
                        craftSlot[i].draggableItem.itemData.quantity -= 1;
                        if (craftSlot[i].draggableItem.itemData.quantity <= 0)
                        {
                            craftSlot[i].draggableItem.itemData.EmptySlot();
                            Debug.Log("Empty branch");
                        }
                        else
                        {
                            Debug.Log("Quantity : " + craftSlot[i].draggableItem.itemData.quantity);
                            craftSlot[i].draggableItem.itemData.quantityText.text = craftSlot[i].draggableItem.itemData.quantity.ToString();
                            craftSlot[i].draggableItem.itemData.itemNameText.text = craftSlot[i].draggableItem.itemData.itemName;
                            craftSlot[i].draggableItem.itemData.itemImage.sprite = craftSlot[i].draggableItem.itemData.itemSprite;
                            craftSlot[i].draggableItem.itemData.ItemDescriptionImage.sprite = craftSlot[i].draggableItem.itemData.itemSprite;
                            craftSlot[i].draggableItem.itemData.ItemDescriptionNameText.text = craftSlot[i].draggableItem.itemData.itemName;
                            craftSlot[i].draggableItem.itemData.ItemDescriptionText.text = craftSlot[i].draggableItem.itemData.itemDescription;
                        }
                    }
                    if (craftSlot[i].draggableItem.itemData.itemName != "Branch" || craftSlot[i].draggableItem.itemData.itemName != "Crystal")
                    {
                        craftSlot[i].draggableItem.itemData.quantityText.text = craftSlot[i].draggableItem.itemData.quantity.ToString();
                        craftSlot[i].draggableItem.itemData.itemNameText.text = craftSlot[i].draggableItem.itemData.itemName;
                        craftSlot[i].draggableItem.itemData.itemImage.sprite = craftSlot[i].draggableItem.itemData.itemSprite;
                        craftSlot[i].draggableItem.itemData.ItemDescriptionImage.sprite = craftSlot[i].draggableItem.itemData.itemSprite;
                        craftSlot[i].draggableItem.itemData.ItemDescriptionNameText.text = craftSlot[i].draggableItem.itemData.itemName;
                        craftSlot[i].draggableItem.itemData.ItemDescriptionText.text = craftSlot[i].draggableItem.itemData.itemDescription;
                    }
                }
                else if (counter == 2)
                {
                    if (craftSlot[i].draggableItem.itemData.itemName == "Branch")
                    {
                        craftSlot[i].draggableItem.itemData.quantity -= 2;
                        if (craftSlot[i].draggableItem.itemData.quantity <= 0)
                        {
                            craftSlot[i].draggableItem.itemData.EmptySlot();
                        }
                        else
                        {
                            Debug.Log("Quantity : " + craftSlot[i].draggableItem.itemData.quantity);
                            craftSlot[i].draggableItem.itemData.quantityText.text = craftSlot[i].draggableItem.itemData.quantity.ToString();
                            craftSlot[i].draggableItem.itemData.itemNameText.text = craftSlot[i].draggableItem.itemData.itemName;
                            craftSlot[i].draggableItem.itemData.itemImage.sprite = craftSlot[i].draggableItem.itemData.itemSprite;
                            craftSlot[i].draggableItem.itemData.ItemDescriptionImage.sprite = craftSlot[i].draggableItem.itemData.itemSprite;
                            craftSlot[i].draggableItem.itemData.ItemDescriptionNameText.text = craftSlot[i].draggableItem.itemData.itemName;
                            craftSlot[i].draggableItem.itemData.ItemDescriptionText.text = craftSlot[i].draggableItem.itemData.itemDescription;
                        }
                    }
                    else if (craftSlot[i].draggableItem.itemData.itemName == "Crystal")
                    {
                        craftSlot[i].draggableItem.itemData.quantity -= 3;
                        if (craftSlot[i].draggableItem.itemData.quantity <= 0)
                        {
                            craftSlot[i].draggableItem.itemData.EmptySlot();
                        }
                        else
                        {
                            craftSlot[i].draggableItem.itemData.quantityText.text = craftSlot[i].draggableItem.itemData.quantity.ToString();
                            craftSlot[i].draggableItem.itemData.itemNameText.text = craftSlot[i].draggableItem.itemData.itemName;
                            craftSlot[i].draggableItem.itemData.itemImage.sprite = craftSlot[i].draggableItem.itemData.itemSprite;
                            craftSlot[i].draggableItem.itemData.ItemDescriptionImage.sprite = craftSlot[i].draggableItem.itemData.itemSprite;
                            craftSlot[i].draggableItem.itemData.ItemDescriptionNameText.text = craftSlot[i].draggableItem.itemData.itemName;
                            craftSlot[i].draggableItem.itemData.ItemDescriptionText.text = craftSlot[i].draggableItem.itemData.itemDescription;
                        }
                    }
                    if (craftSlot[i].draggableItem.itemData.itemName != "Branch" || craftSlot[i].draggableItem.itemData.itemName != "Crystal")
                    {
                        craftSlot[i].draggableItem.itemData.quantityText.text = craftSlot[i].draggableItem.itemData.quantity.ToString();
                        craftSlot[i].draggableItem.itemData.itemNameText.text = craftSlot[i].draggableItem.itemData.itemName;
                        craftSlot[i].draggableItem.itemData.itemImage.sprite = craftSlot[i].draggableItem.itemData.itemSprite;
                        craftSlot[i].draggableItem.itemData.ItemDescriptionImage.sprite = craftSlot[i].draggableItem.itemData.itemSprite;
                        craftSlot[i].draggableItem.itemData.ItemDescriptionNameText.text = craftSlot[i].draggableItem.itemData.itemName;
                        craftSlot[i].draggableItem.itemData.ItemDescriptionText.text = craftSlot[i].draggableItem.itemData.itemDescription;
                    }
                }
            }
            craftSlot[i].RemoveSlotItem();
        }
    }

    [SerializeField] private int quantity;

    public void AddToInventory()
    {
        if (GotItem == true)
        {
            RemoveFromInventory();
            Debug.Log("Add Item To Inventory");
            inventoryManager.leftOverItems = inventoryManager.AddItem(CraftableItems[counter].itemName, quantity, CraftableItems[counter].icon, CraftableItems[counter].itemDesc);
            if (inventoryManager.leftOverItems <= 0)
            {
            }
            else
            {
            quantity = inventoryManager.leftOverItems;
            }

            craftItemName.Clear();
            craftItemsQuantity.Clear();

            CraftedName.text = null;
            CraftedIcon.sprite = null;
            CraftedIcon.enabled = false;

            GotItem = false;

        }
        else if (GotItem == false)
        {
            Debug.Log("Got nothing to add");
        }
    }
}

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

    public List<string> craftItems = new List<string>();

    public List<int> craftItemsQuantity = new List<int>();

    public Transform CraftedContent;

    private CraftItem craftItem;

    [SerializeField] private int counter;

    public Image CraftedIcon;

    public TMP_Text CraftedName;

    public bool GotItem = false;

    public void Add(string craftItem)
    {
        craftItems.Add(craftItem);
        CreateItem();
        /*if (craftItems.Count == 0)
        {
            craftItems.Add(craftItem);
            CreateItem();
        }
        else
        {
            for (int i = 0; i < craftItems.Count; i++)
            {
                if (craftItem[i] == craftItem)
                {

                }
            }
        }*/
    }

    public void AddQuantity(int itemQuantity)
    {
        craftItemsQuantity.Add(itemQuantity);
    }

    public void CreateItem()
    {
        /*for (int i = 0; i < craftItems.Count; i++)
        {
            if (craftItems[i] == "Branch")
            {
                for (int j = 0; j < craftItems.Count; j++)
                {
                    if (craftItems[j] == "CrystalDrop")
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
                    else if (craftItems[j] == "Branch")
                    {
                        for (int a = 0; a < craftItems.Count; a++)
                        {
                            if (craftItems[a] == "Branch")
                            {
                                Debug.Log("Can create torch");
                                GotItem = true;

                                foreach (var item in CraftableItems)
                                {
                                    if (item.itemName == "Torch")
                                    {
                                        counter = 0;
                                        CraftedName.text = CraftableItems[counter].itemName;
                                        CraftedIcon.sprite = CraftableItems[counter].icon;
                                    }
                                }
                            }
                            else if (craftItems[a] == "CrystalDrop")
                            {
                                for (int b = 0; b < craftItems.Count; b++)
                                {
                                    if (craftItems[b] == "CrystalDrop")
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
                }
            }
        }*/
        for (int i = 0; i < craftItems.Count; i++)
        {
             if (craftItems[i] == "Branch")
             {
                if (craftItemsQuantity[i] == 1)
                {
                    for (int j = 0; j < craftItems.Count; j++)
                    {
                        if (craftItems[j] == "CrystalDrop")
                        {
                            if (craftItemsQuantity[j] == 1)
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
                else if (craftItemsQuantity[i] == 2)
                {
                    for (int a = 0; a < craftItems.Count; a++)
                    {
                        if (craftItems[a] == "CrystalDrop")
                        {
                            if (craftItemsQuantity[a] == 3)
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
                else if (craftItemsQuantity[i] == 3)
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
                        }
                    }
                }
             }
        }
    }

    public void RemoveFromInventory()
    {
        craftItem = CraftedContent.GetComponentInChildren<CraftItem>();
        craftItem.RemoveCraftItem();
        InventoryManager.Instance.ListItems();
    }

    public void AddToInventory()
    {

        if (GotItem == true)
        {
            Debug.Log("Add To Inventory");
            InventoryManager.Instance.Add(CraftableItems[counter]);

            craftItems.Clear();

        CraftedName.text = null;
        CraftedIcon.sprite = null;

        GotItem = false;

        RemoveFromInventory();

        }
        else if (GotItem == false)
        {
            Debug.Log("Got nothing to add");
        }
    }
}

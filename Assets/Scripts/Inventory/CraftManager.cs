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


    public Transform CraftedContent;

    private CraftItem craftItem;

    [SerializeField] private int counter;

    public Image CraftedIcon;

    public TMP_Text CraftedName;

    public bool GotItem = false;

    public void Add(string craftItem)
    {
        craftItems.Add(craftItem);
    }

    public void CreateItem()
    {
        for (int i = 0; i < craftItems.Count; i++)
        {
             if(craftItems[i] == "Branch")
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

             /*else if ()
             {
                Debug.Log("Can create axe");
                GotItem = true;

                foreach (var item in CraftableItems)
                {
                    if (item.itemName == "Axe")
                    {
                        counter = 1;
                        CraftedName.text = CraftableItems[counter].itemName;
                        CraftedIcon.sprite = CraftableItems[counter].icon;
                    }
                }
             }*/
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

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

    //public InventoryItemController[] InventoryItems;

    public Transform CraftedContent;

    private CraftItem craftItem;

    [SerializeField] private int counter;

    public Image CraftedIcon;

    public TMP_Text CraftedName;

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

                /*GameObject obj = Instantiate(CraftedItem, CraftedContent);
                var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
                var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
                var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();

                itemName.text = CraftableItems[0].itemName;
                itemIcon.sprite = CraftableItems[0].icon;*/

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

             //SetCraftedItem();
        }
    }

    /*public void SetCraftedItem()
    {
        InventoryItems = CraftedContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < CraftableItems.Count; i++)
        {
            InventoryItems[i].AddItem(CraftableItems[counter]);
            Debug.Log(CraftableItems[counter]);
        }
    }*/

    public void AddToInventory()
    {
        InventoryManager.Instance.Add(CraftableItems[counter]);

        InventoryManager.Instance.ListItems();

        craftItems.Clear();

        CraftedName.text = null;
        CraftedIcon.sprite = null;

        //craftItem = CraftedContent.GetComponentInChildren<CraftItem>();

        craftItem.RemoveCraftItem();
    }
}

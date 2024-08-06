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

    //public Sprite[] iconSprites;

    public Item[] itemSOs;

    public int leftOverItems;

    public bool UseItem(string itemName)
    {
        for (int i = 0; i < itemSOs.Length; i++) 
        {
            if (itemSOs[i].itemName == itemName)
            {
                bool usable = itemSOs[i].UseItem();
                Debug.Log("Usable is " + usable);
                return usable;
            }
        }
        return false;
    }


    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        for (int i = 0; i < itemSlot.Length; i++) 
        {
            if (itemSlot[i].isFull == false && itemSlot[i].itemName == itemName || itemSlot[i].quantity == 0)
            {
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite,  itemDescription);
                if (leftOverItems > 0)
                {
                    leftOverItems = AddItem(itemName, quantity, itemSprite,  itemDescription);
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
            itemSlot[i].isDraggable = false;
        }
    }
}

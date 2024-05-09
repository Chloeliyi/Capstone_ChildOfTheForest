using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItemController : MonoBehaviour
{
    Item item;

    public Button RemoveButton;

    public void RemoveItem()
    {
        //Debug.Log(item.itemName);

        InventoryManager.Instance.Remove(item);

        Destroy(gameObject);
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
    }

    public void UseItem()
    {
        switch (item.itemType)
        {
            case Item.ItemType.Potion:
            FPSController.Instance.IncreaseHealth(item.value);
            break;
            case Item.ItemType.Food:
            FPSController.Instance.IncreaseFood(item.value);
            break;
        }

        RemoveItem();
    }
}

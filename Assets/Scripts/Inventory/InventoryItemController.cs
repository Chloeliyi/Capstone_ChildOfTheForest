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
        InventoryManager.Instance.Remove(item);

        Destroy(gameObject);
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
    }

    public void OnButtonHover()
    {
        InventoryManager.Instance.DescName.text = item.itemName;
        InventoryManager.Instance.DescIcon.sprite = item.icon;
        InventoryManager.Instance.DescText.text = item.itemDesc;
    }

    public void OnButtonHoverExit()
    {
        InventoryManager.Instance.DescName.text = "Empty";
        InventoryManager.Instance.DescIcon.sprite = null;
        InventoryManager.Instance.DescText.text = "Empty";
    }

    public void UseItem()
    {
        switch (item.itemType)
        {
            case Item.ItemType.Potion:
            GameManager.Instance.IncreaseHealth(item.value);
            break;
            case Item.ItemType.Food:
            GameManager.Instance.IncreaseFood(item.value);
            break;
        }

        RemoveItem();
    }
}

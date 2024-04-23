using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item Item;

    public InventoryManager inventoryManager;

    void PickUp()
    {
        InventoryManager.Instance.Add(Item);
        //inventoryManager.ListSmallInvenItems();
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        PickUp();
        inventoryManager.ListItems();
    }
}

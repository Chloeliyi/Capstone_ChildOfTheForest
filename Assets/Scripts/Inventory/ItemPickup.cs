using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item Item;

    public void PickUp()
    {
        InventoryManager.Instance.Add(Item);
        Debug.Log("Destroy Object");
        Destroy(gameObject);

        InventoryManager.Instance.ListSmallInvenItems();
    }

    /*private void OnMouseDown()
    {
        PickUp();
    }*/
    
}

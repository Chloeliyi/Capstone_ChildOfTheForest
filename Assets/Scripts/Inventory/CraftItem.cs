using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CraftItem : MonoBehaviour, IDropHandler
{
    public string craftItemName;
    public int itemQuantity;

    public CraftManager craftManager;

    public Draggable draggableItem;

    private void Start ()
    {
        craftManager = GameObject.Find("CraftManager").GetComponent<CraftManager>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("On drop");
        //if (transform.childCount == 0) 
        if (transform.childCount == 3) 
        {
            Debug.Log("Item is in slot");
            GameObject dropped = eventData.pointerDrag;
            draggableItem = dropped.GetComponent<Draggable>();
            draggableItem.ItemSlot = transform;

            craftItemName = draggableItem.itemData.itemName;
            itemQuantity = draggableItem.itemData.quantity;
            Debug.Log("Item Name : " + craftItemName);
            Debug.Log("Quantity " + itemQuantity);
            craftManager.Add(craftItemName);
            craftManager.AddQuantity(itemQuantity);
        }
        else
        {
            Debug.Log(transform.childCount);
        }
    }

    public void RemoveSlotItem()
    {
        Debug.Log("Remove Craft Item");
        if (draggableItem != null)
        {
            draggableItem.ReturnToPosition();
        }
        //draggableItem.transform.SetParent(draggableItem.ItemSlot);
        //draggableItem.transform.localPosition = new Vector3(0f, 0f, 0f);
        
    }
}

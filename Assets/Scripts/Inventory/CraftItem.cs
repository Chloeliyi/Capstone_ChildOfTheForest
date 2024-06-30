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

    //public Image craftImage;
    //public TMP_Text craftQuantity;

    public CraftManager craftManager;

    public Draggable draggableItem;

    private void Start ()
    {
        craftManager = GameObject.Find("CraftManager").GetComponent<CraftManager>();
        //craftImage.enabled = false;
        //craftQuantity.text = "";
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("On drop");
        //if (transform.childCount == 0) 
        Debug.Log(transform.childCount);
        if (transform.childCount == 2) 
        {
            Debug.Log("Item is in slot");
            GameObject dropped = eventData.pointerDrag;
            draggableItem = dropped.GetComponent<Draggable>();
            draggableItem.ItemSlot = transform;
            //draggableItem.IvenSlot = transform;
            //draggableItem.IvenSlot = draggableItem.ItemSlot;
            //transform.SetParent(draggableItem.ItemSlot);
            //transform.localPosition = new Vector3(0f, 0f, 0f);

            craftItemName = draggableItem.itemData.itemName;
            itemQuantity = draggableItem.itemData.quantity;
            Debug.Log("Item Name : " + craftItemName);
            Debug.Log("Quantity " + itemQuantity);
            craftManager.Add(craftItemName);
            craftManager.AddQuantity(itemQuantity);
            
            /*draggableData = dropped.GetComponent<InventoryItemController>();

            craftItemName = draggableData.itemName;
            Debug.Log("Item Name " + craftItemName);
            itemQuantity = draggableData.quantity;
            Debug.Log("Quantity " + itemQuantity);
            craftManager.Add(craftItemName);
            craftManager.AddQuantity(itemQuantity);

            craftImage.sprite = draggableData.itemSprite;
            craftImage.enabled = true;
            craftQuantity.text = draggableData.quantity.ToString();*/

        }
        else
        {
            Debug.Log("Got Item");
            
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

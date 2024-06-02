using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CraftItem : MonoBehaviour, IDropHandler
{
    public static CraftItem Instance;

    public string craftItem;

    public CraftManager craftManager;

    private InventoryItemController draggableItem;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("On drop");
        if (transform.childCount == 0) 
        {
            Debug.Log("Item is in slot");
            GameObject dropped = eventData.pointerDrag;
            draggableItem = dropped.GetComponent<InventoryItemController>();
            draggableItem.IvenSlot = transform;
            //draggableItem.transform.position = new Vector3(0f, 0f, 0f);

            Debug.Log(draggableItem.item.itemName);
            craftItem = draggableItem.item.itemName;
            craftManager.Add(craftItem);

            craftManager.CreateItem();
        }
    }

    public void RemoveCraftItem()
    {
        draggableItem.RemoveItem();
    }
}

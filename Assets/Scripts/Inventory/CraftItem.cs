using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CraftItem : MonoBehaviour, IDropHandler
{
    //public static CraftItem Instance;

    public string craftItem;

    public int itemQuantity;

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
            Debug.Log("transform: " + transform.position);
            //draggableItem.IvenMenu = transform.parent;
            //draggableItem.transform.position = new Vector3(0f, 0f, 0f);

            var itemCounter = draggableItem.transform.Find("ItemCounter").GetComponent<TMP_Text>();

            craftItem = draggableItem.item.itemName;
            Debug.Log(craftItem);

            int.TryParse(itemCounter.text, out itemQuantity);
            craftManager.AddQuantity(itemQuantity);
            craftManager.Add(craftItem);

        }
    }

    public void RemoveCraftItem()
    {
        Debug.Log("Remove Craft Item");
        draggableItem.RemoveItem();
        InventoryManager.Instance.ClearContent();
    }
}

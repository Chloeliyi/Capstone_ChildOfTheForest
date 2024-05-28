using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CraftItem : MonoBehaviour, IDropHandler
{
    /*public Image craftIcon;

    public GameObject craftItem;

    public InventoryItemController ivenItem;*/

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("On drop");
        if (transform.childCount == 0) 
        {
            Debug.Log("Item is in slot");
            GameObject dropped = eventData.pointerDrag;
            InventoryItemController draggableItem = dropped.GetComponent<InventoryItemController>();
            draggableItem.parentAfterDrag = transform;
            Debug.Log(draggableItem.parentAfterDrag);
        }
        /*if (eventData.pointerDrag != null)
        {
            ivenItem.SetCraftItem(craftIcon);
            ivenItem.RemoveItem();

            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }*/
    }

    public void AddCraftItems(GameObject craftItem)
    {
        craftItem = craftItem;
    }
}

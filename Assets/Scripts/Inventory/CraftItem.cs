using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CraftItem : MonoBehaviour, IDropHandler
{
    /*public Image craftIcon;

    public InventoryItemController ivenItem;*/

    //public string craftItem;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("On drop");
        if (transform.childCount == 0) 
        {
            Debug.Log("Item is in slot");
            GameObject dropped = eventData.pointerDrag;
            InventoryItemController draggableItem = dropped.GetComponent<InventoryItemController>();
            draggableItem.parentAfterDrag = transform;
            //draggableItem.transform.position = new Vector3(0f, 0f, 0f);

            //craftItem = "Branch";
            //CraftManager.Instance.Add(craftItem);
        }
    }
}

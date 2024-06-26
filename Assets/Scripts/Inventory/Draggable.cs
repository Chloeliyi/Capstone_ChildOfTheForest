using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Draggable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //private InventoryItemController inventoryItemController;
    public Transform IvenSlot;
    public Transform IvenMenu;
    [SerializeField] private Image image;

    private void Start()
    {
        image = this.GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log(item.itemName + " begin drag");
        IvenSlot = transform.parent;
        IvenMenu = IvenSlot.parent;
        //transform.SetParent(IvenMenu);
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log(item.itemName + " being dragged");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log(item.itemName + " end drag");
        transform.SetParent(IvenSlot);
        //Debug.Log("Parent : " + IvenSlot);
        transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        image.raycastTarget = true;
    }
}

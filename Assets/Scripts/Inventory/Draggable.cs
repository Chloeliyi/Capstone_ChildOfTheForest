using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Draggable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject ItemContent;
    public Transform ItemSlot;
    public Transform IvenSlot;
    public Transform IvenMenu;
    [SerializeField] private Image image;

    [SerializeField] private InventoryItemController itemData;

    private void Start()
    {
        itemData = this.GetComponentInParent<InventoryItemController>();
        image = this.GetComponentInChildren<Image>();
        Debug.Log("Can drag item");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Item Name : " + itemData.itemName);
        Debug.Log("Item Sprite : " + itemData.itemSprite);
        Debug.Log("Item Quantity : " + itemData.quantity);
        ItemSlot = transform.parent;
        Debug.Log(ItemSlot);
        IvenSlot = ItemSlot.parent;
        IvenMenu = IvenSlot.parent;
        //transform.SetParent(IvenMenu);

        transform.SetParent(transform.root);
        Debug.Log(transform.root);
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
        Debug.Log(" end drag");
        //transform.SetParent(IvenSlot);
        //Debug.Log("Parent : " + IvenSlot);
        transform.SetParent(ItemSlot);
        transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        image.raycastTarget = true;
    }
}

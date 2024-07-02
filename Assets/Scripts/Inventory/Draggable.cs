using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Draggable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //public GameObject ItemContent;
    public Transform ItemSlot;
    public Transform IvenSlot;
    public Transform IvenMenu;
    public Transform currentSlot;
    [SerializeField] private Image image;

    public InventoryItemController itemData;

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
        currentSlot = transform.parent;
        IvenSlot = ItemSlot.parent;
        IvenMenu = IvenSlot.parent;

        transform.SetParent(transform.root);
        Debug.Log(transform.root);
        //transform.SetParent(IvenMenu);
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
        Debug.Log("End drag");
        //transform.SetParent(IvenSlot);
        transform.SetParent(ItemSlot);
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
        transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        itemData.thisItemSelected = false;
        itemData.selectedShader.SetActive(false);
        //itemData.isDraggable = false;
        image.raycastTarget = true;
    }

    public void ReturnToPosition()
    {
        Debug.Log("Return to position");
        transform.SetParent(currentSlot);
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
    }
}

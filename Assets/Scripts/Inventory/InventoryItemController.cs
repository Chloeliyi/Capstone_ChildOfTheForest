using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItemController : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    Item item;

    public Button RemoveButton;

    //private RectTransform rectTransform;

    public Transform parentAfterDrag;

    //public Transform ivenMenu;

    public Image image;

    void Awake()
    {
        //rectTransform = GetComponent<RectTransform>();
    }

    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);

        Destroy(gameObject);
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log(item.itemName + " begin drag");
        parentAfterDrag = transform.parent;
        Debug.Log(parentAfterDrag);
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(item.itemName + " being dragged");
        transform.position = Input.mousePosition;
        //rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log(item.itemName + " end drag");
        transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }
    
    public void OnButtonHover()
    {
        InventoryManager.Instance.DescName.text = item.itemName;
        InventoryManager.Instance.DescIcon.sprite = item.icon;
        InventoryManager.Instance.DescText.text = item.itemDesc;
    }

    public void OnButtonHoverExit()
    {
        InventoryManager.Instance.DescName.text = "Empty";
        InventoryManager.Instance.DescIcon.sprite = null;
        InventoryManager.Instance.DescText.text = "Empty";
    }

    public void SetCraftItem(Image craftIcon/*, GameObject craftItem*/)
    {
        craftIcon.sprite = item.icon;
        //craftItem = ;
    }

    public void UseItem()
    {
        switch (item.itemType)
        {
            case Item.ItemType.Potion:
            GameManager.Instance.IncreaseHealth(item.value);
            RemoveItem();
            break;
            case Item.ItemType.Food:
            GameManager.Instance.IncreaseFood(item.value);
            RemoveItem();
            break;
        }

        //RemoveItem();
    }
}

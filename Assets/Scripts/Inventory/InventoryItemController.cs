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

    Transform parentAfterDrag;

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
        //parentAfterDrag = transform.parent;
        //transform.SetParent(transform.root);
        //transform.SetAsLastSibling();
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
        //transform.SetParent(parentAfterDrag);
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

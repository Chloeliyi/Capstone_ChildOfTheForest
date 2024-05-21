using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItemController : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    Item item;

    public Button RemoveButton;

    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
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
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(item.itemName + " being dragged");
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log(item.itemName + " end drag");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("On Pointer Down");
    }

    /*public void OnDragItem()
    {
        Debug.Log(item.itemName + " begin drag");
    }

    public void OnBeingDragged()
    {
        Debug.Log(item.itemName + " being dragged");
        //Create a ray going from the camera through the mouse position
        Ray ray = Camera.main.ScreenPointToRay(data.position);
        //Calculate the distance between the Camera and the GameObject, and go this distance along the ray
        Vector3 rayPoint = ray.GetPoint(Vector3.Distance(transform.position, Camera.main.transform.position));
        //Move the GameObject when you drag it
        transform.position = rayPoint;
    }

    public void OnStopDragItem()
    {
        Debug.Log(item.itemName + " end drag");
    }*/
    
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
        //craftItem = item.G;
    }

    public void UseItem()
    {
        switch (item.itemType)
        {
            case Item.ItemType.Potion:
            GameManager.Instance.IncreaseHealth(item.value);
            break;
            case Item.ItemType.Food:
            GameManager.Instance.IncreaseFood(item.value);
            break;
        }

        RemoveItem();
    }
}

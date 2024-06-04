using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItemController : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Item item;

    public Button RemoveButton;

    public Transform IvenSlot;

    public Transform IvenMenu;

    public Image image;

    public CraftManager craftManager;

    void Awake()
    {
    }

    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);

        Destroy(gameObject);

        InventoryManager.Instance.DescName.text = "Empty";
        InventoryManager.Instance.DescIcon.sprite = null;
        InventoryManager.Instance.DescText.text = "Empty";
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log(item.itemName + " begin drag");
        IvenSlot = transform.parent;
        IvenMenu = IvenSlot.parent;
        //transform.SetParent(IvenMenu);
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(item.itemName + " being dragged");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log(item.itemName + " end drag");
        transform.SetParent(IvenSlot);
        Debug.Log("Parent : " + IvenSlot);
        transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
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
            case Item.ItemType.Weapon:
            GameManager.Instance.SpawnTorch();
            RemoveItem();
            break;
        }

        //RemoveItem();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CraftItem : MonoBehaviour, IDropHandler
{
    public string craftItemName;
    public int itemQuantity;

    [SerializeField] private Image craftImage;
    [SerializeField] private TMP_Text craftQuantity;

    public CraftManager craftManager;

    private InventoryItemController draggableData;

    private Draggable draggableItem;

    private void Start ()
    {
        craftManager = GameObject.Find("CraftManager").GetComponent<CraftManager>();
        craftImage = this.GetComponentInChildren<Image>();
        //this.GetComponentInChildren<Image>().enabled = false;

        craftQuantity = this.GetComponentInChildren<TMP_Text>();
        //this.GetComponentInChildren<TMP_Text>().enabled = false;
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("On drop");
        //if (transform.childCount == 0) 
        Debug.Log(transform.childCount);
        if (transform.childCount == 2) 
        {
            Debug.Log("Item is in slot");
            GameObject dropped = eventData.pointerDrag;
            draggableItem = dropped.GetComponent<Draggable>();
            draggableItem.IvenSlot = transform;
            //transform.SetParent(draggableItem.ItemSlot);
            draggableData = dropped.GetComponent<InventoryItemController>();
            Debug.Log("transform: " + transform.position);

            craftItemName = draggableData.itemName;
            Debug.Log(craftItemName);
            itemQuantity = draggableData.quantity;
            craftManager.Add(craftItemName);
            craftManager.AddQuantity(itemQuantity);

            //craftImage.sprite = draggableData.itemSprite;
            //this.GetComponentInChildren<Image>().enabled = true;

            //craftQuantity.text = draggableData.quantity.ToString();
            //this.GetComponentInChildren<TMP_Text>().enabled = true;

        }
        else
        {
            Debug.Log("Got Item");
            
        }
    }

    public void RemoveCraftItem()
    {
        Debug.Log("Remove Craft Item");
        //draggableItem.RemoveItem();
        //InventoryManager.Instance.ClearContent();
    }
}

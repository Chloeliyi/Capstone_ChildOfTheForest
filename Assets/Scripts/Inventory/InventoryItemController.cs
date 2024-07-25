using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItemController : MonoBehaviour, IPointerClickHandler/*, IBeginDragHandler, IEndDragHandler, IDragHandler*/
{
    [Header("Item data")]
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public bool isDraggable = false;
    public string itemDescription;
    public Sprite emptySprite;

    [SerializeField] private int maxNumberOfItems;

    [Header("Item data")]
    public TMP_Text itemNameText;
    public TMP_Text quantityText;
    public Image itemImage;

    [Header("Item description slot")]
    public Image ItemDescriptionImage;
    public TMP_Text ItemDescriptionNameText;
    public TMP_Text ItemDescriptionText;

    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        this.GetComponentInChildren<Draggable>().enabled = false;
    }

    private void Update()
    {
        Drag();
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        //Check to see if slot is already full
        if (isFull)
        return quantity;

        //Update name
        this.itemName = itemName;
        itemNameText.text = itemName;
        //Update sprite
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;
        itemImage.enabled = true;
        //Update description
        this.itemDescription = itemDescription;
        
        //Update quantity
        this.quantity += quantity;
        if (this.quantity >= maxNumberOfItems)
        {
            quantityText.text = maxNumberOfItems.ToString();
            quantityText.enabled = true;
            isFull = true;

            //Return leftovers
            int extraItems = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;
            return extraItems;
        };
        //Update quantity text
        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;

        return 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick() 
    {
        if (thisItemSelected)
        {
            bool usable = inventoryManager.UseItem(itemName);
            Debug.Log(itemName);
            if (usable)
            {
                Debug.Log("Usable");
                this.quantity -= 1;
                quantityText.text = this.quantity.ToString();
                if (this.quantity <= 0)
                {   
                    EmptySlot();
                }
            }

            if (!usable)
            {
                Debug.Log("Not usable");
            }

            if (itemName == "Branch" ||itemName == "Crystal")
            {
                isDraggable = true;
            }
            else
            {
                isDraggable = false;
            }
        }
        else
        {
            inventoryManager.DeselectAllSlots();
            selectedShader.SetActive(true);
            thisItemSelected = true;
            ItemDescriptionNameText.text = itemName;
            ItemDescriptionText.text = itemDescription;
            ItemDescriptionImage.sprite = itemSprite;
            ItemDescriptionImage.enabled = true;

            if(ItemDescriptionImage.sprite == null)
            {
                ItemDescriptionImage.enabled = false;
            }
        }
    }

    public void Drag()
    {
        if (isDraggable)
        {
            this.GetComponentInChildren<Draggable>().enabled = true;
        }
    }

    public void EmptySlot()
    {
        itemNameText.text = "";
        quantityText.enabled = false;
        itemImage.sprite = null;
        itemImage.enabled = false;
        ItemDescriptionNameText.text = "";
        ItemDescriptionText.text = "";
        ItemDescriptionImage.sprite = null;
        ItemDescriptionImage.enabled = false;
    }

    public void OnRightClick()
    {
        Debug.Log("Drop item from inventory");
        this.quantity -= 1;
        quantityText.text = this.quantity.ToString();

        if (this.quantity <= 0)
        {   
            EmptySlot();
        }
    }

    /*public Item item;

    public Button RemoveButton;

    public Transform IvenSlot;

    public Transform IvenMenu;

    public Image image;

    public CraftManager craftManager;

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
    
    public void OnButtonHover()
    {
        Debug.Log("On hover");
        InventoryManager.Instance.DescName.text = item.itemName;
        InventoryManager.Instance.DescIcon.sprite = item.icon;
        InventoryManager.Instance.DescText.text = item.itemDesc;
    }

    public void OnButtonHoverExit()
    {
        Debug.Log("No hover");
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
            case Item.ItemType.Torch:
            GameManager.Instance.SpawnTorch();
            RemoveItem();
            break;
            case Item.ItemType.Axe:
            GameManager.Instance.SpawnAxe(item.durability);
            RemoveItem();
            break;
            case Item.ItemType.Spear:
            GameManager.Instance.SpawnSpear(item.durability);
            RemoveItem();
            break;
            case Item.ItemType.Workbench:
            GameManager.Instance.SpawnWorkbench();
            RemoveItem();
            break;
            case Item.ItemType.Wall:
            GameManager.Instance.SpawnWall();
            RemoveItem();
            break;
        }

        //RemoveItem();
    }*/
}

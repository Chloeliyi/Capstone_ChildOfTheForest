using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    //public GameObject SmallInventoryMenu;

    [SerializeField] private bool menuActivated;

    public List<Item> Items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    //public Transform SmallItemContent;

    public Toggle EnableRemove;

    public InventoryItemController[] InventoryItems;

    //public InventoryItemController[] SmallInventoryItems;

    public TMP_Text DescName;

    public Image DescIcon;

    public TMP_Text DescText;

    public int itemQuantity;

    [SerializeField] private int maxitemQuantity = 5;

    public void Start()
    {
        DescName.text = "Item";
        DescIcon.sprite = null;
        DescText.text = "Item Description";
    }
    public void Awake()
    {
        Instance = this;
    }

    void Update()
    {
    }

    public void Add(Item item)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i] == item)
            {
                Debug.Log("Already have item");
                itemQuantity += item.quantity;

                if (itemQuantity >= maxitemQuantity)
                {
                    Items.Add(item);
                    itemQuantity = item.quantity;
                }
            }
            else
            {
                Debug.Log("Don't have item");
                Items.Add(item);
                itemQuantity = item.quantity;
            }
        }

        //Items.Add(item);
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
    }

    private GameObject obj;

    private GameObject desc;

    public void ClearContent()
    {
        //Clear content before open
        foreach(Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
    }

    public void ListItems()
    {
        foreach (var item in Items)
        {
            obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var itemCounter = obj.transform.Find("ItemCounter").GetComponent<TMP_Text>();
            var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;

            itemCounter.text = itemQuantity.ToString();

            if (EnableRemove.isOn)
            {
                removeButton.gameObject.SetActive(true);
            }
        }

        SetInventoryItems();
    }

    /*public void ClearSmallContent()
    {
        foreach(Transform item in SmallItemContent)
        {
            Destroy(item.gameObject);
        }

    }

    public void ListSmallInvenItems()
    {
        //ClearSmallContent();
        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, SmallItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;

            if (EnableRemove.isOn)
            {
                removeButton.gameObject.SetActive(true);
            }
        }

        SetSmallInventoryItems();
    }*/

    public void EnableItemsRemove()
    {
        if (EnableRemove.isOn)
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(true);
            }

            /*foreach (Transform item in SmallItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(true);
            }*/
        }
        else
        {
            foreach(Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }

            /*foreach(Transform item in SmallItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }*/
        }       
    }

    public void SetInventoryItems()
    {
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < Items.Count; i++)
        {
            InventoryItems[i].AddItem(Items[i]);
            Debug.Log(Items[i]);
        }
    }

    /*public int counter;

    public void SetSmallInventoryItems()
    {
        SmallInventoryItems = SmallItemContent.GetComponentsInChildren<InventoryItemController>();

        for (counter = 0; counter < Items.Count; counter++)
        {
            SmallInventoryItems[counter].AddItem(Items[counter]);
        }
    }*/
}

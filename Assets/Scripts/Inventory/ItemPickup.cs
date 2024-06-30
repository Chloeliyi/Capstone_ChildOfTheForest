using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item Item;

    public bool NearItem;

    [SerializeField] private int quantity;

    [SerializeField] private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }

    public void PickUp()
    {
        inventoryManager.leftOverItems = inventoryManager.AddItem(Item.itemName, quantity, Item.icon, Item.itemDesc);
        if (inventoryManager.leftOverItems <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            quantity = inventoryManager.leftOverItems;
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (NearItem)
            {
                PickUp();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player is within range of " + gameObject.name);
            NearItem = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player is within range of item");
            Debug.Log("Near item is : " + NearItem);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player is not within range of item");
            NearItem = false;
        }
    }
    
}

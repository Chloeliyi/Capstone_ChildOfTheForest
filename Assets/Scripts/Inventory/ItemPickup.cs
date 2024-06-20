using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item Item;

    public bool NearItem;

    public void PickUp()
    {
        InventoryManager.Instance.Add(Item);
        Debug.Log("Pick up object");
        Destroy(gameObject);
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
            Debug.Log("Player is within range of item");
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

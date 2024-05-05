using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    public Item Item;

    public int treeHealth = 10;

    public GameObject Branch;

    void Start()
    {
        treeHealth = 10;
    }

    
    void Update()
    {
        if (treeHealth == 0)
        {
            Destroy(gameObject);
            //Instantiate(Branch);
            PickUp();
        }
    }

    void PickUp()
    {
        InventoryManager.Instance.Add(Item);
        //Destroy(gameObject);
    }
}

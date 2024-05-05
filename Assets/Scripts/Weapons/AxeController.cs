using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    public Item Item;

    public TreeController treeController;

    public int AxeDurability = 10;

    void Start()
    {
        AxeDurability = 10;
    }

    public void TakeAxeDamage()
    {
        treeController.treeHealth -= Item.value;
        AxeDurability -= 1;
        Debug.Log("Tree health:" + treeController.treeHealth);
        Debug.Log("Axe Durability:" + AxeDurability);

        /*if (treeController.treeHealth >= 0)
        {
            treeController.treeHealth -= Item.value;
            AxeDurability -= 1;
            Debug.Log("Tree health:" + treeController.treeHealth);
            Debug.Log("Axe Durability:" + AxeDurability);
        }
        else
        {
            Debug.Log("Tree has been cut");
        }*/
    }
}

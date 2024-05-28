using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CraftManager : MonoBehaviour
{

    public CraftItem[] CraftObjects;

    public List<CraftItem> craftItems = new List<CraftItem>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    /*public void AddCraftObjects(CraftItem craftItem)
    {
        craftItems.Add(craftItem);
    }

    public void SetCraftObjects()
    {
        for (int i = 0; i < craftItems.Count; i++)
        {
            CraftObjects[i].AddCraftItems(craftItems[i]);
            Debug.Log(craftItems[i]);
        }
    }*/
}

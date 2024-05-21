using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CraftItem : MonoBehaviour, IDropHandler
{
    public Image craftIcon;

    public GameObject craftItem;

    public InventoryItemController ivenItem;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("On drop");
        if (eventData.pointerDrag != null)
        {
            ivenItem.SetCraftItem(craftIcon/*, craftItem*/);
            ivenItem.RemoveItem();

            //eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }

    public void AddCraftItems(GameObject craftItem)
    {
        craftItem = craftItem;
    }
}

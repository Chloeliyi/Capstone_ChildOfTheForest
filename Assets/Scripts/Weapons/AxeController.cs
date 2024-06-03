using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    public Item AxeItem;

    void Start()
    {
        AxeItem.durability = 40;
    }

    public void DestroyAxe()
    {
        Destroy(gameObject);
        GameManager.Instance.AxeGameObject = null;
        GameManager.Instance.activeAxe = false;
    }
}

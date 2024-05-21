using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    public Item AxeItem;

    public void DestroyAxe()
    {
        Destroy(gameObject);
        GameManager.Instance.AxeGameObject = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    public Item AxeItem;

    public bool PickUpAxe;

    public BoxCollider box;

    public int AxeDurability;

    void Start()
    {
        box.isTrigger = true;
        AxeDurability = AxeItem.durability;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E is pressed");
            if (PickUpAxe == true)
            {
                Destroy(gameObject);
                GameManager.Instance.SpawnAxe(AxeDurability);
                box.isTrigger = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player is within range of axe");
            PickUpAxe = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player is not within range of axe");
            PickUpAxe= false;
        }
    }

    public void AxeDamage()
    {
        AxeDurability -= AxeItem.value;
        GameManager.Instance.AxeDamage(AxeItem.value);
    }


    public void DestroyAxe()
    {
        Destroy(gameObject);
        GameManager.Instance.AxeGameObject = null;
        GameManager.Instance.activeAxe = false;
        GameManager.Instance.WeaponIcon = null;
        GameManager.Instance.Weapondurability.value = GameManager.Instance.MaxAxeDurability;
    }
}

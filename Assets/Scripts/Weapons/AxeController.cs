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
        //box.isTrigger = true;
        AxeDurability = AxeItem.durability;
        PickUpAxe = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (PickUpAxe == true)
            {
                //box.isTrigger = false;
                Destroy(gameObject);
                GameManager.Instance.SpawnAxe(AxeDurability);
            }
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Near enemy");
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (GameManager.Instance.activeAxe)
                {
                    Debug.Log(collision.gameObject.tag + " was hit");
                    Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                    AxeDamage();
                    enemy.TakeAxeDamage(AxeItem.value);
                }
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
        Debug.Log("Axe Durability : " + AxeDurability);
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

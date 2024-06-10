using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileAddon : MonoBehaviour
{

    //public static ProjectileAddon Instance;

    public Item SpearItem;
    
    private Rigidbody rb;

    private bool targetHit;

    public BoxCollider box;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //box = GetComponent<BoxCollider>();
        box.isTrigger = true;

        SpearItem.durability = 100;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E is pressed");
            if (PickUpSpear == true)
            {
                Destroy(gameObject);
                GameManager.Instance.SpawnSpear(SpearItem.durability);
                box.isTrigger = false;
            }
        }

        if (SpearItem.durability == 0)
        {
            Destroy(gameObject);
            GameManager.Instance.projectile = null;
            GameManager.Instance.ActiveSpear = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            rb.isKinematic = true;
            DeparentProjectile();
            Debug.Log(collision.gameObject.tag + " was hit");
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeSpearDamage(SpearItem.value);
            SpearItem.durability -= SpearItem.value;
            box.isTrigger = true;

            //Destroy(gameObject);

        }
        else if (collision.gameObject.tag == "Ground")
        {
            rb.isKinematic = true;
            DeparentProjectile();
            Debug.Log(collision.gameObject.tag + " was hit");
            SpearItem.durability -= SpearItem.value;
            box.isTrigger = true;
        }
        /*else 
        {
            rb.isKinematic = true;
            Debug.Log("Not enemy");
        }*/

        // make sure only to stick to the first target you hit

        /*if (targetHit)
            return;
        else
            targetHit = true;

        if(collision.gameObject.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            enemy.TakeDamage(damage);

            // destroy projectile
            Destroy(gameObject);
        }

        // make sure projectile sticks to surface
        rb.isKinematic = true;

        // make sure projectile moves with target
        transform.SetParent(collision.transform);*/
    }

    public void DeparentProjectile()
    {
        gameObject.transform.SetParent(null);
    }

    public bool PickUpSpear;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player is within range of spear");
            PickUpSpear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player is not within range of spear");
            PickUpSpear = false;
        }
    }
}
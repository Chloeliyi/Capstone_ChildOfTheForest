using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAddon : MonoBehaviour
{
    public int damage;

    private Rigidbody rb;

    private bool targetHit;

    public ThrowingTutorial throwTut;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            rb.isKinematic = true;
            //DeparentProjectile();
            Debug.Log("Enemy was hit");

            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            enemy.TakeDamage(damage);

            Destroy(gameObject);

        }
        else
        {
            Destroy(gameObject);

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
}
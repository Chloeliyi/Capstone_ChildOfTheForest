using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    public Item AxeItem;

    public bool PickUpAxe;

    //public BoxCollider box;

    public bool NearWendigo;
    public bool NearBear;
    public bool NearWolf;
    public bool NearCrystal;

    public Enemy wendigoController;
    public BearController bearController;
    public WolfController wolfController;
    public CrystalController crystalController;

    public int AxeDurability;

    public Animator AxeAttack;

    [SerializeField] private string Attack = "Axe Rig_Attack";

    [SerializeField] private int waitTimer = 1;
    [SerializeField] private bool PlayOnce = false;

    void Start()
    {
        //box.isTrigger = true;
        AxeDurability = AxeItem.durability;
        PickUpAxe = false;
        axeRb = gameObject.GetComponent<Rigidbody>();
        axeRb.constraints = RigidbodyConstraints.FreezeAll;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (PickUpAxe == true)
            {
                //box.isTrigger = false;
                Destroy(gameObject);
                //gameObject.SetActive(false);
                GameManager.Instance.SpawnAxe(AxeDurability);
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (GameManager.Instance.activeAxe)
            {
                if (!PlayOnce)
                {
                    Debug.Log("Swing");
                    AxeAttack.Play(Attack, 0, 0.0f);
                    PlayOnce = true;
                    StartCoroutine(PauseAxeSwing());
                }
            }
            if (NearCrystal)
            {
                Debug.Log("Attack crystal");
                crystalController.TakeAxeDamage(AxeItem.value);
                AxeDamage();
            }

            if (NearWendigo)
            {
                Debug.Log("Attack wendigo");
                wendigoController.TakeAxeDamage(AxeItem.value);
                AxeDamage();
            }

            if (NearBear)
            {
                Debug.Log("Attack bear");
                bearController.TakeAxeDamage(AxeItem.value);
                AxeDamage();
            }

            if (NearWolf)
            {
                Debug.Log("Attack wolf");
                wolfController.TakeAxeDamage(AxeItem.value);
                AxeDamage();
            }

            if (AxeDurability <= 0)
            {
                DestroyAxe();
            }
        }
    }

    private IEnumerator PauseAxeSwing()
    {
        yield return new WaitForSeconds(waitTimer);
        AxeAttack.SetTrigger("Attack");
        PlayOnce = false;
    }

    Rigidbody axeRb;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Crystal")
        {
            Debug.Log("Near crystal");
            NearCrystal = true; 
        }
        if (collision.gameObject.tag == "Wendigo")
        {
            Debug.Log("Near wendigo");
            NearWendigo = true; 
        }

        else if (collision.gameObject.tag == "Bear")
        {
            Debug.Log("Near bear");
            NearBear = true; 
        }

        else if (collision.gameObject.tag == "Wolf")
        {
            Debug.Log("Near wolf");
            NearWolf = true; 
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Crystal")
        {
            Debug.Log("Near crystal");
            NearCrystal = false; 
        }
        if (collision.gameObject.tag == "Wendigo")
        {
            Debug.Log("Leaving wendigo");
            NearWendigo = false; 
        }
        else if (collision.gameObject.tag == "Bear")
        {
            Debug.Log("Leaving bear");
            NearBear = false; 
        }
        else if (collision.gameObject.tag == "Wolf")
        {
            Debug.Log("Leaving wolf");
            NearWolf = false; 
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
        //Destroy(gameObject);
        //GameManager.Instance.AxeGameObject = null;
        GameManager.Instance.AxeGameObject.SetActive(false);
        GameManager.Instance.activeAxe = false;
        GameManager.Instance.WeaponIcon = null;
        GameManager.Instance.Weapondurability.value = GameManager.Instance.MaxAxeDurability;
    }
}

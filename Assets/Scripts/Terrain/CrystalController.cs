using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour
{

    public int crystalValue;

    public AxeController axeController;

    public GameObject crystalDrop;

    [SerializeField] private GameObject crystalDropObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player is within range of crystal");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (GameManager.Instance.activeAxe == true)
            {
                Debug.Log("Have axe");
                if(Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Crystal is being chopped");
                    crystalValue -= axeController.AxeItem.value;
                    axeController.AxeDamage();
                    Debug.Log("Crystal : " + crystalValue);
                    Debug.Log("Axe Durability : " + axeController.AxeItem.durability);
                    if (crystalValue <= 0)
                    {
                        Debug.Log("Crystal is dropped");
                        SpawnCrystalDrop();
                        Destroy(gameObject);
                    }
                    else if (axeController.AxeDurability <= 0)
                    {
                        axeController.DestroyAxe();
                    }
                }
            }
        }
    }

    public void SpawnCrystalDrop()
    {
        //crystalDropObject = Instantiate(crystalDrop, gameObject.transform.position, gameObject.transform.rotation);
        GameManager.Instance.Repair();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player is not within range of crystal");
        }
    }
}

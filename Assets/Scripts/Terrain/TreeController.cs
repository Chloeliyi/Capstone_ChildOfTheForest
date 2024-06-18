using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class TreeController : MonoBehaviour
{
    [SerializeField] private int treeHealth;

    public AxeController axeController;

    [SerializeField] private GameObject tree;

    public GameObject Branch;

    [SerializeField] private GameObject branchGameObject;

    void Start()
    {
        tree = transform.gameObject;
        treeHealth = 30;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player is within range of tree");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player is not within range of tree");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (GameManager.Instance.activeAxe == true)
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Tree is being cut");
                    treeHealth -= axeController.AxeItem.value;
                    axeController.AxeDamage();
                    Debug.Log("Tree health : " + treeHealth);
                    Debug.Log("Axe Durability : " + axeController.AxeItem.durability);
                    if (treeHealth <= 0)
                    {
                        Debug.Log("Tree has been cut");
                        SpawnBranch();
                        Destroy(tree);
                    }
                    else if (axeController.AxeDurability <= 0)
                    {
                        axeController.DestroyAxe();
                    }
                }
            }
            else
            {
                Debug.Log("No Axe");
            }
        }
    }
    public void SpawnBranch()
    {
        branchGameObject = Instantiate(Branch, tree.gameObject.transform.position, tree.gameObject.transform.rotation);
    }
}

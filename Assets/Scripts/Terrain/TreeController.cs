using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class TreeController : MonoBehaviour
{

    public int treeHealth;

    public GameManager gameManager;

    public AxeController axeController;

    public bool axeInRange;

    [SerializeField] private GameObject tree;

    public GameObject Branch;

    [SerializeField] private GameObject branchGameObject;

    void Start()
    {
        tree = transform.parent.gameObject;
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
            /*if(Input.GetKeyDown(KeyCode.M))
                {
                    Debug.Log("Tree is being cut");
                    treeHealth -= 2;
                    Debug.Log("Tree health:" + treeHealth);

                    if (treeHealth == 0)
                    {
                        Debug.Log("Tree has been cut");
                        SpawnBranch();
                        Destroy(tree);
                    }
                }*/
            if (GameManager.Instance.activeAxe == true)
            {
                Debug.Log("Got Axe");

                if(Input.GetKeyDown(KeyCode.N))
                {
                    Debug.Log("Tree is being cut");
                    treeHealth -= axeController.AxeItem.value;
                    axeController.AxeItem.durability -= axeController.AxeItem.value;
                    Debug.Log("Tree health : " + treeHealth);
                    Debug.Log("Axe Durability : " + axeController.AxeItem.durability);
                    if (treeHealth <= 0)
                    {
                        Debug.Log("Tree has been cut");
                        SpawnBranch();
                        Destroy(tree);
                    }
                    else if (axeController.AxeItem.durability <= 0)
                    {
                        //axeInRange = false;
                        axeController.DestroyAxe();
                    }
                }
            }
            else if (GameManager.Instance.activeAxe == false)
            {
                Debug.Log("No Axe");

                if(Input.GetKeyDown(KeyCode.N))
                {
                    Debug.Log("Tree cannot be cut");
                }
            }
        }
    }

    public void SpawnBranch()
    {
        branchGameObject = Instantiate(Branch, tree.gameObject.transform.position, tree.gameObject.transform.rotation);
    }

}

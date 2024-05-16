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

    [SerializeField] public GameObject branchGameObject;

    void Start()
    {
        tree = transform.parent.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            axeInRange = true;
            Debug.Log("Player is within range of tree");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            axeInRange = false;
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

            if (gameManager.AxeGameObject != null)
            {
                Debug.Log("Got Axe");

                if(Input.GetKeyDown(KeyCode.M))
                {
                    Debug.Log("Tree is being cut");
                    treeHealth -= axeController.AxeItem.value;
                    Debug.Log("Tree health : " + treeHealth);
                    if (treeHealth <= 0)
                    {
                        Debug.Log("Tree has been cut");
                        SpawnBranch();
                        Destroy(tree);
                    }
                }
            }
            else if (gameManager.AxeGameObject == null)
            {
                Debug.Log("No Axe");

                if(Input.GetKeyDown(KeyCode.M))
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

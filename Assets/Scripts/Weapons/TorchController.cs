using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchController : MonoBehaviour
{
    public Item TorchItem;
    public bool PickUpTorch;
    public BoxCollider box;
    // Start is called before the first frame update
    void Start()
    {
        box.isTrigger = true;
        gameObject.GetComponentInChildren<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E is pressed");
            if (PickUpTorch == true)
            {
                Destroy(gameObject);
                GameManager.Instance.SpawnTorch();
                box.isTrigger = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player is within range of torch");
            PickUpTorch = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player is not within range of torch");
            PickUpTorch = false;
        }
    }
}

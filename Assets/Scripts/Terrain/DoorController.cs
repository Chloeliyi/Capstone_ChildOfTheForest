using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator Door;
    [SerializeField] private string DoorOpen = "DoorOpen";
    [SerializeField] private int waitTimer = 2;
    [SerializeField] private bool PlayOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        //Door = gameObject.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            Debug.Log("Player is in range of door");
            if (!PlayOnce)
            {
                Door.Play(DoorOpen, 0, 0.0f);
                Debug.Log("Door Is Open");
                PlayOnce = true;
                Debug.Log("PlayOnce is : " + PlayOnce);
                StartCoroutine(PauseDoorInteraction());
            }
        }
    }

    private IEnumerator PauseDoorInteraction()
    {
        yield return new WaitForSeconds(waitTimer);
        Door.SetTrigger("Start");
        Debug.Log("Door Is Closed");
        PlayOnce = false;
        Debug.Log("PlayOnce is : " + PlayOnce);
    }
}

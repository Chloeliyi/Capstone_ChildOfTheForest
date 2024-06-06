using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyRayCast : MonoBehaviour
{
    [SerializeField] private int rayLength = 5;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string excluseLayerName = null;

    private ItemPickup raycastedObject;
    [SerializeField] private KeyCode EKey = KeyCode.E;

    //[SerializeField] private Image crosshair = null;
    private bool isCrosshairActive;
    private bool doOnce;

    private string interactableTag = "InteractiveObject";

    private void Update()
    {

        Debug.Log("Raycast");
        RaycastHit hitInfo;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        int mask = 1 << LayerMask.NameToLayer(excluseLayerName) | layerMaskInteract.value;

        if (Physics.Raycast(transform.position, fwd, out hitInfo, rayLength, mask))
        {
            if(hitInfo.collider.CompareTag(interactableTag))
            {
                Debug.Log("Item in range");
                if (!doOnce)
                {
                    raycastedObject = hitInfo.collider.gameObject.GetComponent<ItemPickup>();
                    //CrosshairChange(true);
                }

                //isCrosshairActive = true;
                doOnce = true;

                if(Input.GetKeyDown(EKey))
                {
                    Debug.Log("Got item");
                    raycastedObject.PickUp();
                }
            }
        }
        else
        {
            if (isCrosshairActive)
            {
                //CrosshairChange(false);
                doOnce = false;
            }
        }
    }

    /*void CrosshairChange (bool on)
    {
        if (on && !doOnce)
        {
            crosshair.color = Color.red;
        }
        else
        {
            crosshair.color = Color.yellow;
            isCrosshairActive = false;
        }
    }*/
}

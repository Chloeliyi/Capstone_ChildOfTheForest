using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearController : MonoBehaviour
{
    public void DestroySpear()
    {
        Destroy(gameObject);
        GameManager.Instance.projectile = null;
        GameManager.Instance.ActiveSpear = false;
    }
}

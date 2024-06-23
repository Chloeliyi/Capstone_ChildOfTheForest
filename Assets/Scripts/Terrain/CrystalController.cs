using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour
{

    [SerializeField] private int crystalValue;

    //public AxeController axeController;

    public GameObject crystalDrop;

    [SerializeField] private GameObject crystalDropObject;
    [SerializeField] private GameObject crystal;

    // Start is called before the first frame update
    void Start()
    {
        crystalValue = 50;
        crystal = transform.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeAxeDamage(int axedamage)
    {
        crystalValue -= axedamage;

        Debug.Log("Crystal Value : " + crystalValue);
        if (crystalValue <= 0)
        {
            Debug.Log("Crystal is dropped");
            SpawnCrystalDrop();
            Destroy(gameObject);
        }
    }

    public void SpawnCrystalDrop()
    {
        crystalDropObject = Instantiate(crystalDrop, crystal.gameObject.transform.position, crystal.gameObject.transform.rotation);
        GameManager.Instance.Repair();
    }
}

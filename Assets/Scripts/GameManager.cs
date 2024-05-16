using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    /*public Camera MenuCam;
    public GameObject StartMenu;*/
    public Transform playerCamera;

    public int MaxHealth = 30;

    public int CurrentHealth;

    public int MaxFood = 30;

    public int CurrentFood;

    public TMP_Text HealthText;

    public TMP_Text FoodText;

    public Slider Healthslider;

    public Slider Foodslider;

    public GameObject SmallIvenMenu;

    public GameObject BigIvenMenu;

    public GameObject ItemDescMenu;

    public GameObject CraftMenu;

    public GameObject Axe;

    public Transform parent;

    public Quaternion rotation;

    public GameObject AxeGameObject;

    public GameObject Spear;

    public Transform attackPoint; 

    public GameObject Tree;

    int AmountOfTrees = 1;

    void Start()
    {        
        CurrentHealth = MaxHealth;
        Healthslider.value = CurrentHealth;
        HealthText.text = $"HP:{CurrentHealth}";

        CurrentFood = MaxFood;
        Foodslider.value = CurrentFood;
        FoodText.text = $"Food:{CurrentFood}";

        SpawnTrees();

        readyToThrow = true;
    }

    public void Awake()
    {
        Instance = this;
    }

    public void TimeStop()
    {
        Time.timeScale = 0;
    }
    
    public void TimeStart()
    {
        Time.timeScale = 1;
    }

    public void SpawnTrees()
    {
         for (var i = 0; i <= AmountOfTrees; i++)
        {
            Vector3 SpawnPosition = new Vector3(Random.Range(-13, 13), 0, Random.Range(-13, 13));

            Instantiate(Tree, SpawnPosition, Quaternion.identity);
        }
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Quit");
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            TakeHealthDamage(3);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeFoodDamage(3);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (AxeGameObject == null)
            {

                AxeGameObject = Instantiate(Axe, parent);
            
                AxeGameObject.name = "PlayerAxe";
            }
            else if (AxeGameObject != null)
            {
                if (AxeGameObject.activeSelf)
                {
                    AxeGameObject.gameObject.SetActive(false);
                }
                else if (!AxeGameObject.activeSelf)
                {
                    AxeGameObject.gameObject.SetActive(true);
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            Throw();
        }
    }

    public void OpenCraftMenu()
    {
        SmallIvenMenu.gameObject.SetActive(false);
        BigIvenMenu.gameObject.SetActive(true);
        ItemDescMenu.gameObject.SetActive(false);
        CraftMenu.gameObject.SetActive(true);
    }

    public void CloseCraftMenu()
    {
        SmallIvenMenu.gameObject.SetActive(true);
        BigIvenMenu.gameObject.SetActive(false);
        ItemDescMenu.gameObject.SetActive(true);
        CraftMenu.gameObject.SetActive(false);
    }

    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.T;
    public float throwForce;
    public float throwUpwardForce;

    bool readyToThrow;

    private void Throw()
    {
        readyToThrow = false;

        // instantiate object to throw
        Quaternion rotation = Quaternion.Euler(0f, 90f, 0f);
        GameObject projectile = Instantiate(Spear, attackPoint.position, rotation);

        // get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // calculate direction
        Vector3 forceDirection = playerCamera.transform.forward;

        RaycastHit hit;

        if(Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        // add force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

        // implement throwCooldown
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }

    void TakeHealthDamage(int damage)
    {
        CurrentHealth -= damage;
        Healthslider.value = CurrentHealth;
        HealthText.text = $"HP:{CurrentHealth}";
    }

    public void SetHealth()
    {
        Healthslider.value = CurrentHealth;
        HealthText.text = $"HP:{CurrentHealth}";
    }

    public void SetMaxHealth()
    {
        Healthslider.maxValue = MaxHealth;
        Healthslider.value = CurrentHealth;
        HealthText.text = $"HP:{CurrentHealth}";
    }

    public void IncreaseHealth(int value)
    {
        CurrentHealth += value;
        Healthslider.value = CurrentHealth;
        HealthText.text = $"HP:{CurrentHealth}";
    }

    void TakeFoodDamage(int damage)
    {
        CurrentFood -= damage;
        Foodslider.value = CurrentFood;
        FoodText.text = $"Food:{CurrentFood}";
    }

    public void SetFood()
    {
        Foodslider.value = CurrentFood;
        FoodText.text = $"Food:{CurrentFood}";
    }

    public void SetMaxFood()
    {
        Foodslider.maxValue = MaxFood;
        Foodslider.value = CurrentFood;
        FoodText.text = $"Food:{CurrentFood}";
    }

    public void IncreaseFood(int value)
    {
        CurrentFood += value;
        Foodslider.value = CurrentFood;
        FoodText.text = $"Food:{CurrentFood}";
    }

    /*public AxeController axeController;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Tree")
        {
            if (AxeGameObject != null)
            {
                if(Input.GetKeyDown(KeyCode.M))
                {
                    Debug.Log("Tree is being cut");
                    axeController.TakeAxeDamage();
                }
            }

            else if (AxeGameObject == null)
            {
                if(Input.GetKeyDown(KeyCode.M))
                {
                    Debug.Log("No Axe");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Tree")
        {
            Debug.Log("Too far from tree");
        }
    }*/
}

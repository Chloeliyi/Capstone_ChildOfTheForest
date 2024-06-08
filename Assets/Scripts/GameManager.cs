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

    public int MaxStamina = 30;

    public int CurrentStamina;

    public TMP_Text HealthText;

    public TMP_Text FoodText;

    public TMP_Text StaminaText;

    public Slider Healthslider;

    public Slider Foodslider;

    public Slider Staminaslider;

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

    public TreeController treeController;

    public GameObject WallObject;

    public GameObject Torch;

    public bool activeAxe;

    public bool ActiveSpear;

    int AmountOfTrees = 1;

    public Enemy enemy;

    public bool TakeDamage;

    //public string username;

    void Start()
    {        
        CurrentHealth = MaxHealth;
        Healthslider.value = CurrentHealth;
        HealthText.text = $"HP:{CurrentHealth}";

        CurrentFood = MaxFood;
        Foodslider.value = CurrentFood;
        FoodText.text = $"Food:{CurrentFood}";

        CurrentStamina = MaxStamina;
        Staminaslider.value = CurrentStamina;
        StaminaText.text = $"Stamina:{CurrentStamina}";

        //SpawnTrees();

        //readyToThrow = true;
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

        /*if (Input.GetKeyDown(KeyCode.G))
        {
            TakeHealthDamage(3);
        }*/

        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeFoodDamage(2);
        }

        if (CurrentFood <= 10)
        {

            if (CurrentHealth > 0)
            {
                TakeDamage = true;
                TakeHealthDamage(2);
            }

        }


        /*if (CurrentFood == MaxFood)
        {
            Debug.Log("Food full");
            if (CurrentHealth <= MaxHealth)
            {
                Debug.Log("Health is not full");
                StartCoroutine(HealthAttackIncrease(2));
            }
            else
            {
                Debug.Log("Health is full");
            }
        }*/

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (AxeGameObject == null)
            {
                AxeGameObject = Instantiate(Axe, parent);
            
                AxeGameObject.name = "PlayerAxe";
                activeAxe = true;
            }
            else if (AxeGameObject != null)
            {
                if (AxeGameObject.activeSelf)
                {
                    AxeGameObject.gameObject.SetActive(false);
                    activeAxe = false;
                }
                else if (!AxeGameObject.activeSelf)
                {
                    AxeGameObject.gameObject.SetActive(true);
                    activeAxe = true;
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (projectile == null)
            {
                SpawnSpear();
                Debug.Log("Spear is spawned");
                ActiveSpear = true;
            }
            else if (projectile != null)
            {
                if (projectile.activeSelf)
                {
                    projectile.gameObject.SetActive(false);
                    ActiveSpear = false;
                }
                else if (!projectile.activeSelf)
                {
                    projectile.gameObject.SetActive(true);
                    ActiveSpear = true;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("K is pressed");
            if (projectile != null)
            {
                Throw();
                Debug.Log("Spear is thrown");
            }
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
                if (projectile != null)
            {
                StartCoroutine(Stab());
                Debug.Log("Stab with spear");
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (Wallpart == null)
            {
                SpawnWall();
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Wallpart != null)
            {
                PlaceWall();
            }
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (enemy.activeWendigo == false)
            {
                enemy.activeWendigo = true;
                Debug.Log("Activate Wendigo");
            }
            else if (enemy.activeWendigo == true)
            {
                enemy.activeWendigo = false;
                Debug.Log("Deactivate Wendigo");
            }
        }

        if (CurrentHealth == 0)
        {
            Time.timeScale = 0f;
            FPSController.Instance.canMove = false;
            Debug.Log("You have died");
        }
        
    }

    GameObject Activebench;

    public GameObject Workbench;

    public void SpawnWorkbench()
    {
        Activebench = Instantiate(Workbench, parent);
    }

    public void PlaceWorkbench()
    {
       Activebench.transform.SetParent(null);

       Activebench.tag = "Workbench";

        float originalx = Activebench.transform.position.x;
        float xposition = originalx -= 1;
        Activebench.transform.Translate(new Vector3(0, xposition, 0));
    }

    public void OpenCraftMenu()
    {
        SmallIvenMenu.gameObject.SetActive(false);
        BigIvenMenu.gameObject.SetActive(true);
        ItemDescMenu.gameObject.SetActive(false);
        CraftMenu.gameObject.SetActive(true);

        InventoryManager.Instance.ListItems();
    }

    public void CloseCraftMenu()
    {
        SmallIvenMenu.gameObject.SetActive(true);
        BigIvenMenu.gameObject.SetActive(false);
        ItemDescMenu.gameObject.SetActive(true);
        CraftMenu.gameObject.SetActive(false);

        InventoryManager.Instance.ClearContent();
    }

    GameObject Wallpart;

    public void SpawnWall()
    {
        Wallpart = Instantiate(WallObject, parent);
    }

    public void PlaceWall()
    {
        Wallpart.transform.SetParent(null);

        float originalx = Wallpart.transform.position.x;
        float xposition = originalx -= 1;
        Wallpart.transform.Translate(new Vector3(0, xposition, 0));
    }

    public void GoToBed()
    {
        if (TimeManager.Instance.hours >= 20 || TimeManager.Instance.hours <= 6)
        {
            Debug.Log("Go To Bed");
            TimeManager.Instance.hours = 7;
        }
        else
        {
            Debug.Log("To early for bed");
        }
    }

    GameObject torchObject;

    public void SpawnTorch()
    {
        torchObject = Instantiate(Torch, parent);
    }

    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.T;
    public float throwForce;
    public float throwUpwardForce;

    bool readyToThrow;

    public GameObject projectile;

    Rigidbody projectileRb;

    public void SpawnSpear()
    {
        readyToThrow = false;

        projectile = Instantiate(Spear, parent);
        projectileRb = projectile.GetComponent<Rigidbody>();
        projectileRb.constraints = RigidbodyConstraints.FreezeAll;
        projectileRb.velocity = Vector3.zero;
    }

    private void Throw()
    {
        projectileRb.useGravity = true;
        projectileRb.constraints = RigidbodyConstraints.None;

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

        projectile = null;

        // implement throwCooldown
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }

    float stabMotion = 1;

    IEnumerator Stab()
    {
        projectileRb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX;
        stabMotion += 1;
        projectile.transform.localPosition = new Vector3(0f, 0f, stabMotion);
        yield return new WaitForSeconds(1f);
        
        stabMotion -= 1;
        projectile.transform.localPosition = new Vector3(0f, 0f, stabMotion);
        projectileRb.constraints = RigidbodyConstraints.FreezeAll;
    }

    Coroutine damageInProgress;

    IEnumerator TimeBetween(int damage)
    {
        while (TakeDamage == true)
        {
            int count = 0;
            count += damage;
            Debug.Log("Damage : " + count);
            CurrentHealth -= damage;
            Healthslider.value = CurrentHealth;
            HealthText.text = $"HP:{CurrentHealth}";
            yield return new WaitForSeconds(5f);
            Debug.Log("Next damage");
        }
    }

    public void TakeHealthDamage(int damage)
    {
        StartCoroutine(TimeBetween(damage));
        /*CurrentHealth -= damage;
        Healthslider.value = CurrentHealth;
        HealthText.text = $"HP:{CurrentHealth}";*/
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

    IEnumerator HealthAttackIncrease(int value)
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("Increase health");
        IncreaseHealth(value);
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

    void TakeStaminaDamage(int damage)
    {
        CurrentStamina -= damage;
        Staminaslider.value = CurrentStamina;
        StaminaText.text = $"Stamina:{CurrentStamina}";
    }

    public void SetStamina()
    {
        Staminaslider.value = CurrentStamina;
        StaminaText.text = $"Stamina:{CurrentStamina}";
    }

    public void SetMaxStamina()
    {
        Staminaslider.maxValue = MaxStamina;
        Staminaslider.value = CurrentStamina;
        StaminaText.text = $"Stamina:{CurrentStamina}";
    }

    public void IncreaseStamina(int value)
    {
        CurrentStamina += value;
        Staminaslider.value = CurrentStamina;
        StaminaText.text = $"Stamina:{CurrentStamina}";
    }

}

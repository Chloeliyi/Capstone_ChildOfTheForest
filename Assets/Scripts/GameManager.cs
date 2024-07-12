using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string username;
    public static GameManager Instance;

    /*public Camera MenuCam;
    public GameObject StartMenu;*/
    public Transform playerCamera;

    [SerializeField] private bool newGame;

    public int MaxHealth = 50;

    public int CurrentHealth;

    public int MaxFood = 30;

    public int CurrentFood;

    public int MaxWater = 30;

    public int CurrentWater;

    public int MaxSpearDurability = 100;
    public int MaxAxeDurability = 30;
    public int CurrentDurability;
    public int CurrentSpearDurability;

    //public TMP_Text HealthText;

    //public TMP_Text FoodText;

    //public TMP_Text StaminaText;

    public Slider Healthslider;

    public Slider Foodslider;

    public Slider Waterslider;

    public Slider Weapondurability;

    public Image WeaponIcon;

    public Sprite[] WeaponsSprite;

    public TMP_Text RepairCounter;

    public int Repaircount;

    //public GameObject SmallIvenMenu;

    public GameObject BigIvenMenu;

    public GameObject ItemDescMenu;

    public GameObject CraftMenu;

    public GameObject Axe;

    public Transform parent;

    public Quaternion rotation;

    public GameObject AxeGameObject;

    public GameObject Spear;

    public Transform attackPoint; 

    //public GameObject Tree;

    public GameObject WallObject;

    public GameObject Torch;

    public bool Nearwater;

    public bool activeAxe;

    public bool ActiveSpear;

    public Enemy enemy;

    public int TakeHealthDamage;

    public int TakeHealthIncrease;

    Coroutine damageInProgress;

    Coroutine increaseInProgress;

    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    void Start()
    {        
        CurrentHealth = MaxHealth;
        SetMaxHealth();
        //HealthText.text = $"HP:{CurrentHealth}";

        CurrentFood = MaxFood;
        Debug.Log("Start food is: " + MaxFood);
        SetMaxFood();
        //FoodText.text = $"Food:{CurrentFood}";

        CurrentWater = MaxWater;
        SetMaxWater();
        //StaminaText.text = $"Stamina:{CurrentStamina}";

        RepairCounter.text = $"{Repaircount}";

        BigIvenMenu.SetActive(false);
        Axe.SetActive(false);
        //projectile.SetActive(false);
    }

    public void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void TimeStop()
    {
        Time.timeScale = 0;
    }
    
    public void TimeStart()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        if (newGame)
        {
            Debug.Log("New Game");
        }
        if(Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Quit");
            Application.Quit();
        }

        if (CurrentFood <= 10 || CurrentWater <= 10)
        {
            if (CurrentHealth > 0)
            {
                TakeHealthDamage ++;
                if (damageInProgress == null)
                {
                    damageInProgress = StartCoroutine(TimeBetweenDamage(5));
                }
            }
        }

        if (CurrentHealth < MaxHealth)
        {
            if (CurrentFood >= 20 && CurrentWater >= 20)
            {
                TakeHealthIncrease ++;
                if (increaseInProgress == null)
                {
                    increaseInProgress = StartCoroutine(TimeBetweenIncrease(10));
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!BigIvenMenu.activeSelf)
            {
                Debug.Log("Open Inventory");
                //InventoryManager.Instance.ListItems();
                BigIvenMenu.SetActive(true);
            }
            else if (BigIvenMenu.activeSelf)
            {
                Debug.Log("Close Inventory");
                //InventoryManager.Instance.ClearContent();
                BigIvenMenu.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (AxeGameObject == null)
            {
                //SpawnAxe();
                Debug.Log("No axe");
            }
            else if (AxeGameObject != null)
            {
                if (AxeGameObject.activeSelf)
                {
                    AxeGameObject.SetActive(false);
                    activeAxe = false;
                    HideWeaponDurability();
                }
                else if (!AxeGameObject.activeSelf)
                {
                    Debug.Log("Active axe");
                    AxeGameObject.SetActive(true);
                    activeAxe = true;
                    OpenWeaponDurability();

                    if (projectile != null)
                    {
                        projectile.SetActive(false);
                        ActiveSpear = false;
                        HideWeaponDurability();
                    }
                    else if (torchObject != null)
                    {
                        torchObject.SetActive(false);
                    }
                    else if (Activebench != null)
                    {
                        Activebench.SetActive(false);
                    }
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (projectile == null)
            {
                //SpawnSpear();
                Debug.Log("No spear");
            }
            else if (projectile != null)
            {
                if (projectile.activeSelf)
                {
                    projectile.SetActive(false);
                    ActiveSpear = false;
                    HideWeaponDurability();
                }
                else if (!projectile.activeSelf)
                {
                    projectile.SetActive(true);
                    ActiveSpear = true;
                    OpenWeaponDurability();

                    if (AxeGameObject != null)
                    {
                        AxeGameObject.SetActive(false);
                        activeAxe = false;
                        HideWeaponDurability();
                    }
                    else if (torchObject != null)
                    {
                        torchObject.SetActive(false);
                    }
                    else if (Activebench != null)
                    {
                        Activebench.SetActive(false);
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (torchObject != null)
            {
                if (torchObject.activeSelf)
                {
                    torchObject.SetActive(false);
                    activeAxe = false;
                    ActiveSpear = false;
                    HideWeaponDurability();
                }
                else if (!torchObject.activeSelf)
                {
                    torchObject.SetActive(true);
                    activeAxe = false;
                    ActiveSpear = false;
                    HideWeaponDurability();
                    if (projectile != null)
                    {
                        projectile.SetActive(false);
                    }
                    else if (AxeGameObject != null)
                    {
                        AxeGameObject.SetActive(false);
                    }
                    else if (Activebench != null)
                    {
                        Activebench.SetActive(false);
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (projectile != null && projectile.activeSelf)
            {
                Throw();
                Debug.Log("Spear is thrown");
            }

            if (Nearwater)
            {
                Debug.Log("Drink water");
                IncreaseWater(2);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (projectile != null && projectile.activeSelf)
            {
                StartCoroutine(Stab());
                Debug.Log("Stab with spear");
            }
        }

        /*if (Input.GetKeyDown(KeyCode.L))
        {
            if (Wallpart == null)
            {
                SpawnWall();
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Wallpart != null)
            {
                PlaceWall();
            }
            else if (Activebench != null)
            {
                PlaceWorkbench();
            }
        }*/

        /*if (Input.GetKeyDown(KeyCode.Y))
        {
            if (Activebench == null)
            {
                SpawnWorkbench();
            }
        }*/

        if (CurrentHealth <= 0)
        {
            Healthslider.value = 0;
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
        if (Wallpart.activeSelf)
        {
            Wallpart.SetActive(false);
        }
        else if (projectile.activeSelf)
        {
            projectile.SetActive(false);
            ActiveSpear = false;
            HideWeaponDurability();
        }
        else if (AxeGameObject.activeSelf)
        {
            AxeGameObject.SetActive(false);
            activeAxe = false;
            HideWeaponDurability();
        }
        else if (torchObject.activeSelf)
        {
            torchObject.SetActive(false);
        }
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
        //Time.timeScale = 0f;
        BigIvenMenu.gameObject.SetActive(true);
        ItemDescMenu.gameObject.SetActive(false);
        CraftMenu.gameObject.SetActive(true);
    }

    public CraftItem[] craftSlot;

    public void CloseCraftMenu()
    {
        //Time.timeScale = 1f;
        BigIvenMenu.gameObject.SetActive(false);
        ItemDescMenu.gameObject.SetActive(true);
        CraftMenu.gameObject.SetActive(false);
    }

    GameObject Wallpart;

    public void SpawnWall()
    {
        Wallpart = Instantiate(WallObject, parent);
        if (Activebench != null)
        {
            Activebench.SetActive(false);
        }
        else if (projectile != null)
        {
            projectile.SetActive(false);
            ActiveSpear = false;
            HideWeaponDurability();
        }
        else if (AxeGameObject != null)
        {
            AxeGameObject.SetActive(false);
            activeAxe = false;
            HideWeaponDurability();
        }
        else if (torchObject != null)
        {
            torchObject.SetActive(false);
        }
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
        torchObject = Instantiate(Torch, playerCamera);
        torchObject.GetComponentInChildren<ParticleSystem>().Stop();
        if (Activebench != null)
        {
            Activebench.SetActive(false);
        }
        else if (Wallpart != null)
        {
            Wallpart.SetActive(false);
        }
        else if (projectile != null)
        {
            projectile.SetActive(false);
            ActiveSpear = false;
            HideWeaponDurability();
        }
        else if (AxeGameObject != null)
        {
            AxeGameObject.SetActive(false);
            activeAxe = false;
            HideWeaponDurability();
        }
    }

    public void LightTorch()
    {
        torchObject.GetComponentInChildren<ParticleSystem>().Play();
    }

    public void SpawnAxe(int durability)
    {
        //AxeGameObject = Instantiate(Axe, parent);
        AxeGameObject = Axe;
        AxeGameObject.SetActive(true);
        activeAxe = true;

        CurrentDurability = durability;
        OpenWeaponDurability();
        /*Weapondurability.maxValue = MaxAxeDurability;
        Weapondurability.value = CurrentDurability;
        WeaponIcon.sprite = WeaponsSprite[0];*/

        if (Activebench != null)
        {
            Activebench.SetActive(false);
        }
        else if (Wallpart != null)
        {
            Wallpart.SetActive(false);
        }
        else if (projectile != null)
        {
            projectile.SetActive(false);
            ActiveSpear = false;
            HideWeaponDurability();
        }
        else if (torchObject != null)
        {
            torchObject.SetActive(false);
        }
    }

    public void AxeDamage(int damage)
    {
        CurrentDurability -= damage;
        Weapondurability.value = CurrentDurability;
    }

    public void Repair()
    {
        Repaircount ++;
        RepairCounter.text = $"{Repaircount}";
    }

    public void HideWeaponDurability()
    {
        WeaponIcon.sprite = null;
        Weapondurability.maxValue = 1;
        Weapondurability.value = 1;
    }

    public void OpenWeaponDurability()
    {
        if (activeAxe == true)
        {
            WeaponIcon.sprite = WeaponsSprite[0];
            Weapondurability.maxValue = MaxAxeDurability;
            Weapondurability.value = CurrentDurability;
        }
        else if (ActiveSpear == true)
        {
            WeaponIcon.sprite = WeaponsSprite[1];
            Weapondurability.maxValue = MaxSpearDurability;
            Weapondurability.value = CurrentSpearDurability;
        }
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

    public Rigidbody projectileRb;

    public void SpawnSpear(int durability)
    {
        readyToThrow = false;
        projectile = Instantiate(Spear, playerCamera);
        projectileRb = projectile.GetComponent<Rigidbody>();
        projectileRb.constraints = RigidbodyConstraints.FreezeAll;
        projectileRb.velocity = Vector3.zero;

        ActiveSpear = true;

        CurrentSpearDurability = durability;
        OpenWeaponDurability();
        /*Weapondurability.maxValue = MaxSpearDurability;
        Weapondurability.value = CurrentSpearDurability;
        WeaponIcon.sprite = WeaponsSprite[1];*/

        if (Activebench != null)
        {
            Activebench.SetActive(false);
        }
        else if (Wallpart != null)
        {
            Wallpart.SetActive(false);
        }
        else if (AxeGameObject != null)
        {
            AxeGameObject.SetActive(false);
            activeAxe = false;
            HideWeaponDurability();
        }
        else if (torchObject != null)
        {
            torchObject.SetActive(false);
        }
    }

    public void SpearDamage(int damage)
    {
        CurrentSpearDurability -= damage;
        Weapondurability.value = CurrentSpearDurability;
    }

    public void Throw()
    {
        //projectileRb.useGravity = true;
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
        projectile.transform.localPosition = new Vector3(0.5f, 0.3f, stabMotion);
        yield return new WaitForSeconds(1f);
        
        stabMotion -= 1;
        projectile.transform.localPosition = new Vector3(0.5f, 0.3f, stabMotion);
        projectileRb.constraints = RigidbodyConstraints.FreezeAll;
    }

    IEnumerator TimeBetweenDamage(int time)
    {
        while (TakeHealthDamage > 0)
        {
            HealthDamage(2);
            yield return new WaitForSeconds(time);
        }
        damageInProgress = null;
    }

    public void HealthDamage(int damage)
    {  
        CurrentHealth -= damage;
        Healthslider.value = CurrentHealth;
        //HealthText.text = $"HP:{CurrentHealth}";
        Debug.Log("HP:" + CurrentHealth);  
    }

    public void SetHealth()
    {
        Healthslider.value = CurrentHealth;
        //HealthText.text = $"HP:{CurrentHealth}";
        Debug.Log("HP:" + CurrentHealth);
    }

    public void SetMaxHealth()
    {
        Healthslider.maxValue = MaxHealth;
        Healthslider.value = CurrentHealth;
        //HealthText.text = $"HP:{CurrentHealth}";
        Debug.Log("HP:" + CurrentHealth);
    }

    IEnumerator TimeBetweenIncrease(int time)
    {
        while (TakeHealthIncrease > 0)
        {
            IncreaseHealth(2);
            yield return new WaitForSeconds(time);
        }
        increaseInProgress = null;
    }

    public void IncreaseHealth(int value)
    {
        CurrentHealth += value;
        Healthslider.value = CurrentHealth;
        //HealthText.text = $"HP:{CurrentHealth}";
        Debug.Log("HP:" + CurrentHealth);
    }

    public void TakeFoodDamage(int damage)
    {
        CurrentFood -= damage;
        Foodslider.value = CurrentFood;
        //FoodText.text = $"Food:{CurrentFood}";
        Debug.Log("Food:" + CurrentFood);
    }

    public void SetFood()
    {
        Foodslider.value = CurrentFood;
        //FoodText.text = $"Food:{CurrentFood}";
        Debug.Log("Food:" + CurrentFood);
    }

    public void SetMaxFood()
    {
        Foodslider.maxValue = MaxFood;
        Foodslider.value = CurrentFood;
        //FoodText.text = $"Food:{CurrentFood}";
        Debug.Log("Food:" + CurrentFood);
    }

    //public InventoryItemController inventoryItemController;

    public void IncreaseFood(int value)
    {
        if (CurrentFood <= MaxFood)
        {
            CurrentFood += value;
            if (CurrentFood >= MaxFood)
            {
                CurrentFood = MaxFood;
                Foodslider.value = CurrentFood;
            }
            else
            {
                Foodslider.value = CurrentFood;
                //FoodText.text = $"Food:{CurrentFood}";
                Debug.Log("Food:" + CurrentFood);
            }
        }
    }

    public void TakeWaterDamage(int damage)
    {
        CurrentWater -= damage;
        Waterslider.value = CurrentWater;
        //StaminaText.text = $"Stamina:{CurrentStamina}";
    }

    public void SetWater()
    {
        Waterslider.value = CurrentWater;
        //StaminaText.text = $"Stamina:{CurrentStamina}";
    }

    public void SetMaxWater()
    {
        Waterslider.maxValue = MaxWater;
        Waterslider.value = CurrentWater;
        Debug.Log("Water:" + CurrentWater);
    }

    public void IncreaseWater(int value)
    {
        if (CurrentWater <= MaxWater)
        {
            CurrentWater += value;
            if (CurrentWater >= MaxWater)
            {
                CurrentWater = MaxWater;
                Waterslider.value = CurrentWater;
            }
            else
            {
                Waterslider.value = CurrentWater;
                //WaterText.text = $"Water:{CurrentWater}";
                Debug.Log("Water:" + CurrentWater);
            }
        }
    }
}

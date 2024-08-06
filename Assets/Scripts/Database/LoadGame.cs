using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class LoadGame : MonoBehaviour
{
    FirebaseAuth auth;
    DatabaseReference DatabaseRef;
    DatabaseReference dataRef;

    public GameObject playerSpawn;
    public GameObject player;
    public GameManager gameManager;
    public InventoryManager inventoryManager;

    public Sprite branchSprite;
    public Sprite crystalSprite;
    public Sprite berrySprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        DatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
        dataRef = DatabaseRef.Child("Users");  
        playerSpawn = GameObject.Find("playerSpawn");
        if (playerSpawn != null)
        {
            RetrieveSaveData();
            RetrieveInventoryData();
            RetrieveStatsData();
        }
    }

    public void RetrieveSaveData()
    {
        string userId = FindObjectOfType<GameManager>().username;
        dataRef.Child(userId).Child("Location").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Failed to retrieve save data: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                Debug.Log("Retrieved save data");
                DataSnapshot dataSnapshot = task.Result;
                //Debug.Log("DataSnapshot: " + dataSnapshot.GetRawJsonValue());
                Location location = JsonUtility.FromJson<Location>(dataSnapshot.GetRawJsonValue());
                Debug.Log("Location: " + location.x + ", " + location.y + ", " + location.z);

                playerSpawn.transform.position = new Vector3(location.x, location.y, location.z);
                Debug.Log("Player Spawn: " + playerSpawn.transform.position);
                player.GetComponent<FPSController>().SetPosition();
            }
        });
        return;
    }

    public void RetrieveInventoryData()
    {
        string userId = FindObjectOfType<GameManager>().username;
        dataRef.Child(userId).Child("Inventory").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Failed to retrieve inventory data: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                Debug.Log("Retrieved inventory data");
                DataSnapshot dataSnapshot = task.Result;
                Debug.Log("DataSnapshot: " + dataSnapshot.GetRawJsonValue());
                foreach (DataSnapshot d in dataSnapshot.Children)
                {
                    Inventory item = JsonUtility.FromJson<Inventory>(d.GetRawJsonValue());
                    Debug.Log("Item: " + item.itemName + ", " + item.quantity);
                    if (item.itemName == "Branch")
                    {
                        inventoryManager.AddItem(item.itemName, item.quantity, branchSprite, item.itemDescription);
                    }
                    else if (item.itemName == "Crystal")
                    {
                        inventoryManager.AddItem(item.itemName, item.quantity, crystalSprite, item.itemDescription);
                    }
                    else if (item.itemName == "Berry")
                    {
                        inventoryManager.AddItem(item.itemName, item.quantity, berrySprite, item.itemDescription);
                    }
                }
            }
        });
        return;
    }

    public void RetrieveStatsData()
    {
        string userId = FindObjectOfType<GameManager>().username;
        dataRef.Child(userId).Child("Stats").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Failed to retrieve stats data: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                Debug.Log("Retrieved stats data");
                DataSnapshot dataSnapshot = task.Result;
                Debug.Log("DataSnapshot: " + dataSnapshot.GetRawJsonValue());
                Stats stats = JsonUtility.FromJson<Stats>(dataSnapshot.GetRawJsonValue());
                Debug.Log("Stats: " + stats.health + ", " + stats.food + ", " + stats.water);
                gameManager.CurrentHealth = stats.health;
                gameManager.CurrentFood = stats.food;
                gameManager.CurrentWater = stats.water;
                gameManager.SetFood();
                gameManager.SetHealth();
                gameManager.SetWater();
            }
        });
        return;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
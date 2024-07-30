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

public class SaveData : MonoBehaviour
{
    FirebaseAuth auth;
    DatabaseReference DatabaseRef;
    DatabaseReference playerRef;
    DatabaseReference invRef;
    DatabaseReference statsRef;

    public GameObject player;
    public InventoryManager inventoryManager;
    public InventoryItemController[] invList;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player Animations");
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }

    void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        DatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
        playerRef = DatabaseRef.Child("Users");  
    }

    public void SaveLocationData() {
        // Get the player's position
        float x = player.transform.position.x;
        float y = player.transform.position.y;
        float z = player.transform.position.z;

        string userId = "testUser";

        // Save the player's position to the database
        DatabaseReference playerRef = DatabaseRef.Child("Users").Child(userId).Child("Location");
        playerRef.Child("x").SetValueAsync(x);
        playerRef.Child("y").SetValueAsync(y);
        playerRef.Child("z").SetValueAsync(z);
    }

    public void SaveInventoryData() {
        string userId = "testUser";
        invList = inventoryManager.itemSlot;

        DatabaseReference invRef = DatabaseRef.Child("Users").Child(userId).Child("Inventory");

        for (int i = 0; i < invList.Length; i++)
        {
            if (invList[i].itemName != "")
            {
                invRef.Child("item" + i).Child("itemName").SetValueAsync(invList[i].itemName);
                invRef.Child("item" + i).Child("quantity").SetValueAsync(invList[i].quantity);
                invRef.Child("item" + i).Child("itemDescription").SetValueAsync(invList[i].itemDescription);
            }
        }
    }

    public void SaveStatsData() {
        string userId = "testUser";
        int health = FindObjectOfType<GameManager>().CurrentHealth;
        int food = FindObjectOfType<GameManager>().CurrentFood;
        int water = FindObjectOfType<GameManager>().CurrentWater;

        DatabaseReference statsRef = DatabaseRef.Child("Users").Child(userId).Child("Stats");
        statsRef.Child("health").SetValueAsync(health);
        statsRef.Child("food").SetValueAsync(food);
        statsRef.Child("water").SetValueAsync(water);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Player position: " + player.transform.position.x + ", " + player.transform.position.y + ", " + player.transform.position.z);
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("Space key was pressed");
            SaveLocationData();
            SaveInventoryData();
        }*/
    }
}

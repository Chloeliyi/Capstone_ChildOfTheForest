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

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        DatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
        playerRef = DatabaseRef.Child("Users");  
    }

    public void SavePlayerData() {
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

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Player position: " + player.transform.position.x + ", " + player.transform.position.y + ", " + player.transform.position.z);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("Space key was pressed");
            SavePlayerData();
        }
    }
}

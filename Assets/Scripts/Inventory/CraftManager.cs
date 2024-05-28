using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CraftManager : MonoBehaviour
{
    public static CraftManager Instance;

    public GameObject[] CraftObjects;

    public List<string> craftItems = new List<string>();
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void Add(string craftItem)
    {
        craftItems.Add(craftItem);
    }
}

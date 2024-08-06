using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float minXPos;
    public float maxXPos;
    public float minZPos;
    public float maxZPos;
    private float xPos;
    private float zPos;
    private float spawnWait;
    public float minSpawnWait;
    public float maxSpawnWait;
    private bool stop = true;

    
    public GameObject[] animalPrefabs;
    private int randAnim;
    private int animCount;
    public int maxAnimCount;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        spawnWait = Random.Range(minSpawnWait, maxSpawnWait);
    }

    IEnumerator waitSpawn()
    {
        yield return new WaitForSeconds(2);
        
        while(stop)
        {
            xPos = Random.Range(minXPos, maxXPos);
            zPos = Random.Range(minZPos, maxZPos);
            float yPos = Terrain.activeTerrain.SampleHeight(new Vector3(xPos, 0, zPos));

            randAnim = Random.Range(0, animalPrefabs.Length);
            Vector3 spawnPos = new Vector3(xPos, yPos, zPos);

            if (animCount < maxAnimCount)
            {
                animCount++;
                GameObject newSpawn = Instantiate (animalPrefabs[randAnim], spawnPos, animalPrefabs[randAnim].transform.rotation);
                newSpawn.name = newSpawn.name.Replace("(Clone)", "");
            }
            else
            {
                stop = false;
            }            yield return new WaitForSeconds(spawnWait);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WendigoSpawner : MonoBehaviour
{
    public float minXPos;
    public float maxXPos;
    public float minZPos;
    public float maxZPos;
    private float xPos;
    private float zPos;
    private bool stop = true;
    public float wendigoSpawnCooldown;

    public GameObject wendigoPrefab;
    public GameObject wendigo;

    void Awake()
    {
        StartCoroutine(waitSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        if (wendigo == null)
        {
            stop = true;
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(wendigoSpawnCooldown);
        StartCoroutine(waitSpawn());
        StopCoroutine(Cooldown());
    }

    IEnumerator waitSpawn()
    {
        yield return new WaitForSeconds(1);
        
        while(stop & wendigo == null)
        {
            xPos = Random.Range(minXPos, maxXPos);
            zPos = Random.Range(minZPos, maxZPos);
            float yPos = Terrain.activeTerrain.SampleHeight(new Vector3(xPos, 0, zPos));

            Vector3 spawnPos = new Vector3(xPos, yPos, zPos);

            wendigo = Instantiate (wendigoPrefab, spawnPos, wendigoPrefab.transform.rotation);
            yield return new WaitForSeconds(2);
        }
        StopCoroutine(waitSpawn());
    }
}

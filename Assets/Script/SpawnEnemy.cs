using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    GameObject[] spawnEnemies;
    public GameObject zombie;
    public float minSpawnTime;
    public float maxSpawnTime;
    float lastSpawnTime = 0;
    float spawnTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        minSpawnTime = 0.5f;
        maxSpawnTime = 1.5f;
        UpdateSpawnTime();
        spawnEnemies = GameObject.FindGameObjectsWithTag("Respawn");
    }


    // Update is called once per frame
    void Update()
    {
        if (Time.time >= lastSpawnTime + spawnTime)
        {
            Spawn();
        }
    }

    void UpdateSpawnTime()
    {
        lastSpawnTime = Time.time;
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    void Spawn()
    {
        int point = Random.Range(0, spawnEnemies.Length);
        if (spawnEnemies == null)
        {
            return;
        };
        Instantiate(zombie, spawnEnemies[point].transform.position, Quaternion.identity);
        UpdateSpawnTime();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawnController : MonoBehaviour
{
    public GameObject[] enemies;
    public float spawnTime = 0.5f;
    private float spawnTimer = 0f;

    void Start()
    {

    }

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnTime)
        {
            int randomIndex = Random.Range(0, enemies.Length);
            GameObject enemy = enemies[randomIndex];
            if (enemy != null)
            {
                Instantiate(enemy, transform.position, Quaternion.identity);
            }
            spawnTimer = 0f;
        }
    }
}

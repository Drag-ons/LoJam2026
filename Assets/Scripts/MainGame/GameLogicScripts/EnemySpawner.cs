using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float minimumSpawnRange;
    public float maximumSpawnRange;
    public float spawnDistance;
    public float spawnCooldownTime;
    public float spawnRateCooldownTime;
    public float spawnRateModifier;
    public List<GameObject> enemies;

    private GameObject player;
    private bool canSpawn = true;
    private bool canSpawnRate = true;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (canSpawn)
        {
            canSpawn = false;

            SpawnRandomEnemy();

            StartCoroutine(SpawnCooldownRoutine());
        }

        if (canSpawnRate)
        {
            canSpawnRate = false;

            UpSpawnRate();

            StartCoroutine(SpawnRateCooldownRoutine());
        }
    }

    IEnumerator SpawnCooldownRoutine()
    {
        yield return new WaitForSeconds(spawnCooldownTime);
        canSpawn = true;
    }

    IEnumerator SpawnRateCooldownRoutine()
    {
        yield return new WaitForSeconds(spawnRateCooldownTime);
        canSpawnRate = true;
    }

    public void SpawnRandomEnemy()
    {
        Vector3 spawnLocation = new Vector3(
            Random.Range(player.transform.position.x - minimumSpawnRange, player.transform.position.x + maximumSpawnRange), 
            Random.Range(player.transform.position.y - minimumSpawnRange, player.transform.position.y + maximumSpawnRange), 
            0);

        if (Vector3.Distance(player.transform.position, spawnLocation) > spawnDistance)
        {
            Instantiate(enemies[Random.Range(0, enemies.Count)], spawnLocation, Quaternion.identity, gameObject.transform);
        }
        else
        {
            SpawnRandomEnemy();
        }
    }

    public void UpSpawnRate()
    {
        spawnCooldownTime = spawnCooldownTime * spawnRateModifier;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnCooldownTime;
    public float spawnRateCooldownTime;
    public float spawnRateModifier;
    public List<EnemySpawnData> enemies;

    private GameObject player;
    private bool canSpawn = true;
    private bool canSpawnRate = true;

    [System.Serializable]
    public class EnemySpawnData
    {
        public GameObject prefab;
        public float minimumSpawnRangeX;
        public float maximumSpawnRangeX;
        public float minimumSpawnRangeY;
        public float maximumSpawnRangeY;
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (canSpawn)
        {
            canSpawn = false;

            Spawn(enemies[Random.Range(0, enemies.Count)]);

            StartCoroutine(SpawnCooldownRoutine());
        }

        if (canSpawnRate)
        {
            canSpawnRate = false;

            UpSpawnRate();

            StartCoroutine(SpawnRateCooldownRoutine());
        }
    }

    private void Spawn(EnemySpawnData spawnData)
    {
        Vector2 playerPosition = player.gameObject.transform.position;

        float randomX = Random.Range(playerPosition.x + spawnData.minimumSpawnRangeX, playerPosition.x + spawnData.maximumSpawnRangeX);
        float randomY = Random.Range(playerPosition.y + spawnData.minimumSpawnRangeY, playerPosition.y + spawnData.maximumSpawnRangeY);

        Vector2 differenceVector = playerPosition - new Vector2(randomX, randomY);
        float absoluteXDistance = Mathf.Abs(differenceVector.x);
        float absoluteYDistance = Mathf.Abs(differenceVector.y);
        
        if (Random.value < 0.5f)
        {
            randomX -= absoluteXDistance * 2;
        }

        if (Random.value < 0.5f)
        {
            randomY -= absoluteYDistance * 2;
        }

        Instantiate(spawnData.prefab, new Vector2(randomX, randomY), Quaternion.identity, gameObject.transform);
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

    public void UpSpawnRate()
    {
        spawnCooldownTime = spawnCooldownTime * spawnRateModifier;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public bool canSpawn = true;
    public int spawnCap;
    public float spawnCooldownTime;
    public float spawnRateCooldownTime;
    public float spawnRateModifier;
    public float cullRange;
    public float cullRangeCooldownTime;
    public List<EnemySpawnData> enemies;
    public List<GameObject> spawnedEnemies = new();

    private GameObject player;
    private PlayerMovement playerMovement;
    private PlayerResourceController playerResource;
    private bool canSpawnRate = true;
    private bool canCull = true;

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
        playerMovement = player.GetComponent<PlayerMovement>();
        playerResource = player.GetComponent<PlayerResourceController>();
    }

    void Update()
    {
        if (canSpawn && spawnedEnemies.Count < spawnCap
            && !playerResource.isDead && !playerMovement.nukeCooldown)
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

        if (canCull)
        {
            canCull = false;

            CullEnemiesCheck();

            StartCoroutine(CullCooldownRoutine());
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

        spawnedEnemies.Add(Instantiate(spawnData.prefab, new Vector2(randomX, randomY), Quaternion.identity, gameObject.transform));
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

    IEnumerator CullCooldownRoutine()
    {
        yield return new WaitForSeconds(cullRangeCooldownTime);
        canCull = true;
    }

    private void CullEnemiesCheck()
    {
        List<GameObject> enemiesToDespawn = new List<GameObject>();
        foreach (GameObject enemy in spawnedEnemies)
        {
            if (Vector2.Distance(player.transform.position, enemy.transform.position) > cullRange)
            {
                enemiesToDespawn.Add(enemy);
            }
        }

        foreach (GameObject enemy in enemiesToDespawn)
        {
            spawnedEnemies.Remove(enemy);
            Destroy(enemy);
        }
    }

    public void RemoveEnemyFromSpawnedList(GameObject enemy)
    {
        spawnedEnemies.Remove(enemy);
    }

    public void UpSpawnRate()
    {
        spawnCooldownTime = spawnCooldownTime * spawnRateModifier;
    }

    public void NukeAllEnemies()
    {
        List<GameObject> enemiesToDespawn = new List<GameObject>();
        foreach (GameObject enemy in spawnedEnemies)
        {
            enemiesToDespawn.Add(enemy);
        }

        foreach (GameObject enemy in enemiesToDespawn)
        {
            spawnedEnemies.Remove(enemy);
            Destroy(enemy);
        }
    }
}

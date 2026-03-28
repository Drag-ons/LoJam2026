using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public bool canSpawn = true;
    public int spawnCap;
    public int wave = 0;
    public float timeBetweenWave;
    public GameObject snakePrefab;
    public EnemyStats snakeStats;
    public GameObject deerPrefab;
    public EnemyStats deerStats;
    public float cullRange;
    public float cullRangeCooldownTime;
    public List<GameObject> spawnedEnemies = new();
    public List<WaveEvents> waveEvents = new();

    [System.Serializable]
    public class WaveEvents
    {
        public int waveTrigger;
        public int spawnSnakeAmount;
        public int spawnDeerAmount;
    }

    private GameObject player;
    private PlayerMovement playerMovement;
    private PlayerResourceController playerResource;
    private bool canCull = true;

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
            foreach (WaveEvents waveEvent in waveEvents)
            {
                if (waveEvent.waveTrigger <= wave)
                {
                    for (int i = 0; i < waveEvent.spawnSnakeAmount; i++)
                    {
                        Spawn(snakePrefab, snakeStats);
                    }

                    for (int i = 0; i < waveEvent.spawnDeerAmount; i++)
                    {
                        Spawn(deerPrefab, deerStats);
                    }
                }
            }
            StartCoroutine(WaveCooldown());
        }

        if (canCull)
        {
            canCull = false;
            CullEnemies();
            StartCoroutine(CullCooldownRoutine());
        }
    }

    private void Spawn(GameObject enemyPrefab, EnemyStats enemyStats)
    {
        Vector2 playerPosition = player.gameObject.transform.position;

        float randomX = Random.Range(playerPosition.x + enemyStats.minimumSpawnRangeX, playerPosition.x + enemyStats.maximumSpawnRangeX);
        float randomY = Random.Range(playerPosition.y + enemyStats.minimumSpawnRangeY, playerPosition.y + enemyStats.maximumSpawnRangeY);

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

        spawnedEnemies.Add(Instantiate(enemyPrefab, new Vector2(randomX, randomY), Quaternion.identity, gameObject.transform));
    }

    IEnumerator WaveCooldown()
    {
        yield return new WaitForSeconds(timeBetweenWave);
        canSpawn = true;
        wave++;
    }

    IEnumerator CullCooldownRoutine()
    {
        yield return new WaitForSeconds(cullRangeCooldownTime);
        canCull = true;
    }

    private void CullEnemies()
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

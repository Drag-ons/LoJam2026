using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnr;
    public float spawndis;
    public float cooldownTime;
    public List<GameObject> enemies;

    private GameObject player;
    private bool canSpawn = true;

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

            StartCoroutine(CooldownRoutine());
        }
    }

    IEnumerator CooldownRoutine()
    {
        yield return new WaitForSeconds(cooldownTime);
        canSpawn = true;
    }

    public void SpawnRandomEnemy()
    {
        Vector3 spawnloc = new Vector3(Random.Range(player.transform.position.x - spawnr, player.transform.position.x + spawnr), Random.Range(player.transform.position.y - spawnr, player.transform.position.y + spawnr), 0);
        if (Vector3.Distance(player.transform.position, spawnloc) > spawndis)
        {
            Instantiate(enemies[Random.Range(0, enemies.Count)], spawnloc, Quaternion.identity, gameObject.transform);
        }
        else
        {
            SpawnRandomEnemy();
        }
    }

    public void UpDifficulty()
    {
        cooldownTime = cooldownTime * 0.9f;
    }
}

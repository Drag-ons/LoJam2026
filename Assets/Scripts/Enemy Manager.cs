
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float timer;
    public float cooldown;
    public GameObject Player;


    public float spawnr;
    public float spawndis;
    public GameObject[] enemies;
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }
    void FixedUpdate()
    {
        if (timer > cooldown)
        {
            timer = 0;
            SpawnRandomEnemy();
        }
        timer++;
    }
    public void SpawnRandomEnemy()
    {
        Vector3 spawnloc = new Vector3(Random.Range(Player.transform.position.x - spawnr, Player.transform.position.x + spawnr), Random.Range(Player.transform.position.y - spawnr, Player.transform.position.y + spawnr), 0);
        if (Vector3.Distance(Player.transform.position, spawnloc) > spawndis)
        {
            Instantiate(enemies[Random.Range(0, enemies.Length)], spawnloc, Quaternion.identity);
        }
        else
        {
            SpawnRandomEnemy();
        }

    }
    public void difficultyup()
    {
        cooldown = cooldown * 0.9f;
    }
}

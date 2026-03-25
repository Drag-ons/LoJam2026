using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject currentChunk;
    public List<GameObject> activeChunks;
    public List<GameObject> prefabChunks;
    public float despawnRange;
    public float cooldownTime;

    private GameObject player;
    private bool canCheck = true;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        SpawnNewTerrainChunk(new Vector3(0, 0, 1));
        SpawnChunkCheck();
    }

    void Update()
    {
        if (canCheck)
        {
            canCheck = false;

            DespawnChunkCheck();
            SpawnChunkCheck();

            StartCoroutine(CooldownRoutine());
        }
    }

    IEnumerator CooldownRoutine()
    {
        yield return new WaitForSeconds(cooldownTime);
        canCheck = true;
    }

    private void DespawnChunkCheck()
    {
        List<GameObject> chunksToDestroy = new List<GameObject>();
        foreach (GameObject chunk in activeChunks)
        {
            if (Vector2.Distance(player.transform.position, chunk.transform.position) > despawnRange)
            {
                chunksToDestroy.Add(chunk);
            }
        }

        foreach (GameObject chunk in chunksToDestroy)
        {
            activeChunks.Remove(chunk);
            Destroy(chunk);
        }
    }

    private void SpawnChunkCheck()
    {
        if (currentChunk == null)
        {
            return;
        }

        float spawnCheckRadius = 0.1f;
        if (!Physics2D.OverlapCircle(currentChunk.transform.Find("EastSpawn").position, spawnCheckRadius))
        {
            SpawnNewTerrainChunk(currentChunk.transform.Find("EastSpawn").position);
        }
        
        if (!Physics2D.OverlapCircle(currentChunk.transform.Find("WestSpawn").position, spawnCheckRadius))
        {
            SpawnNewTerrainChunk(currentChunk.transform.Find("WestSpawn").position);
        }
        
        if (!Physics2D.OverlapCircle(currentChunk.transform.Find("NorthSpawn").position, spawnCheckRadius))
        {
            SpawnNewTerrainChunk(currentChunk.transform.Find("NorthSpawn").position);
        }
        
        if (!Physics2D.OverlapCircle(currentChunk.transform.Find("SouthSpawn").position, spawnCheckRadius))
        {
            SpawnNewTerrainChunk(currentChunk.transform.Find("SouthSpawn").position);
        }
        
        if (!Physics2D.OverlapCircle(currentChunk.transform.Find("NorthEastSpawn").position, spawnCheckRadius))
        {
            SpawnNewTerrainChunk(currentChunk.transform.Find("NorthEastSpawn").position);
        }
        
        if (!Physics2D.OverlapCircle(currentChunk.transform.Find("NorthWestSpawn").position, spawnCheckRadius))
        {
            SpawnNewTerrainChunk(currentChunk.transform.Find("NorthWestSpawn").position);
        }
        
        if (!Physics2D.OverlapCircle(currentChunk.transform.Find("SouthEastSpawn").position, spawnCheckRadius))
        {
            SpawnNewTerrainChunk(currentChunk.transform.Find("SouthEastSpawn").position);
        }
        
        if (!Physics2D.OverlapCircle(currentChunk.transform.Find("SouthWestSpawn").position, spawnCheckRadius))
        {
            SpawnNewTerrainChunk(currentChunk.transform.Find("SouthWestSpawn").position);
        }
    }

    private void SpawnNewTerrainChunk(Vector3 newTerrainChunkPos)
    {
        activeChunks.Add(Instantiate(prefabChunks[Random.Range(0, prefabChunks.Count)], newTerrainChunkPos, Quaternion.identity, gameObject.transform));
    }
}

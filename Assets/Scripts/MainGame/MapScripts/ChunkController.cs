using System.Collections.Generic;
using UnityEngine;

public class ChunkController : MonoBehaviour
{
    public List<GameObject> obstacleSpawns;
    public List<GameObject> obstacles;

    private MapController mapController;

    void Start()
    {
        mapController = FindAnyObjectByType<MapController>();

        foreach (GameObject spawn in obstacleSpawns)
        {
            Instantiate(obstacles[Random.Range(0, obstacles.Count)], spawn.transform.position, Quaternion.identity, spawn.transform);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            mapController.currentChunk = gameObject.transform.parent.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            mapController.currentChunk = null;
        }
    }
}

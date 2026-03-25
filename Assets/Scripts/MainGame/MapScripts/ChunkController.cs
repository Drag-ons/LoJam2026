using UnityEngine;

public class ChunkController : MonoBehaviour
{
    private MapController mapController;

    void Start()
    {
        mapController = FindAnyObjectByType<MapController>();
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

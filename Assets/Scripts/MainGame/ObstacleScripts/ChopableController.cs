using System.Collections.Generic;
using UnityEngine;

public class ChopableController : MonoBehaviour
{
    public List<GameObject> spawnables;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Chop()
    {
        Instantiate(spawnables[Random.Range(0, spawnables.Count)], transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

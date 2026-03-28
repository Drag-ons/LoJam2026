using System.Collections.Generic;
using UnityEngine;

public class ChopableController : MonoBehaviour
{
    public List<GameObject> spawnables;
    public Slashaim slashaim;

    void Start()
    {
         slashaim = GameObject.FindWithTag("Slash").GetComponent<Slashaim>();
    }

    

    public void Chop()
    {
        slashaim.Slash(this.transform.position);
        Instantiate(spawnables[Random.Range(0, spawnables.Count)], transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

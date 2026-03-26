using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class FilterControl : MonoBehaviour
{
    public Material filter;
    private bool active;
    public float nukelength;
    public EnemySpawner spawner;
    public GameObject player;
    public void Nuke(InputAction.CallbackContext context)
    {
        ;
        active = true;
        StartCoroutine(NukeDuration());


    }
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        filter.SetFloat("_Intensity", 0);
        filter.SetFloat("_Active", 5);
    }
    void FixedUpdate()
    {
        if (active == true)
        {
            spawner.canSpawn = false;
            //filter.SetFloat("_Active", 0.5f)
            if (filter.GetFloat("_Intensity") <= 3)
            {
                filter.SetFloat("_Intensity", filter.GetFloat("_Intensity") + 0.1f);
                filter.SetFloat("_Active", filter.GetFloat("_Active") - .13f);
            }

        }
        else
        {

            if (filter.GetFloat("_Intensity") >= 0)
            {
                filter.SetFloat("_Intensity", filter.GetFloat("_Intensity") - .1f);
                filter.SetFloat("_Active", filter.GetFloat("_Active") + .13f);
            }
            else
            {
                //filter.SetFloat("_Active", 20f);
            }
        }
    }
    IEnumerator NukeDuration()
    {
        while (active == true)
        {
            yield return new WaitForSeconds(nukelength / 2);
            foreach (GameObject enemy in spawner.spawnedEnemies)
            {
                enemy.transform.position = player.transform.position + new Vector3(100, 100, 100);
            }
            yield return new WaitForSeconds(nukelength / 2);
            spawner.canSpawn = true;

            active = false;
        }
    }

}

using System.Collections;
using UnityEngine;

public class FilterControl : MonoBehaviour
{
    public Material filter;
    public Material damage;
    public bool active;
    public bool death;
    public float nukelength;
    public EnemySpawner spawner;
    public GameObject player;

    public void Nuke()
    {
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
        
        if(death == true)
        {
            if(filter.GetFloat("_Intensity") <= 5)
            {
                damage.SetFloat("_DMG",0);
                filter.SetFloat("_Intensity", Mathf.Clamp(filter.GetFloat("_Intensity") + 0.1f, 0f, 10f));
                filter.SetFloat("_Active", Mathf.Clamp(filter.GetFloat("_Active") - .13f, 0f, 10f));
            }
        }
        else
        {
            if (active == true)
        {
            //filter.SetFloat("_Active", 0.5f)
            if (filter.GetFloat("_Intensity") <= 3)
            {
                damage.SetFloat("_DMG",0);
                filter.SetFloat("_Intensity", Mathf.Clamp(filter.GetFloat("_Intensity") + 0.1f, 0f, 10f));
                filter.SetFloat("_Active", Mathf.Clamp(filter.GetFloat("_Active") - .13f, 0f, 10f));
            }

        }
        else
        {

            if (filter.GetFloat("_Intensity") >= 0)
            {
                filter.SetFloat("_Intensity", Mathf.Clamp(filter.GetFloat("_Intensity") - .1f, 0f, 10f));
                filter.SetFloat("_Active", Mathf.Clamp(filter.GetFloat("_Active") + .13f,0f,10f));
            }
            else
            {
                //filter.SetFloat("_Active", 20f);
            }
        }
        }

    }

    IEnumerator NukeDuration()
    {
        while (active == true)
        {
            yield return new WaitForSeconds(nukelength / 2);
            spawner.NukeAllEnemies();
            yield return new WaitForSeconds(nukelength / 2);
            active = false;
        }
    }
    
}

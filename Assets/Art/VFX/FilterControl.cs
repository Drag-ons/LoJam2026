using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class FilterControl : MonoBehaviour
{
    public Material filter;
    private bool active;
    public float nukelength;

    public void Nuke(InputAction.CallbackContext context)
    {
        ;
        active = true;
        StartCoroutine(NukeDuration());


    }
    void Start()
    {
        filter.SetFloat("_Intensity", 0);
        filter.SetFloat("_Active", 5);
    }
    void FixedUpdate()
    {
        if (active == true)
        {
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
            yield return new WaitForSeconds(nukelength);
            active = false;
        }
    }

}

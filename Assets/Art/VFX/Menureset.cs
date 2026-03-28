using UnityEngine;

public class Menureset : MonoBehaviour
{
    public Material filter;
    public Material Damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        filter.SetFloat("_Intensity", 0);
        filter.SetFloat("_Active", 5);
        Damage.SetFloat("_DMG", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

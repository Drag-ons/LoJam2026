using UnityEngine;
using UnityEngine.VFX;
public class Dashani : MonoBehaviour
{
    public Transform player;
    public VisualEffect ve;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        ve = this.GetComponent<VisualEffect>();
    }
    void Update()
    {
        ve.SetVector3("Position", player.position);
    }

    // Update is called once per frame
    public void Dash()
    {
        
        ve.SetBool("Dash", true);
    }
    public void Undash()
    {
        ve.SetBool("Dash", false);
    }
}

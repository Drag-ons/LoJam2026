using UnityEngine;
using UnityEngine.VFX;
public class Slashaim : MonoBehaviour
{
    private Transform player;
    private VisualEffect ve;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = player = FindAnyObjectByType<PlayerMovement>().transform;
        ve = this.GetComponent<VisualEffect>();
    }

    // Update is called once per frame
    public void Slash(Vector3 pos)
    {
        this.transform.position = pos;
        ve.SendEvent("Slash");
    }
}

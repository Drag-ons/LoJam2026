using UnityEngine;
using UnityEngine.VFX;

public class Deathaim : MonoBehaviour
{
    private Transform player;
    private VisualEffect ve;
    void Start()
    {
        player = player = FindAnyObjectByType<PlayerMovement>().transform;
        ve = this.GetComponent<VisualEffect>();
    }

    public void deathevent(Vector3 pos)
    {
        ve.SetVector3("Playerpos", player.position);
        ve.SetVector3("Unitpos", pos);
        ve.SendEvent("Death");

    }
}

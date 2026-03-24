using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int heal;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<HPManager>().Damage(heal);
            Destroy(this);
        }
    }
}

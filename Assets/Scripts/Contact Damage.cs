using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    public float damage;
    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("enter");
        if (other.CompareTag("Player"))
        {
            other.GetComponent<HPManager>().Damage(damage);
        }
    }
}

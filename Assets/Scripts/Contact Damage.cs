using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    public int damage;
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("enter");
        if (other.CompareTag("Player"))
        {
            other.GetComponent<HPManager>().Damage(damage);
        }
    }
}

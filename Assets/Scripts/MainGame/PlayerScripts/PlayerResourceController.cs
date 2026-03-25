using UnityEngine;

public class PlayerResourceController : MonoBehaviour
{
    public float sanity;
    public float stamina;
    public PlayerStats playerStats;

    public GameObject deathmenu;

    void Start()
    {
        sanity = playerStats.maxSanity;
        stamina = playerStats.maxStamina;
    }

    public void TakeDamage(float dmg)
    {
        sanity = Mathf.Clamp(sanity - dmg, 0, playerStats.maxSanity);

        if (sanity <= 0)
        {
            Time.timeScale = 0;
            deathmenu.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            TakeDamage(collision.GetComponent<EnemyMovement>().enemyStats.damage);
        }

        if (collision.CompareTag("Health") && sanity < playerStats.maxSanity)
        {
            TakeDamage(-10);
            Destroy(collision.gameObject);
        }
    }

    void Update()
    {
        
    }
}

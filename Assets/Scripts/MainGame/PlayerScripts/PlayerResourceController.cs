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

    public void RemoveSanity(float dmg)
    {
        sanity = Mathf.Clamp(sanity - dmg, 0, playerStats.maxSanity);

        if (sanity <= 0)
        {
            Time.timeScale = 0;
            deathmenu.SetActive(true);
        }
    }

    public void AddSanity(float dmg)
    {
        sanity = Mathf.Clamp(sanity + dmg, 0, playerStats.maxSanity);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            RemoveSanity(collision.GetComponent<EnemyMovement>().enemyStats.damage);
        }

        if (collision.gameObject.TryGetComponent(out ICollectable collectable))
        {
            collectable.Collect(this);
        }
    }

    void Update()
    {
        
    }
}

using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public EnemyStats enemyStats;
    public float lastXVelocity;
    public float distanceGoal;

    private Camera playerCamera;
    private float distanceFromPlayer;
    private Transform player;
    private Rigidbody2D rigidBody;
    private bool spottedByPlayer = false;

    void Start()
    {
        playerCamera = Camera.main;
        rigidBody = GetComponent<Rigidbody2D>();
        player = FindAnyObjectByType<PlayerMovement>().transform;
    }

    private void Update()
    {
        Vector3 viewPos = playerCamera.WorldToViewportPoint(transform.position);
        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        {
            spottedByPlayer = true;
        }
        else if (spottedByPlayer)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer > distanceGoal)
        {
            rigidBody.linearVelocity = (player.position - transform.position).normalized * Random.Range(enemyStats.minimumSpeed, enemyStats.maximumSpeed);
        } 

        if (rigidBody.linearVelocity.x != 0)
        {
            lastXVelocity = rigidBody.linearVelocity.x;
        }
    }
}

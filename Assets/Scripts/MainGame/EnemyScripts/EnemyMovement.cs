using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public EnemyStats enemyStats;
    public float lastXVelocity;
    public float distanceGoal;
    public bool canMove = false;

    private Camera playerCamera;
    private float distanceFromPlayer;
    private Transform player;
    private Rigidbody2D rigidBody;
    private bool spottedByPlayer = false;
    public Deathaim deathvfx;

    void Start()
    {
        playerCamera = Camera.main;
        rigidBody = GetComponent<Rigidbody2D>();
        player = FindAnyObjectByType<PlayerMovement>().transform;
        deathvfx = GameObject.FindWithTag("Deathvfx").GetComponent<Deathaim>();
    }

    void Update()
    {
        Vector3 viewPos = playerCamera.WorldToViewportPoint(transform.position);
        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0 && canMove)
        {
            spottedByPlayer = true;
        }
        else if (spottedByPlayer)
        {
            deathvfx.deathevent(this.transform.position);
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (canMove)
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
}

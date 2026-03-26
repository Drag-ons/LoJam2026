using UnityEngine;

public class EnemyMovement : MonoBehaviour, IEnemy
{
    public EnemyStats enemyStats;
    public float lastXVelocity;
    public bool canMove = false;
    public bool canDamage = false;
    public float distanceFromPlayer;
    public GameObject player;
    public Rigidbody2D rigidBody;

    private Camera playerCamera;
    private bool spottedByPlayer = false;
    public Deathaim deathvfx;
    public IEnemyMovement enemyMovementInterface;

    private void Start()
    {
        playerCamera = Camera.main;
        rigidBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        enemyMovementInterface = gameObject.GetComponent<IEnemyMovement>();
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
        enemyMovementInterface.Move();

        if (rigidBody.linearVelocity.x != 0)
        {
            lastXVelocity = rigidBody.linearVelocity.x;
        }
    }

    public void DamagePlayer(PlayerResourceController playerResourceController)
    {
        if (canDamage)
        {
            playerResourceController.RemoveSanity(enemyStats.damage);
        }
    }
}

using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IEnemy
{
    public PlayerStats playerStats;
    public EnemyStats enemyStats;
    public float lastXVelocity;
    public bool canMove = false;
    public bool isFleeing = false;
    public bool canDamage = false;
    public bool beingPushed = false;
    public bool finishedSpawning = false;
    public float distanceFromPlayer;
    public GameObject player;
    public Rigidbody2D rigidBody;
    public Deathaim deathvfx;
    public IEnemyMovement enemyMovementInterface;
    public EnemySpawner enemySpawner;

    private Camera playerCamera;
    private bool spottedByPlayer = false;
    private PlayerResourceController resourceController;
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerCamera = Camera.main;
        rigidBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        resourceController = player.GetComponent<PlayerResourceController>();
        playerMovement = player.GetComponent<PlayerMovement>();
        enemyMovementInterface = gameObject.GetComponent<IEnemyMovement>();
        deathvfx = GameObject.FindWithTag("Deathvfx").GetComponent<Deathaim>();
        enemySpawner = GameObject.FindWithTag("EnemySpawner").GetComponent<EnemySpawner>();
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
            resourceController.AddAbilityResource(enemyStats.abilityGain);
            deathvfx.deathevent(this.transform.position);
            enemySpawner.RemoveEnemyFromSpawnedList(gameObject);
            Destroy(gameObject);
        }

        if (playerMovement.isNuking)
        {
            isFleeing = true;
        }
        else
        {
            isFleeing = false;
        }
    }

    void FixedUpdate()
    {
        enemyMovementInterface.Move();

        if (rigidBody.linearVelocity.x != 0)
        {
            lastXVelocity = rigidBody.linearVelocity.x;
        }

        if (beingPushed && finishedSpawning)
        {
            rigidBody.AddForce((transform.position - player.transform.position).normalized * (playerStats.pushingPower / enemyStats.weight));
        }
    }

    public void DamagePlayer(PlayerResourceController playerResourceController)
    {
        if (canDamage)
        {
            playerResourceController.RemoveSanity(enemyStats.damage);
        }
    }

    public void Push()
    {
        StartCoroutine(PushMove());
    }

    private IEnumerator PushMove()
    {
        beingPushed = true;
        yield return new WaitForSeconds(playerStats.pushingTime);
        beingPushed = false;
    }
}

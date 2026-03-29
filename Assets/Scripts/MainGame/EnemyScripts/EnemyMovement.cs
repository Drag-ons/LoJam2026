using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IEnemy
{
    public PlayerStats playerStats;
    public EnemyStats enemyStats;
    public float lastXVelocity;
    public float lastYVelocity;
    public bool canMove = false;
    public bool isFleeing = false;
    public bool canDamage = false;
    public bool beingPushed = false;
    public bool finishedSpawning = false;
    public bool spottedByPlayer = false;
    public bool verticalMovement;
    public bool isVisible;
    public float distanceFromPlayer;
    public float spottedDelay;
    public GameObject player;
    public Rigidbody2D rigidBody;
    public Deathaim deathvfx;
    public IEnemyMovement enemyMovementInterface;
    public EnemySpawner enemySpawner;

    private Camera mainCamera;
    private PlayerResourceController resourceController;
    private PlayerMovement playerMovement;

    private void Start()
    {
        mainCamera = Camera.main;
        rigidBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        resourceController = player.GetComponent<PlayerResourceController>();
        playerMovement = player.GetComponent<PlayerMovement>();
        enemyMovementInterface = gameObject.GetComponent<IEnemyMovement>();
        deathvfx = GameObject.FindWithTag("Deathvfx").GetComponent<Deathaim>();
        enemySpawner = GameObject.FindWithTag("EnemySpawner").GetComponent<EnemySpawner>();
    }

    void FixedUpdate()
    {
        Vector3 viewPos = mainCamera.WorldToViewportPoint(transform.position);
        isVisible = viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1;
        if (isVisible && canMove)
        {
            StartCoroutine(SpottedDelay());
        }
        else if (spottedByPlayer && !isVisible)
        {
            resourceController.AddAbilityResource(enemyStats.abilityGain);
            deathvfx.deathevent(this.transform.position);
            enemySpawner.RemoveEnemyFromSpawnedList(gameObject);
            AudioManager.Instance.AnimalDeath();
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

        enemyMovementInterface.Move();

        if (rigidBody.linearVelocity.x != 0)
        {
            lastXVelocity = rigidBody.linearVelocity.x;
        }

        if (rigidBody.linearVelocity.y != 0)
        {
            lastYVelocity = rigidBody.linearVelocity.y;
        }

        if (beingPushed && finishedSpawning)
        {
            rigidBody.AddForce((transform.position - player.transform.position).normalized * (playerStats.pushingPower / enemyStats.weight));
        }
    }

    private IEnumerator SpottedDelay()
    {
        yield return new WaitForSeconds(spottedDelay);
        spottedByPlayer = true;
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

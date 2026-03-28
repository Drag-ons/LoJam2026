using UnityEngine;

public class SnakeMovement : MonoBehaviour, IEnemyMovement
{
    public float distanceGoal;

    private EnemyMovement enemyMovement;

    private void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }

    public void Move()
    {
        if (enemyMovement.canMove)
        {
            Vector3 playerPosition = enemyMovement.player.gameObject.transform.position;
            enemyMovement.distanceFromPlayer = Vector2.Distance(playerPosition, transform.position);
            if (enemyMovement.isFleeing)
            {
                enemyMovement.rigidBody.linearVelocity = (transform.position - playerPosition).normalized * Random.Range(enemyMovement.enemyStats.minimumSpeed, enemyMovement.enemyStats.maximumSpeed);
            }
            else
            {
                if (enemyMovement.distanceFromPlayer > distanceGoal)
                {
                    enemyMovement.rigidBody.linearVelocity = (playerPosition - transform.position).normalized * Random.Range(enemyMovement.enemyStats.minimumSpeed, enemyMovement.enemyStats.maximumSpeed);
                }
            }
        }
    }
}

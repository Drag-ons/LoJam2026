using UnityEngine;

public class DeerMovement : MonoBehaviour, IEnemyMovement
{
    private EnemyMovement enemyMovement;

    private void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }

    public void Move()
    {
        if (enemyMovement.canMove && enemyMovement.rigidBody.linearVelocity.x == 0)
        {
            if (enemyMovement.player.transform.InverseTransformPoint(gameObject.transform.position).x < 0)
            {
                enemyMovement.rigidBody.linearVelocity = new Vector2(Random.Range(enemyMovement.enemyStats.minimumSpeed, enemyMovement.enemyStats.maximumSpeed), 0);
            }
            else
            {
                enemyMovement.rigidBody.linearVelocity = new Vector2(Random.Range(-enemyMovement.enemyStats.minimumSpeed, -enemyMovement.enemyStats.maximumSpeed), 0);
            }
        }
    }
}

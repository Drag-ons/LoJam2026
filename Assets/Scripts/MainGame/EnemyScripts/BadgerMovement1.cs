using UnityEngine;

public class BadgerMovement1 : MonoBehaviour, IEnemyMovement
{
    private EnemyMovement enemyMovement;

    private void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }

    public void Move()
    {
        if (enemyMovement.isFleeing)
        {
            Vector3 playerPosition = enemyMovement.player.gameObject.transform.position;
            enemyMovement.rigidBody.linearVelocity = (transform.position - playerPosition).normalized * Random.Range(enemyMovement.enemyStats.minimumSpeed, enemyMovement.enemyStats.maximumSpeed);
        }
        else
        {
            if (enemyMovement.canMove && enemyMovement.rigidBody.linearVelocity.y == 0)
            {
                if (enemyMovement.player.transform.InverseTransformPoint(gameObject.transform.position).y < 0)
                {
                    enemyMovement.rigidBody.linearVelocity = new Vector2(0, Random.Range(enemyMovement.enemyStats.minimumSpeed, enemyMovement.enemyStats.maximumSpeed));
                }
                else
                {
                    enemyMovement.rigidBody.linearVelocity = new Vector2(0, Random.Range(-enemyMovement.enemyStats.minimumSpeed, -enemyMovement.enemyStats.maximumSpeed));
                }
            }
        }
    }
}

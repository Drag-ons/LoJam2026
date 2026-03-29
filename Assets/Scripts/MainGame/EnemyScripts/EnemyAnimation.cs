using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    GameObject player;
    EnemyMovement enemyMovement;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        enemyMovement = GetComponent<EnemyMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (enemyMovement.verticalMovement)
        {
            spriteRenderer.flipY = player.transform.InverseTransformPoint(gameObject.transform.position).y < 0;
        }
        else
        {
            spriteRenderer.flipX = player.transform.InverseTransformPoint(gameObject.transform.position).x < 0;
        }
    }

    public void OnSpawnEnd()
    {
        enemyMovement.canMove = true;
        enemyMovement.canDamage = true;
        enemyMovement.finishedSpawning = true;
    }

    public void flipAssetX(bool flipX)
    {
        spriteRenderer.flipX = flipX;
    }

    void Update()
    {
        if (enemyMovement.canMove)
        {
            if (enemyMovement.verticalMovement)
            {
                spriteRenderer.flipY = enemyMovement.lastYVelocity > 0;
            }
            else
            {
                spriteRenderer.flipX = enemyMovement.lastXVelocity > 0;
            }
        }
    }
}

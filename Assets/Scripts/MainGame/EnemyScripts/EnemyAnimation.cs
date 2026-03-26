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
        spriteRenderer.flipX = player.transform.InverseTransformPoint(gameObject.transform.position).x < 0;
    }

    public void OnSpawnEnd()
    {
        enemyMovement.canMove = true;
        enemyMovement.canDamage = true;
    }

    void Update()
    {
        if (enemyMovement.canMove)
        {
            spriteRenderer.flipX = enemyMovement.lastXVelocity > 0;
        }
    }
}

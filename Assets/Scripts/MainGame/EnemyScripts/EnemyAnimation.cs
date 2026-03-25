using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    Animator animator;
    EnemyMovement enemyMovement;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        spriteRenderer.flipX = enemyMovement.lastXVelocity > 0;
    }
}

using System.Collections;
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
        StartCoroutine(WaitForAnimationAndMove());
    }

    private IEnumerator WaitForAnimationAndMove()
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && !animator.IsInTransition(0));
        enemyMovement.canMove = true;
    }

    void Update()
    {
        spriteRenderer.flipX = enemyMovement.lastXVelocity > 0;
    }
}

using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    PlayerMovement playerMovement;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        animator.SetBool("Nuking", playerMovement.isNuking);
        animator.SetBool("Dashing", playerMovement.isDashing);
        if (playerMovement.isDashing)
        {
            animator.SetBool("Moving", false);
        }
        else
        {
            animator.SetBool("Moving", playerMovement.isMoving);
        }
        spriteRenderer.flipX = playerMovement.lastXVelocity < 0;
    }
}

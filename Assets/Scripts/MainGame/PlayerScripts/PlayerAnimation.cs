using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;
    PlayerMovement playerMovement;
    PlayerResourceController playerResource;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
        playerResource = GetComponent<PlayerResourceController>();
    }

    private void Update()
    {
        animator.SetBool("Pushing", playerMovement.isPushing);
        animator.SetBool("Dying", playerResource.isDead);
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

    public void OnPushEnd()
    {
        playerMovement.isPushing = false;
        playerMovement.canMove = true;
        playerResource.canBeDamaged = true;
        playerResource.canGainAbility = true;
    }
}

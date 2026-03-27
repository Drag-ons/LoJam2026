using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float lastXVelocity;
    public float xVelocity;
    public float yVelocity;
    public bool canMove = true;
    public bool isMoving = false;
    public bool canDash = true;
    public bool isDashing = false;
    public bool isNuking = false;
    public float shakeAmount;
    public float shakeSpeed;
    public bool isShaking = false;
    public PlayerStats playerStats;

    private Rigidbody2D rigidBody;
    private float activeMoveSpeed;
    private PlayerResourceController resourceController;
    private FilterControl filterControl;
    public Dashani dash;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        resourceController = GetComponent<PlayerResourceController>();
        activeMoveSpeed = playerStats.movementSpeed;
        filterControl = Camera.main.GetComponent<FilterControl>();
    }

    private void FixedUpdate()
    {
        isMoving = xVelocity != 0 || yVelocity != 0;

        if (xVelocity != 0)
        {
            lastXVelocity = xVelocity;
        }

        rigidBody.linearVelocity = new Vector2(xVelocity * activeMoveSpeed, yVelocity * activeMoveSpeed);

        if (isShaking)
        {
            float shakeX = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;
            transform.localPosition = new Vector2(gameObject.transform.position.x + shakeX, gameObject.transform.position.y);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (canMove)
        {
            xVelocity = context.ReadValue<Vector2>().x;
            yVelocity = context.ReadValue<Vector2>().y;
        }
        else
        {
            xVelocity = 0;
            yVelocity = 0;
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (isMoving && canDash && resourceController.stamina > playerStats.dashingCost)
        {
            resourceController.RemoveStamina(playerStats.dashingCost);
            StartCoroutine(DashMove());
        }
    }

    private IEnumerator DashMove()
    {
        canDash = false;
        isDashing = true;
        dash.Dash();
        activeMoveSpeed = playerStats.dashingPower;
        yield return new WaitForSeconds(playerStats.dashingTime);
        isDashing = false;
        activeMoveSpeed = playerStats.movementSpeed;
        dash.Undash();
        yield return new WaitForSeconds(playerStats.dashingCooldown);
        canDash = true;
    }

    public void Nuke(InputAction.CallbackContext context)
    {
        if (resourceController.CanUseAbility(playerStats.nukeOrbCost))
        {
            resourceController.RemoveAbilityOrbs(playerStats.nukeOrbCost);
            StartCoroutine(NukeAction());
        }
    }

    private IEnumerator NukeAction()
    {
        resourceController.canGainAbility = false;
        resourceController.canBeDamaged = false;
        isShaking = true;
        isNuking = true;
        canMove = false;
        xVelocity = 0;
        yVelocity = 0;
        filterControl.Nuke();
        yield return new WaitUntil(() => filterControl.active == false);
        canMove = true;
        isNuking = false;
        isShaking = false;
        resourceController.canBeDamaged = true;
        resourceController.canGainAbility = true;
    }
}

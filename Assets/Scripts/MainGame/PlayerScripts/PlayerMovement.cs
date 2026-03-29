using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;
public class PlayerMovement : MonoBehaviour
{
    public float lastXVelocity;
    public Vector2 lastLinearVelocity;
    public float xVelocity;
    public float yVelocity;
    public bool canMove = true;
    public bool isMoving = false;
    public bool canDash = true;
    public bool isDashing = false;
    public bool isPushing = false;
    public bool isNuking = false;
    public bool nukeCooldown = false;
    public float shakeAmount;
    public float shakeSpeed;
    public bool isShaking = false;
    public PlayerStats playerStats;
    //public AudioManager manager;

    private Rigidbody2D rigidBody;
    private float activeMoveSpeed;
    private PlayerResourceController resourceController;
    private FilterControl filterControl;
    public Dashani dash;
    public GameObject blast;

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

        if (rigidBody.linearVelocity.x != 0 || rigidBody.linearVelocity.y != 0)
        {
            lastLinearVelocity = rigidBody.linearVelocity.normalized;
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

    private Vector2 GetLastPlayerDirection()
    {
        if (Mathf.Abs(lastLinearVelocity.x) > Mathf.Abs(lastLinearVelocity.y))
        {
            if (lastLinearVelocity.x > 0)
            {
                blast.transform.localRotation = Quaternion.Euler(0,0,90);
                return transform.right;
            }
            else
            {
                blast.transform.localRotation = Quaternion.Euler(0,0,270);
                return -transform.right;
            }
        }
        else
        {
            if (lastLinearVelocity.y > 0)
            {
                blast.transform.localRotation = Quaternion.Euler(0,0,180);
                return transform.up;
            }
            else
            {
                blast.transform.localRotation = Quaternion.Euler(0,0,0);
                return -transform.up;
            }
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (isMoving && canDash && resourceController.stamina > playerStats.dashingCost)
        {
            AudioManager.Instance.Play(AudioManager.SoundType.Dash);
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

    public void Chop(InputAction.CallbackContext context)
    {
        if (resourceController.CanUseAbility(playerStats.chopOrbCost))
        {
            if (ChopAction())
            {
                resourceController.RemoveAbilityOrbs(playerStats.chopOrbCost);
            }
        }
    }

    public bool ChopAction()
    {
        ChopableController chopableController = null;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, playerStats.chopRange);

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.gameObject.TryGetComponent(out chopableController))
            {
                chopableController.Chop();
                 AudioManager.Instance.Play(AudioManager.SoundType.Axe);
                return true;
            }
        }

        return false;
    }

    public void Push(InputAction.CallbackContext context)
    {
        if (resourceController.CanUseAbility(playerStats.pushingOrbCost))
        {
            resourceController.RemoveAbilityOrbs(playerStats.pushingOrbCost);
            isPushing = true;
            resourceController.canGainAbility = false;
            resourceController.canBeDamaged = false;
            canMove = false;
            xVelocity = 0;
            yVelocity = 0;

            List<IEnemy> enemiesInCone = new List<IEnemy>();
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, playerStats.pushingRange);
            GetLastPlayerDirection();
            blast.GetComponent<VisualEffect>().SendEvent("Blast");
            foreach (Collider2D collider in hitColliders)
             AudioManager.Instance.Play(AudioManager.SoundType.Shockwave);
            {
                if (GetComponent<Collider>().gameObject.TryGetComponent(out IEnemy enemy))
                {
                    Vector2 directionToTarget = (GetComponent<Collider>().gameObject.transform.position - transform.position).normalized;
                    float angle = Vector2.Angle(GetLastPlayerDirection(), directionToTarget);
                    if (angle < playerStats.pushingAngle / 2f)
                    {
                        enemiesInCone.Add(enemy);
                    }
                }
            }

            if (enemiesInCone.Count > 0)
            {
                foreach (IEnemy enemy in enemiesInCone)
                {
                    enemy.Push();
                }
            }
        }
    }

    public void Nuke(InputAction.CallbackContext context)
    {
        if (resourceController.CanUseAbility(playerStats.nukeOrbCost))
        {
             AudioManager.Instance.Play(AudioManager.SoundType.Nuke);
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
        nukeCooldown = true;
        canMove = false;
        xVelocity = 0;
        yVelocity = 0;
        filterControl.Nuke();
        yield return new WaitUntil(() => filterControl.active == false);
        isNuking = false;
        canMove = true;
        isShaking = false;
        resourceController.canBeDamaged = true;
        resourceController.canGainAbility = true;
        yield return new WaitForSeconds(playerStats.nukeDowntime);
        nukeCooldown = false;
    }
}

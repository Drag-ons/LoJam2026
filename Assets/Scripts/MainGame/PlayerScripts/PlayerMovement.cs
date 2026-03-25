using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    public bool isMoving = false;
    public float lastXVelocity;
    public float xVelocity;
    public float yVelocity;

    private Rigidbody2D rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        isMoving = xVelocity != 0 || yVelocity != 0;

        if (xVelocity != 0)
        {
            lastXVelocity = xVelocity;
        }

        rigidBody.linearVelocity = new Vector2(xVelocity * speed, yVelocity * speed);
    }

    public void Move(InputAction.CallbackContext context)
    {
        xVelocity = context.ReadValue<Vector2>().x;
        yVelocity = context.ReadValue<Vector2>().y;
    }
}

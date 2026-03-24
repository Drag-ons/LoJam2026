using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 10;
    public Rigidbody2D rb;

    private float horizontal;
    private float vertical;
    
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, vertical * speed);
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
    }
}

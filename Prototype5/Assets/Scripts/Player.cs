using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public InputAction playerMovement;
    public InputAction playerJump;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public PlayerGroundCollision playerGroundCollision;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement.Enable();
        playerJump.Enable();
    }

    private void Update()
    {
        moveInput = playerMovement.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();
        
    }

    private void Move()
    {
        Vector2 velocity = moveInput * moveSpeed;

        rb.velocity = new Vector2(velocity.x, rb.velocity.y);

        if (moveInput.y > 0 && playerGroundCollision.GetIsGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        else
        {
            rb.velocity -= new Vector2(0, 1f);
        }
    }

    private void OnDisable()
    {
        playerJump.Disable();
        playerMovement.Disable();
    }

    
}

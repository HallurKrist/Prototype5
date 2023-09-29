using UnityEngine;
using UnityEngine.InputSystem;
using MushroomGame;
public class Player : Agent
{
    public InputAction playerMovement;
    public InputAction playerAttack;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public PlayerGroundCollision playerGroundCollision;
    

    private Rigidbody2D rb;
    private Vector2 moveInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement.Enable();
        playerAttack.Enable();
    }

    private void Update()
    {
        moveInput = playerMovement.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();
        Attack();
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

    private void Attack() {
        if (playerAttack.triggered) {
            Debug.Log("Attack");
        }
    }

    private void OnDisable()
    {
        playerMovement.Disable();
        playerAttack.Disable();
    }
}

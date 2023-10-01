using UnityEngine;
using UnityEngine.InputSystem;
using MushroomGame;
using System.Collections;


public class Enemy : Agent
{
    public InputAction playerMovement;
    public InputAction playerAttack;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public EnemyGroundCollision playerGroundCollision;
    private Animator animator;
    private Transform playerTransform;
    [SerializeField]
    private bool isFacingLeft = true;
    public float attackDelayInSeconds = 1.5f;
    public float durationOfAttackAnimationInSeconds = 2f;
    private bool isAttacking = false;
    [SerializeField]
    GameObject attackHitbox;
    [SerializeField]
    public float delayBetweenAttacks = 8f;
    
    private bool attackWaiting = false;
    public   bool isDead = false;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement.Enable();
        playerAttack.Enable();
        animator = GetComponent<Animator>();
        playerTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (GetHealth() <= 0)
        {
            isDead = true;
        }

        if (isDead && !animator.GetBool("dead"))
        {
            PlayDeathAnimation();
        }
        else
        {
            StartCoroutine(AttackDelay());
        }
    }

    private void PlayDeathAnimation()
    {
        animator.SetBool("dead", true);
    }


    private void FixedUpdate()
    {
        if (!isDead)
            Move();
    }

    private void Move()
    {

        //It's a mushroom does it need to move?


        /* Vector2 velocity = moveInput * moveSpeed;

        if (moveInput.x != 0)
        {
            isFacingRight = moveInput.x > 0;
        } */

        if (isFacingLeft)
        {
            playerTransform.localScale = new Vector3(Mathf.Abs(playerTransform.localScale.x), playerTransform.localScale.y, playerTransform.localScale.z);
        }
        else
        {
            playerTransform.localScale = new Vector3(-Mathf.Abs(playerTransform.localScale.x), playerTransform.localScale.y, playerTransform.localScale.z);
        }

        /* rb.velocity = new Vector2(velocity.x, rb.velocity.y);

        if (moveInput.y > 0 && playerGroundCollision.GetIsGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        else
        {
            rb.velocity -= new Vector2(0, 1f);

        } */

        // animator.SetFloat("velocityX", Mathf.Abs(velocity.x));
    }

    

    private void Attack()
    {
        /* if (playerAttack.triggered && !animator.GetBool("isAttacking"))
        {
            isAttacking = true;
            StartCoroutine(AnimationAttackDelay());
        } */



        isAttacking = true;
        StartCoroutine(AnimationAttackDelay());
        animator.SetBool("isAttacking", isAttacking);
        Debug.Log("Animator: isAttacking == true");
        if (attackHitbox != null)
        {
            attackHitbox.SetActive(animator.GetBool("isAttacking"));
        }
    }

    IEnumerator AttackDelay()
    {
        if (!attackWaiting) 
        {
            attackWaiting = true;
            yield return new WaitForSeconds(delayBetweenAttacks);
            attackWaiting = false;
            Attack();
        }
    }

    IEnumerator AnimationAttackDelay()
    {
        yield return new WaitForSeconds(durationOfAttackAnimationInSeconds);
        isAttacking = false;
        animator.SetBool("isAttacking", isAttacking);
        Debug.Log("Animator: isAttacking == false");
    }

    /* private void OnDisable()
    {
        playerMovement.Disable();
        playerAttack.Disable();
    } */

    /* void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            TakeDamage(collision.gameObject.GetComponent<Agent>().GetDamage());
        }
    }   

    override public void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (GetHealth() <= 0)
        {
            isDead = true;
        }
    } */
}


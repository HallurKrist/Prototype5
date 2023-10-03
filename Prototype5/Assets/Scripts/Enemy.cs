using UnityEngine;
using UnityEngine.InputSystem;
using MushroomGame;
using System.Collections;
using UnityEngine.SceneManagement;


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
    private float timeRemaining;
    private bool timerIsRunning = true;
    public float durationOfFadingAnimationInSeconds = 1f;
    public float durationOfDeathAnimationInSeconds = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement.Enable();
        playerAttack.Enable();
        animator = GetComponent<Animator>();
        playerTransform = GetComponent<Transform>();

        timeRemaining = 60;
    }

    private void Update()
    {
        Fade();
        if (GetHealth() <= 0)
        {
            isDead = true;
        }

        if (isDead && !animator.GetBool("isDead"))
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
        animator.SetBool("isDead", true);
        Debug.Log(animator.GetBool("isDead"));
        Debug.Log("gonna call animation delay");
        StartCoroutine(AnimationDeathDelay());
    }


    private void FixedUpdate()
    {
        if (!isDead)
            Move();
    }

    private void Fade()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                StartCoroutine(AnimationFadingDelay());
                animator.SetBool("isFading", true);
            }
        }
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


        if (isAttacking) 
        {
            StartCoroutine(AnimationAttackDelay());
            animator.SetBool("isAttacking", isAttacking);
            if (attackHitbox != null)
            {
                attackHitbox.SetActive(animator.GetBool("isAttacking"));
            }
        }
    }

    IEnumerator AttackDelay()
    {
        if (!attackWaiting) 
        {
            isAttacking = true;
            attackWaiting = true;
            yield return new WaitForSeconds(delayBetweenAttacks);
            attackWaiting = false;
            Attack();
        }
    }

    IEnumerator AnimationDeathDelay()
    {
        Debug.Log("in animation delay");
        yield return new WaitForSeconds(durationOfDeathAnimationInSeconds);
        Debug.Log("done waiting");
        animator.SetBool("isDead", false);
        Debug.Log("Change to endScreen");
        timerIsRunning = false;
        SceneManager.LoadScene (sceneName:"EndScreen");
    }

    IEnumerator AnimationAttackDelay()
    {
        yield return new WaitForSeconds(durationOfFadingAnimationInSeconds);
        isAttacking = false;
        animator.SetBool("isAttacking", isAttacking);
    }

    IEnumerator AnimationFadingDelay()
    {
        yield return new WaitForSeconds(durationOfAttackAnimationInSeconds);
        SceneManager.LoadScene (sceneName:"StartScreen");
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
    }  */  

    override public void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (GetHealth() <= 0)
        {
            isDead = true;
            isAttacking = false;
        }
    }
}


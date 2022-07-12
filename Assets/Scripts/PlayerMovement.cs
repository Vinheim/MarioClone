using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Physics and movement variables
    public float speed;
    public float runSpeed = 6f;
    float horizontalMove = 0f;
    public float jumpForce;
    Rigidbody2D rb;
    bool facingRight = true;

    // Animation transitions and particle effects for movement
    public Animator animator;
    private SpriteRenderer sr;
    public ParticleSystem dust;

    // Advanced platformer jump movement variables
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    // Check if player is grounded
    bool isGrounded = false;
    public Transform groundCheckCollider;
    public float checkGroundRadius;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public LayerMask hiddenLayer;

    // Responsive grounding timer
    public float groundedTimer;
    public TimerCountdown timer;

    // Jump sound
    public AudioSource jumpSource;

    // Start is called before the first frame update
    void Start()
    {
        jumpSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>() as Rigidbody2D;
        sr = GetComponent<SpriteRenderer>();
        timer = FindObjectOfType<TimerCountdown>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        GroundCheck();

        // Check for pause functionality
        timer.PauseTimer();
    }

    // Handle horizontal player movement on x-axis
    void Move()
    {
        // Read input movement controls and calculate speed in left-right direction
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        // Flip sprite along x axis accordingly with key presses of Left/Right arrow keys or A/D
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && facingRight)
        {
            sr.flipX = true;
            facingRight = false;
            CreateDust();
        }
        
        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && !facingRight)
        {
            sr.flipX = false;
            facingRight = true;
            CreateDust();
        }
        
        // Instantiate new velocity vector with new horizontal position
        rb.velocity = new Vector2(horizontalMove, rb.velocity.y);
        

        // Adjust Speed transition variable for animation (Idle -> Run)  
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }

    /**
     * Implement jump logic with space bar press if player is grounded on terrain
     * Handle platformer jump function for accurate parabolic falling pattern and speeds
     * Transition idle <-> jump animations based on grounded status with animator SetBool function
     */
    void Jump()
    {
        // Create new rigid body velocity vector with calculated jump force
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpSource.Play();
            animator.SetBool("IsJumping", true);
            CreateDust();
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
            animator.SetBool("IsJumping", false);
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
            animator.SetBool("IsJumping", false);
        }
    }

    /**
     * Check if player is grounded on a platform
     * Allow the jump to execute if the player sprite is currently on the ground, pipe, or hidden block
     */
    void GroundCheck()
    {
        // Initialize colliders to represent the transforms the groundCheckCollider is overlapping
        
        Collider2D collider = Physics2D.OverlapCircle(groundCheckCollider.position, checkGroundRadius, groundLayer);
        Collider2D pipeCollider = Physics2D.OverlapCircle(groundCheckCollider.position, checkGroundRadius, wallLayer);
        Collider2D hiddenCollider = Physics2D.OverlapCircle(groundCheckCollider.position, checkGroundRadius, hiddenLayer);

        // If not null, then the player is grounded (making contact with at least one floor or a platform)
        if (collider != null || pipeCollider != null || hiddenCollider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void CreateDust()
    {
        dust.Play();
    }

    // Collect coins as player from blocks or in the air
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Block Coin"))
        {
            Destroy(other.gameObject);
            ScoreManager.scoreValue += 500;
        }
        else if (other.gameObject.CompareTag("Flying Coin"))
        {
            Destroy(other.gameObject);
            ScoreManager.scoreValue += 100;
        }
        else if(other.gameObject.name == "ULTRA RARE INFINITY COIN")
        {
            Destroy(other.gameObject);
            ScoreManager.scoreValue += 5000;
        }    
    
    }
}


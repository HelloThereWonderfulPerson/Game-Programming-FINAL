using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigidBody;
    
    [SerializeField] private float speed;
    [SerializeField] private Vector2 movement;
    [SerializeField] private float jumpForce;
    [SerializeField] private float doubleJumpForce;
    [SerializeField] private bool canDoubleJump;

    [SerializeField] private Vector2 wallJumpForce = new Vector2(4f, 8f);

    [SerializeField] private float wallJumpMovementCooldown;
    [SerializeField] public float wallJumpCooldown { get; set;}

    [SerializeField] private Vector2 screenBounds;
    [SerializeField] private float playerHalfWidth;
    [SerializeField] private float playerHalfHeight;

    [SerializeField] private float playerLastPosition;

    [SerializeField] private bool isGrounded;

    audioManager AudioManager;

    private void Awake()
    {
        AudioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audioManager>();
    }

    void Start()
    {
        //ScreenToViewportPoint -> Get the width & height of the screen
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)); //to prevent Bob from going off stage cause he aint gonna stop dancing. Poor Bob. Gonna die from OD. So sad. Didn't even get a chance. It's ok Bob. The audience loves you Bob. 
        playerHalfWidth = GetComponent<SpriteRenderer>().bounds.extents.x; // jarak tengah sprite ke ujung kiri (-) or kanan (+)
        playerHalfHeight = GetComponent<SpriteRenderer>().bounds.extents.y; // jarak tengah sprite ke ujung bawah (-) or atas (+)
    }

    void Update()
    {

        //"jump" if press space (cause default unity)
        if (Input.GetButtonDown("Jump"))
        {
            CheckJumpType();
        }

        //character move only when NOT pushing the wall
        if (!IsPushingAgainstWall())
        {
            CharacterMovement();
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        //flip sprite
        FlipCharacter();

        if (GetIsGrounded())
        {
            wallJumpCooldown = 0f;
        }

        if (wallJumpCooldown > 0f)
        {
            wallJumpCooldown -= Time.deltaTime; //reduce wall jump cooldown
        }

        Debug.DrawRay(transform.position, Vector2.left * (playerHalfWidth),
        IsPushingAgainstWall() ? Color.green : Color.red);
        Debug.DrawRay(transform.position, Vector2.right * (playerHalfWidth),
            IsPushingAgainstWall() ? Color.green : Color.red);
        Debug.DrawRay(transform.position, Vector2.down * (playerHalfHeight),
            GetIsGrounded() ? Color.green : Color.red);
    }

    private bool IsPushingAgainstWall()
    {
        float input = Input.GetAxis("Horizontal");
        bool pushingLeft = input < 0 && Physics2D.Raycast(transform.position, Vector2.left,
                          playerHalfWidth, LayerMask.GetMask("Ground"));
        bool pushingRight = input > 0 && Physics2D.Raycast(transform.position, Vector2.right,
                          playerHalfWidth, LayerMask.GetMask("Ground"));

        return pushingLeft || pushingRight;
    }

    private void FlipCharacter()
    {
        float input = Input.GetAxis("Horizontal");
        if (input > 0 && (transform.position.x > playerLastPosition)) //if player moving right
        {
            spriteRenderer.flipX = false;
        }
        else if (input < 0 && transform.position.x < playerLastPosition) //if player moving left
        {
            spriteRenderer.flipX = true;
        }
        playerLastPosition = transform.position.x;
    }

    private void OnScreen()
    {
        float clampedX = Mathf.Clamp(transform.position.x, -screenBounds.x + playerHalfWidth, screenBounds.x - playerHalfWidth); //keep the half body on screen
        Vector2 currPosition = transform.position;
        currPosition.x = clampedX;
        transform.position = currPosition;
    }
    
    private void CharacterMovement() //input if character move left and right
    {
        if (wallJumpCooldown > 0f)
        {
            return;
        }

        float input = Input.GetAxis("Horizontal"); //input left-right arrow or a-d
        movement.x = input * speed * Time.deltaTime; //calculate how far the object moves
        transform.Translate(movement); //actually moving the object

        //animation
        if (input != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void CheckJumpType()
    {
        bool isGrounded = GetIsGrounded();

        if (isGrounded)
        {
            Jump(jumpForce);
        }
        else
        {
            //wall jump or double jump
            int direction = GetWallJumpDirection();
            if (direction == 0 && canDoubleJump && rigidBody.velocity.y <= 0.1f) 
            {
                //no wall & double jump
                DoubleJump();
            }
            else if (direction != 0)
            {
                //wall jump yes
                WallJump(direction);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        GetIsGrounded();
    }

    private int GetWallJumpDirection()
    {
        if (Physics2D.Raycast(transform.position, Vector2.right, playerHalfWidth + 0.1f, LayerMask.GetMask("Ground")))
        {
            return -1;
        }
        if (Physics2D.Raycast(transform.position, Vector2.left, playerHalfWidth + 0.1f, LayerMask.GetMask("Ground")))
        {
            return 1;
        }
        return 0;
    }

    private bool GetIsGrounded() //check if player touch grass
    {
        bool hit = Physics2D.Raycast(transform.position, Vector2.down, playerHalfHeight + 0.1f, LayerMask.GetMask("Ground"));
        if (hit)
        {
            canDoubleJump = true;
        }
        return hit;
    }

    private void DoubleJump()
    {
        //double jump
        rigidBody.velocity = Vector2.zero; //reset kecepatan to 0 biar double jump konsisten
        rigidBody.angularVelocity = 0; //reset kecepatan to 0 biar double jump konsisten
        Jump(doubleJumpForce);
        canDoubleJump = false;
    }

    private void WallJump(int direction)
    {
        AudioManager.PlaySFX(AudioManager.wallJump);

        Vector2 force = wallJumpForce;
        force.x *= direction;
        rigidBody.velocity = Vector2.zero; 
        rigidBody.angularVelocity = 0;
        wallJumpCooldown = wallJumpMovementCooldown;
        rigidBody.AddForce(force, ForceMode2D.Impulse);
    }

    private void Jump(float force)
    {
        AudioManager.PlaySFX(AudioManager.jump);
        rigidBody.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }
}

using UnityEngine;
using System.Collections;


public class Character : MonoBehaviour
{
    // Public variables can be accessed through the gui , while private ones can't
    // But "SerializeFiel" let you acess your private vars from the gui
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D box;

    private bool isFacingright = true;
    private bool isRunning;
    private float horizontalInput;

    [Header("Ground")]
    [SerializeField] private bool isGround;
    [SerializeField] private LayerMask groundLayer;

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool canJump;

    [Header("WallSliding")]
    [SerializeField] private float wallSlidingSpeed;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] public Transform wallCheck;
    [SerializeField] Vector2 wallCheckSize;
    [SerializeField] private bool isTouchingWall;
    [SerializeField] private bool isWallSliding;

    [Header("WallJumping")]
    [SerializeField] private Vector2 wallJumpingAngle;
    [SerializeField] private float wallJumpingDirection;
    [SerializeField] private float wallJumpingForce;

    [Header("Dashing")]
    [SerializeField] private bool canDash = true;
    [SerializeField] private bool isDashing = false;
    [SerializeField] private float DashingPower = 24f;
    [SerializeField] private float DashingTime = 1f;
    [SerializeField] private float DashingCoolDown = 1f;
    [SerializeField] private TrailRenderer tr;

    // Start method is called once before the first frame update
    // Awake method executed once when the game starts even if the component wasen't called
    private void Awake()
    {
        // Get the component actual reference from charachter object
        body = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        wallJumpingAngle.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        WallSlide();
        WallJump();
        Move();
        CheckDirection();
        UpdateAnimation();
    }

    private void CheckInput()
    {
        if (isDashing)
        {
            return;
        }
        // Access the horizontal component from the input manager
        horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            canJump = true;
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }
    private void Move()
    {
        if (isDashing)
        {
            return;
        }
        // Change the running speed and directions
        if (!isWallSliding && !isTouchingWall)
        // Get axis horizontal allow the user to move left and right by the keyboard (-1,1)
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        }
        if (body.velocity.x != 0)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }
    private void CheckDirection()
    {
        if (isDashing)
        {
            return;
        }
        // Check the case of moving right without actually facing right and vice versa
        if (horizontalInput > 0 && !isFacingright)
        {
            flip();
        }
        else if (horizontalInput < 0 && isFacingright)
        {
            flip();
        }
    }
    void flip()
    {
        // Jumping direction will be the opposite of your current movement
        wallJumpingDirection *= -1; 
        isFacingright = !isFacingright;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpForce);
        Debug.Log("normal jump");
    }
    private bool isGrounded()
    {
        return isGround = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
    }
    private bool isWalled()
    {
       return isTouchingWall = Physics2D.OverlapBox(wallCheck.position, wallCheckSize,0 ,wallLayer);
    }
    private void WallSlide()
    {
        if (isWalled() && !isGrounded() && body.velocity.y < 0)
        {
            isWallSliding = true;
            //Debug.Log(isTouchingWall);
            body.velocity = new Vector2(body.velocity.x,wallSlidingSpeed);
        }
        else
        {
            isWallSliding = false;
        }
    }
    private void WallJump()
    {
        if ((isWallSliding && isTouchingWall)&& Input.GetButtonDown("Jump")) 
        {
            body.AddForce(new Vector2(wallJumpingForce * wallJumpingDirection * wallJumpingAngle.x, wallJumpingForce * wallJumpingAngle.y), ForceMode2D.Impulse);
            //canJump = false;
            Debug.Log("walljump");
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(wallCheck.position, wallCheckSize);
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = body.gravityScale;
        body.gravityScale = 0f;
        body.velocity = new Vector2((isFacingright ? 1 : -1) * DashingPower, 0f);
        tr.emitting = true;
        tr.startColor = Color.clear;
        tr.endColor = Color.clear;
        yield return new WaitForSeconds(DashingCoolDown);
        tr.emitting = false;
        body.gravityScale = originalGravity;
        isDashing = false;
        canDash = true;
    }
    private void UpdateAnimation()
    {
        anim.SetBool("isDashing", isDashing);
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isGrounded", isGrounded());
        anim.SetFloat("yVelocity", body.velocity.y);
        anim.SetBool("isSliding", isWallSliding);
    }

}
// Bugs
// --FIXED  Jumping while touching wall is so high (isTouchingWall & isGround & canJump)
// He can't jump while standing on a wall 
// --FIXED When it's (isTouchingWall & !isGround & !isWallSliding) it jumps 
// --FIXED Dashing animation while flying dosen't work
// Only the first frame of dashing appears
// --FIXED Error in the blind tree animations 
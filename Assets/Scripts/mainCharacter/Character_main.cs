using UnityEngine;

public class Character : MonoBehaviour
{
    // Public variables can be accessed through the gui , while private ones can't
    // But "SerializeFiel" let you acess your private vars from the gui
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D box;
    [SerializeField] private LayerMask groundLayer;

    private bool isFacingright = true;
    private bool isRunning;
    private float horizontalInput;

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    [Header("WallSliding")]
    [SerializeField] private float wallSlidingSpeed;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] public Transform wallCheck;
    [SerializeField] Vector2 wallCheckSize;
    [SerializeField] private bool isTouchingWall;
    [SerializeField] private bool isWallSliding;

    // Start method is called once before the first frame update
    // Awake method executed once when the game starts even if the component wasen't called
    private void Awake()
    {
        // Get the component actual reference from charachter object
        body = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        WallSlide();
        Move();
        CheckDirection();
        UpdateAnimation();
    }

    private void CheckInput()
    {
        // Access the horizontal component from the input manager
        horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            Jump();
        }
    }
    private void Move()
    {
        // Change the running speed and directions
        if(!isWallSliding) // Get axis horizontal allow the user to move left and right by the keyboard (-1,1)
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
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
        isFacingright = !isFacingright;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpForce);
    }
    private bool isGrounded()
    {
        return Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(wallCheck.position, wallCheckSize);
    }
    private void UpdateAnimation()
    {
        anim.SetBool("isRunning",isRunning);
        anim.SetBool("isGrounded", isGrounded());
        anim.SetFloat("yVelocity", body.velocity.y);
        anim.SetBool("isSliding", isWallSliding);
    }
}

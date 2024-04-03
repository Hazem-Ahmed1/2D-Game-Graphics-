using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tenguMovement : MonoBehaviour
{
    PlayerMovement playerclass;
    public float moveSpeed = 5f; // Adjust the speed as needed for player
    public float speed = 5f;
    public float groundRange = 5f; // Range for NPC of movement on the ground
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D body;
    public Animator anim;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private float horizontalInput;
    private bool isFacingRight = true; //default

    private Transform player;
    public float lineOfSite = 16;

    Collision2D collision;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("player").transform;
        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        body.freezeRotation = true;
        MoveCharacter();
        // Get the position of the character
        Vector2 currentPosition = transform.position;

        float distanceFromThePlayer = Vector2.Distance(player.position, this.transform.position);
       
        // Check if the player entered the character range 
        if (distanceFromThePlayer <= lineOfSite)
        { 
            checkPlayerDirection();
            anim.SetBool("runTowardsPlayer", true);  //correct
            OnCollisionStay2D(collision); //correct // if the player and character are collided the character will attack the player 
 
        }
        else if (distanceFromThePlayer > lineOfSite) // Check if the character is reaching the edge of the ground range to flip
        {
            anim.SetBool("runTowardsPlayer", false);
            if (currentPosition.x >= groundRange && isFacingRight || currentPosition.x <= -groundRange && !isFacingRight)
            {
                // Flip the character
                FlipCharacter();
            }
        }

    }

    void MoveCharacter()
    {
        // Move the character horizontally
        float horizontalInput = isFacingRight ? 1f : -1f;
        body.velocity = new Vector2(horizontalInput * moveSpeed, body.velocity.y);

    }

    void FlipCharacter()
    {
        // Flip the character horizontally
        isFacingRight = !isFacingRight;

        Vector3 myScale = this.transform.localScale;
        myScale.x *= -1;
        this.transform.localScale = myScale;
    }

    void checkPlayerDirection() //if scale.X is +ve the character is facing right , is -ve facing left.
    {
        if (player.transform.position.x < gameObject.transform.position.x && isFacingRight)
            FlipCharacter();
        if (player.transform.position.x > gameObject.transform.position.x && !isFacingRight)
            FlipCharacter();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);

    }

    private void OnDrawGizmos() //not necessary
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, groundRange);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "player")
            anim.SetBool("attackPlayer", true);
        else
        {
            anim.SetBool("attackPlayer", false); // IMP. condition
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkingWorm : MonoBehaviour
{

    public float moveSpeed = 5f; // Adjust the speed as needed
    public float groundRange = 8f; // Range of movement on the ground
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D body;
    public Animator anim;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private float horizontalInput;
    private bool isFacingRight = true; //default

    private void Awake()
    {
        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        //default that the worm is to walk 
        anim.SetBool("walkWorm", true);

        // Move the character
        MoveCharacter();

        // Check if the character needs to flip
        CheckFlip();
    }

    void MoveCharacter()
    {
        // Move the character horizontally
        float horizontalInput = isFacingRight ? 1f : -1f;
        body.velocity = new Vector2(horizontalInput * moveSpeed, body.velocity.y);
    }

    void CheckFlip()
    {
        // Get the position of the character
        Vector2 currentPosition = transform.position;

        // Check if the character is reaching the edge of the ground range
        if (currentPosition.x >= groundRange && isFacingRight || currentPosition.x <= -groundRange && !isFacingRight)
        {
            // Flip the character
            FlipCharacter();
        }
    }

    void FlipCharacter()
    {
        // Flip the character horizontally
        isFacingRight = !isFacingRight; 
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
}

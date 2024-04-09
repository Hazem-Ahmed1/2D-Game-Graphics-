using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkingWorm : MonoBehaviour
{

    public float moveSpeed = 5f; // Adjust the speed as needed
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D body;
    public Animator anim;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private float horizontalInput;
    private bool isFacingRight = true; //default
    public float lineOfSite = 3;
    public Transform startPoint;
    public Transform endPoint;



    private void Awake()
    {
        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPoint = GameObject.FindGameObjectWithTag("startPoint").transform;
        endPoint = GameObject.FindGameObjectWithTag("endPoint").transform;
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


        float distanceFromStartPoint = Vector2.Distance(startPoint.position, this.transform.position);
        float distanceFromEndPoint = Vector2.Distance(endPoint.position, this.transform.position);


        if (distanceFromStartPoint <= lineOfSite && isFacingRight == false)
        {
            FlipCharacter();
        }
        else if (distanceFromEndPoint <= lineOfSite && isFacingRight == true)
        {
            FlipCharacter();
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);

    }


    void FlipCharacter()
    {
        // Flip the character horizontally
        isFacingRight = !isFacingRight;

        Vector3 myScale = this.transform.localScale;
        myScale.x *= -1;
        this.transform.localScale = myScale;

    }


}


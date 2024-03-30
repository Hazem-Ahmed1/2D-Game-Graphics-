using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mageMovement : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float speed;
    private Rigidbody2D body;
    //[SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    private bool grounded;
    public Animator anim;
    private BoxCollider2D boxCollider;
   
    // [SerializeField] private float attakCooldown;
    //private float cooldownTimer = Mathf.Infinity;
    
    private float horizontalInput;

    private Transform player;
    public float linOfSite;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("player").transform;
        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    /*private void Awake()
    {
        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }*/

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");


        float distanceFromThePlayer = Vector2.Distance(player.position, transform.position);
        attackPlayer(distanceFromThePlayer);

        //body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);


        //body.transform.localScale *= 2f;

        //Flip player when moving left-right
        /*if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(5, 3, 1);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-5, 3, 1);*/

        //jump
        /*if (Input.GetKey(KeyCode.Space) && grounded )
        {
            Jump();
        }


        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            anim.SetTrigger("light-charge");
        }


        //Set animator parameters
        anim.SetBool("walkMage", horizontalInput != 0);*/
        //default that the mage is in the ground
        anim.SetBool("grounded", grounded);


        //dragon attack
        /*if (Input.GetMouseButtonDown(0) && cooldownTimer > attakCooldown && canAttak())
            Attak();

        cooldownTimer += Time.deltaTime;*/


    }


    void attackPlayer(float distanceFromThePlayer)
    {
        if (distanceFromThePlayer < linOfSite)
        {
            anim.SetTrigger("light-charge");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, linOfSite);
    }

    /*public void Attak()
    {
        anim.SetTrigger("dragon-attack");
        cooldownTimer = 0;
    }

    public bool canAttak()
    {
        return horizontalInput == 0;

    }
    /*
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
            anim.SetTrigger("dragon-death");
    }
    */
    private void OnCollisionStay2D(Collision2D collision)
     {
         if (collision.gameObject.tag == "ground")
             grounded = true ;
     }

    /*private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }*/

    /*private void Jump()
    {
         body.velocity = new Vector2(body.velocity.x, speed);
         grounded = false;
    }*/
       
    
}


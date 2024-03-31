using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private readonly float gravityScale = 5;
    private readonly float gravityFallScale = 6;
    private bool rightFacing;
    private bool grounded;
    [SerializeField] float speed = 5f;
    [SerializeField] float jump = 3f;
    //public GameObject deathEffect;
    public int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rightFacing = true;
        grounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float hInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * hInput, rb.velocity.y);

        if((rightFacing && (hInput < 0)) || (!rightFacing && (hInput > 0))) Flip();
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && grounded){
            // rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            float jumpForce = Mathf.Sqrt(jump * -2 * (Physics2D.gravity.y * rb.gravityScale));
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        if(rb.velocity.y >= 0)
        {
            rb.gravityScale = gravityScale;
        }
        else if (rb.velocity.y < 0)
        {
            rb.gravityScale = gravityFallScale;
        }
    }

    void Flip()
    {
        rightFacing = !rightFacing;
        Vector2 curr = gameObject.transform.localScale;
        curr.x *= -1;
        gameObject.transform.localScale = curr;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }
 
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

    public void TakeDamage (int damage)
	{
		health -= damage;

		if (health <= 0)
		{
            Destroy(gameObject);
            //Die();
		}
	}
    //TODO
    // void Die ()
    // {
    //     Instantiate(deathEffect, transform.position, Quaternion.identity);
    //     Destroy(gameObject);
    // }
}

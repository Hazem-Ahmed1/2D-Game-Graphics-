using UnityEngine;

public class Character : MonoBehaviour
{
    // Public variables can be accessed through the gui , while private ones can't
    // But "SerializeFiel" let you acess your private vars from the gui
    private Rigidbody2D body;
    private bool facingright = true;
    private BoxCollider2D box;
    //[SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;



    // Start method is called once before the first frame update
    // Awake method executed once when the game starts even if the component wasen't called
    private void Awake()
    {
        // Get the component actual reference from charachter object
        body = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Acess the horizontal component from the input manager
        float horizontalInput = Input.GetAxis("Horizontal");
        // Change the running speed and directions
        // Get axis horizontal allow the user to move left and right by the keyboard (-1,1)
        body.velocity = new Vector2(horizontalInput*speed, body.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            body.velocity = new Vector2(body.velocity.x, jumpForce);
        }
        // Check the case of moving right without actually facing right and vice versa
        if (horizontalInput > 0 && !facingright)
            flip();
        else if (horizontalInput < 0 && facingright)
            flip();
    }
    void flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1; 
        gameObject.transform.localScale =  currentScale;
        facingright = !facingright;
    }
    // This function has an error in its functionality
    private bool IsGround()
    {
        return Physics2D.BoxCast(box.bounds.center, box.bounds.size ,0f, Vector2.down, 0.1f, groundLayer);
    }
}

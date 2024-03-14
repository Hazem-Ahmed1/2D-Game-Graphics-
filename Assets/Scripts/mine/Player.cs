using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rb;
    private readonly float _gravityScale = 5;
    private readonly float _gravityFallScale = 6;
    private bool _rightFacing;
    private bool _grounded;
    [SerializeField] float _speed = 5f;
    [SerializeField] float _jump = 3f;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rightFacing = true;
        _grounded = true;
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
        _rb.velocity = new Vector2(_speed * hInput, _rb.velocity.y);

        if((_rightFacing && (hInput < 0)) || (!_rightFacing && (hInput > 0))) Flip();
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && _grounded){
            // _rb.AddForce(Vector2.up * _jump, ForceMode2D.Impulse);
            float jumpForce = Mathf.Sqrt(_jump * -2 * (Physics2D.gravity.y * _rb.gravityScale));
            _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        if(_rb.velocity.y >= 0)
        {
            _rb.gravityScale = _gravityScale;
        }
        else if (_rb.velocity.y < 0)
        {
            _rb.gravityScale = _gravityFallScale;
        }
    }

    void Flip()
    {
        _rightFacing = !_rightFacing;
        Vector2 curr = gameObject.transform.localScale;
        curr.x *= -1;
        gameObject.transform.localScale = curr;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _grounded = true;
        }
    }
 
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _grounded = false;
        }
    }
}

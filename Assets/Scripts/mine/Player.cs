using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] float _speed = 5f;
    [SerializeField] float _jump = 1f;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move(){
        float hInput = Input.GetAxis("Horizontal");

        _rb.velocity = new Vector2(_speed * hInput, _rb.velocity.y);
    }

    void Jump(){
        if(Input.GetButtonDown("Jump")){
            float jumpForce = Mathf.Sqrt(_jump * -2 * (Physics2D.gravity.y * _rb.gravityScale));
            // _rb.AddForce(Vector2.up * _jump, ForceMode2D.Impulse);
            _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }
}

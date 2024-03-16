using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    [SerializeField] private float attakCooldown;
    private float cooldownTimer = Mathf.Infinity;
    private float horizontalInput;

    private void Awake()
    {
        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        //body.transform.localScale *= 2f;

        //Flip player when moving left-right
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(3, 2, 1);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-3, 2, 1);

        if (Input.GetKey(KeyCode.Space))
            body.velocity = new Vector2(body.velocity.x, speed);


        //Set animator parameters
        anim.SetBool("lizard-walk", horizontalInput != 0);
        //anim.SetBool("grounded", isGrounded());


        //dragon attack
        if (Input.GetMouseButtonDown(0) && cooldownTimer > attakCooldown && canAttak())
            Attak();

        cooldownTimer += Time.deltaTime;

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
            anim.SetTrigger("lizard-death");
    }

    public void Attak()
    {
        anim.SetTrigger("lizard-attack");
        cooldownTimer = 0;
    }

    public bool canAttak()
    {
        return horizontalInput == 0;

    }
}   

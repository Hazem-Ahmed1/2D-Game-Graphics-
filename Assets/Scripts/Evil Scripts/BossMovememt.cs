using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovememt : MonoBehaviour
{

    Animator boss_animator;
    public float speed = 5.0f;
    public float jumpForce = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        boss_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float direction = Input.GetAxisRaw("Horizontal");
        if (direction != 0)
        {
            boss_animator.SetBool("Move", true);
        }else {
            boss_animator.SetBool("Move", false);
        }

        if (direction < 0)
        {
          transform.rotation = new Quaternion(0, 180, 0, 0);

        }else if(direction > 0)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);

        }
        transform.position += new Vector3(direction * Time.deltaTime * speed, 0, 0);

        if (Input.GetKeyDown("space") && Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) < 0.01f)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, jumpForce, 0);
        }

    }
}

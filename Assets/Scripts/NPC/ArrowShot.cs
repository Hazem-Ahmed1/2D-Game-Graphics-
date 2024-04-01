using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShot : MonoBehaviour
{
    GameObject target;
    public float speed = 10;
    Rigidbody2D ArrowRB;
    public bool isFlipped = false;
    // Start is called before the first frame update
    void Start()
    {
        ArrowRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        LookAtPlayer();
        Vector2 moveDir = (target.transform.position - this.transform.position).normalized * speed;
        ArrowRB.velocity = new Vector2(moveDir.x, moveDir.y);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject,3);
        }
    }
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;
        if (transform.position.x > target.transform.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
        else if (transform.position.x < target.transform.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
    }
}

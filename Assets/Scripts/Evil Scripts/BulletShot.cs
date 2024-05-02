using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShot : MonoBehaviour
{
    GameObject target;
    public GameObject Fire_Spark;
    private float speed_Bullet = 20;
    Rigidbody2D bulletRB;
    public bool isFlipped = false;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(15, 11, true);
        Physics2D.IgnoreLayerCollision(15, 6, true);
        Physics2D.IgnoreLayerCollision(15, 16, true);
        Physics2D.IgnoreLayerCollision(15, 17, true);
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        LookAtPlayer();
        Vector2 moveDir = (target.transform.position - this.transform.position).normalized * speed_Bullet;
        bulletRB.velocity = new Vector2(moveDir.x, moveDir.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(Fire_Spark, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
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

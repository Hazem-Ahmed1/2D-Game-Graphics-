using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BulletPlayer : MonoBehaviour
{
    public float speed_Bullet = 10;
    Rigidbody2D BulletRB;
    SpriteRenderer BulletSR;
    public bool isFlipped = false;
    // Start is called before the first frame update
    void Start()
    {
        BulletRB = GetComponent<Rigidbody2D>();
        BulletSR = GetComponent<SpriteRenderer>();
        if (Main_Character.isFlied)
        {
            BulletSR.flipX = true;
            Vector2 moveDir = (new Vector3(-500,this.transform.position.y,this.transform.position.z) - this.transform.position).normalized * speed_Bullet;
            BulletRB.velocity = new Vector2(moveDir.x, moveDir.y);
        }
        else
        {
            BulletSR.flipX = false;
            Vector2 moveDir = (new Vector3(500,this.transform.position.y,this.transform.position.z) - this.transform.position).normalized * speed_Bullet;
            BulletRB.velocity = new Vector2(moveDir.x, moveDir.y);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("stoneBoss"))
        {
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject,3);
        }
    }
}

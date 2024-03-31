using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneArm : MonoBehaviour
{   
    public float speed = 5f;
    public Rigidbody2D rb;
    
    public NPCAttributes attributes;
    private Transform player;
    public int damage = 20 ;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player.position.x > transform.position.x)
        {
            rb.velocity = Vector2.right * speed;
        }
        else
        {
            rb.velocity = Vector2.left * speed;
        }
	}
    void OnTriggerEnter2D (Collider2D hitInfo)
	{   
        Player p = hitInfo.GetComponent<Player>();
        if (p != null)
		{
			p.TakeDamage(20);
		}
		Destroy(gameObject);
	}
}

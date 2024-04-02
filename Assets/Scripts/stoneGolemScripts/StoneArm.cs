using UnityEngine;

public class StoneArm : MonoBehaviour
{   
    public float track = 1f;
    private float timer = 0;
    public float speed = 0.5f;
    public Rigidbody2D rb;
    private Transform player;
    public int damage = 20;

	void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        KeepTracking();
	}

    void Update(){
        if (timer < track)
        {
            timer += Time.deltaTime;
        }
        else
        {
            KeepTracking();
            timer = 0;
        }
    }

    void KeepTracking(){
        Vector3 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }

    // void OnCollisionEnter2D(Collision2D collision)
	// {   
    //     if (collision.gameObject.CompareTag("Player"))
	// 	{
    //         Player p = player.GetComponent<Player>();
	// 		p.TakeDamage(damage);
    //         Debug.Log("HIT SHOOT");
	// 	}
	// 	Destroy(gameObject);
	// }

    void OnTriggerEnter2D(Collider2D hitInfo)
	{   
        Player p = hitInfo.GetComponent<Player>();
        if (p != null)
		{
			p.TakeDamage(damage);
            Debug.Log("HIT SHOOT");
		}
        Debug.Log(hitInfo.name);
        if(hitInfo.name != "arm_projectile(Clone)")
		    Destroy(gameObject);
	}

    // private void OnBecameInvisible()
    // {
    //     // Destroy the object when it becomes invisible
    //     Destroy(gameObject);
    // }
}

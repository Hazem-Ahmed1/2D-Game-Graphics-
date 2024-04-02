using UnityEngine;

public class StoneArm : MonoBehaviour
{   
    public float track = 1f;
    private float timer = 0;
    public float speed = 0.5f;
    public Rigidbody2D rb;
    
    public NPCAttributes attributes;
    private Transform player;
    public int damage = 20 ;

	void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        keepTracking();
	}

    void Update(){
    if (timer < track)
        {
            timer += Time.deltaTime;
        }
        else
        {
                keepTracking();
            timer = 0;
        }

    }
    void keepTracking(){
        Vector3 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }
    void OnTriggerEnter2D(Collider2D hitInfo)
	{   
        Player p = hitInfo.GetComponent<Player>();
        if (p != null)
		{
			p.TakeDamage(damage);
		}
		Destroy(gameObject);
	}

    // private void OnBecameInvisible()
    // {
    //     // Destroy the object when it becomes invisible
    //     Destroy(gameObject);
    // }
}

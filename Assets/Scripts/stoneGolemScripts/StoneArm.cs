using UnityEngine;

public class StoneArm : MonoBehaviour
{   
    public float track = 1f;
    private float timer = 0;
    private float timeToDie = 0;
    public float speed = 0.5f;
    public Rigidbody2D rb;
    private Transform player;
    public int damage = 20;
    Vector3 direction;

	void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if(player) {
            direction = (player.position - transform.position).normalized;
        }
        else{
            direction = -transform.right.normalized;
        }
	}

    void Update(){
        if (timer < track)
        {
            timer += Time.deltaTime;
        }
        else
        {
            
            rb.velocity = direction * speed;
            timer = 0;
        }
        timeToDie +=Time.deltaTime;
        Kill();
    }
    void OnTriggerEnter2D(Collider2D hitInfo)
	{   
        Player p = hitInfo.GetComponent<Player>();
        if (p != null)
		{
			p.TakeDamage(damage);
            Destroy(this.gameObject);
		}
	}
    public void Kill(){
        if(timeToDie > 6){
            Destroy(gameObject);
        }
    }
}

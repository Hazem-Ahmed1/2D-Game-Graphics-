using UnityEngine;

public class StoneNPC : NPC
{
    public Transform firePoint;
    public Transform firePoint2;
    public GameObject stonePrefap;
    public LineRenderer lineRenderer;
    public HealthBar healthBar;
    public LayerMask laserHitLayers;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        currHealth = attributes.healthPoints;
        healthBar.SetMaxHealth(currHealth);
    }

    void FixedUpdate()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // void Update(){
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         BossTakeDamage(15);
    //     }
    // }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player_Sowrd"))
        {
            // Effect();
            // Destroy(this.gameObject);
            // Player player = collision.gameObject.GetComponent<Player>();
            BossTakeDamage(15);
        }
        else if(collision.gameObject.CompareTag("Player_Bullet"))
        {
            BossTakeDamage(5);
        }
    }
    public void shoot()
    {
        Instantiate(stonePrefap, firePoint.position, firePoint.rotation);
    }
    public void BossTakeDamage(int damage)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("immune"))
        {
            currHealth -= damage;
            healthBar.SetHealth(currHealth);
            if (currHealth <= 0)
            {
                Die();
            }
        }
    }

}

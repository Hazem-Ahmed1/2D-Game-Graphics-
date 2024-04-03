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
    void update(){
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BossTakeDamage(15);
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

using UnityEngine;

public class StoneNPC : NPC
{
    public Transform firePoint;
    public Transform firePoint2;
    public GameObject stonePrefap;
    public LineRenderer lineRenderer;
    public HealthBar healthBar;
    public LayerMask laserHitLayers;
    public GameObject VictoryPanel;
    [SerializeField] AudioSource takeDmg;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        if (attributes != null)
        {
            currHealth = attributes.healthPoints;
            healthBar.SetMaxHealth(currHealth);
        }
    }

    void FixedUpdate()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player_Sowrd"))
        {
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
                appearVictoryPanel();
            }
        }
    }
    public void appearVictoryPanel()
    {
        VictoryPanel.SetActive(true);
    }

}

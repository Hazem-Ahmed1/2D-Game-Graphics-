using UnityEngine;

public class StoneNPC : NPC
{
    public Transform firePoint;
    public Transform firePoint2;
    public GameObject stonePrefap;
    public LineRenderer lineRenderer;
    public HealthBar healthBar;
    protected const string BOSS_SHOOT = "shoot";
    protected const string BOSS_WALK = "walk";
    protected const string BOSS_IMMUNE = "immune";
    protected const string BOSS_LAZER = "lazer";

    //private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        currHealth = attributes.healthPoints;
        // healthBar.SetMaxHealth(currHealth);
    }

    void FixedUpdate()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        BossMove();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            BossTakeDamage(15);
        }
    }

    private void BossMove()
    {
        if(isDead || isHurt) return;

        LookAtPlayer();
        float distanceToPlayer = Vector2.Distance(player.transform.position, rb.position);

        if (distanceToPlayer < attributes.atkRange && !isAttacking)
        {
            ChangeAnimationState(BOSS_SHOOT);
            isAttacking = true;
            // player.GetComponent<Player>().TakeDamage(attributes.damage);

            Invoke("AttackDone", animator.GetCurrentAnimatorStateInfo(0).length);
        }
        else if (distanceToPlayer < attributes.lookRange && !isAttacking)
        {
            ChangeAnimationState(BOSS_WALK);
            MoveToPlayer();
        }
        else if (!isAttacking)
        {
            ChangeAnimationState(NPC_IDLE);
        }
    }

    public void shoot()
    {
        Instantiate(stonePrefap, firePoint.position, firePoint.rotation);
    }

    public void lazer()
    {
		RaycastHit2D hitInfo = Physics2D.Raycast(firePoint2.position, -firePoint2.right); // Cast ray in the opposite direction
        Player p = hitInfo.transform.GetComponent<Player>();
        if (p != null)
        {
            Debug.Log("boobs");
            p.TakeDamage(20);
            lineRenderer.SetPosition(0, firePoint2.position);
            lineRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            Debug.Log("boooooobs");
            lineRenderer.SetPosition(0, firePoint2.position);
            lineRenderer.SetPosition(1, firePoint2.position - firePoint2.right * 100); // Extend the line in the opposite direction
        }
    }

    public void BossTakeDamage(int damage)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("immune"))
        {
            currHealth -= damage;
            // healthBar.SetHealth(currHealth);
            if (currHealth <= 0)
            {
                Die();
            }
        }
    }

}

using UnityEngine;

public class NPC : MonoBehaviour
{
    protected Transform player;
    protected Rigidbody2D rb;
    protected Animator animator;
    private string currentAnimaton;
    public NPCAttributes attributes;
    protected bool isFlipped = false;
    protected bool isAttacking = false;
    protected bool isHurt = false;
    protected bool isDead = false;
    public int currHealth;
    public bool boss = false;
    protected const string NPC_IDLE = "idle";
    protected const string NPC_RUN = "run";
    protected const string NPC_ATTACK_1 = "atk_1";
    protected const string NPC_ATTACK_2 = "atk_2";
    protected const string NPC_ATTACK_3 = "atk_3";
    protected const string NPC_RUN_ATTACK = "run_atk";
    protected const string NPC_HURT = "hurt";
    protected const string NPC_DEATH = "death";
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currHealth = attributes.healthPoints;
    }

    void Update()
    {   
        LookAtPlayer();
    }

    void FixedUpdate()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if(!boss) 
            BasicMovement();
        
    }

    protected void BasicMovement()
    {
        if(isDead || isHurt) return;

        LookAtPlayer();
        float distanceToPlayer = Vector2.Distance(player.transform.position, rb.position);

        if (distanceToPlayer < attributes.atkRange && !isAttacking)
        {
            ChangeAnimationState(NPC_ATTACK_1);
            isAttacking = true;
            player.GetComponent<Player>().TakeDamage(attributes.damage);

            Invoke("AttackDone", animator.GetCurrentAnimatorStateInfo(0).length);
        }
        else if (distanceToPlayer < attributes.lookRange && !isAttacking)
        {
            ChangeAnimationState(NPC_RUN);
            MoveToPlayer();
        }
        else if (!isAttacking)
        {
            ChangeAnimationState(NPC_IDLE);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            isHurt = true;
            TakeDamage(30);
        }
    }

    protected void MoveToPlayer()
    {
        Vector2 target = new(player.position.x, rb.position.y);
        Vector2 newpos = Vector2.MoveTowards(rb.position, target, attributes.speed * Time.fixedDeltaTime);
        rb.MovePosition(newpos);
    }

    protected void MoveToPlayer(float speed)
    {
        Vector2 target = new(player.position.x, rb.position.y);
        Vector2 newpos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newpos);
    }

    public void TakeDamage(int damage)
    {
        ChangeAnimationState(NPC_HURT);
        currHealth -= damage;
        Debug.Log("hurt: "+currHealth);
        Invoke("DamageDone", animator.GetCurrentAnimatorStateInfo(0).length);

        if(currHealth <= 0)
        {
            Die();
        }
    }


    public void LookAtPlayer()
{
    if ((transform.position.x > player.position.x && isFlipped) ||
        (transform.position.x < player.position.x && !isFlipped))
    {
        transform.Rotate(0f, 180f, 0f);
        isFlipped = !isFlipped;
    }
}

    protected void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimaton == newAnimation) return;

        animator.Play(newAnimation);
        //Debug.Log(newAnimation+" is being played");
        currentAnimaton = newAnimation;
    }

    void AttackDone()
    {
        isAttacking = false;
    }
    void DamageDone()
    {
        isHurt = false;
    }
    
    void Die()
    {   
        ChangeAnimationState(NPC_DEATH);
        isDead = true;
        Debug.Log("dead");
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }
}

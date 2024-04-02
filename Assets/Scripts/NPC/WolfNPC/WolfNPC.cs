using UnityEngine;

public class WerewolfNPC : NPC
{   
    void Start()
    {
        
    }

    void FixedUpdate()
    {   
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        BasicMovement();
    }
    
    protected new void BasicMovement()
    {
        if(isDead || isHurt) return;

        LookAtPlayer();
        float distanceToPlayer = Vector2.Distance(player.transform.position, rb.position);

        if (distanceToPlayer < (attributes.atkRange + 0.3f) && distanceToPlayer >= attributes.atkRange && !isAttacking)
        {
            ChangeAnimationState(NPC_RUN_ATTACK);
            isAttacking = true;
            MoveToPlayer(10);
            // player.GetComponent<Player>().TakeDamage(attributes.damage);
            Invoke("AttackDone", animator.GetCurrentAnimatorStateInfo(0).length);
        }
        else if (distanceToPlayer < attributes.atkRange && !isAttacking)
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
}

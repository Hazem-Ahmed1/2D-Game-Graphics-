using System;
using UnityEngine;

    public class NPC : MonoBehaviour
    {
        protected Transform player;
        protected Rigidbody2D rb;
        protected Animator animator;
        public NPCAttributes attributes;
        private bool isFlipped = false;
        private bool isAttacking = false;
        public int currHealth;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
            // currHealth = attributes.healthPoints;
        }

        void Update()
        {   
            
        }

        void FixedUpdate()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
            
            LookAtPlayer();
            BasicMovement();
        }

        protected void BasicMovement()
        {
            float distanceToPlayer = Vector2.Distance(player.position, rb.position);

            if (distanceToPlayer < attributes.atkRange && !isAttacking)
            {
                isAttacking = true;
                animator.SetBool("isAttacking", true);
                Invoke("AttackDone", animator.GetCurrentAnimatorStateInfo(0).length);
            }
            else if (distanceToPlayer < attributes.lookRange && !isAttacking)
            {
                animator.SetBool("isRunning", true);
                Vector2 target = new(player.position.x, rb.position.y);
                Vector2 newpos = Vector2.MoveTowards(rb.position, target, attributes.speed * Time.fixedDeltaTime);
                rb.MovePosition(newpos);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
        }

    void TakeDamage(int damage)
    {
        currHealth -= damage;
        if(currHealth <= 0)
        {
            Destroy(gameObject);
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

    void AttackDone()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", false);
    }
}

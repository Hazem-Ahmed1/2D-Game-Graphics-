using System;
using UnityEngine;

    public class NPC : MonoBehaviour
    {
        protected Transform player;
        protected Rigidbody2D rb;
        protected Animator animator;
        private string currentAnimaton;
        public NPCAttributes attributes;
        private bool isFlipped = false;
        private bool isAttacking = false;
        public int currHealth;
        const string NPC_IDLE = "idle";
        const string NPC_RUN = "run";
        const string NPC_ATTACK_1 = "atk_1";
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        void Update()
        {   
            
        }

        void FixedUpdate()
        {
            BasicMovement();
        }

        protected void BasicMovement()
        {
            LookAtPlayer();

            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag("Player").transform;

            float distanceToPlayer = Vector2.Distance(player.position, rb.position);

            if (distanceToPlayer < attributes.atkRange && !isAttacking)
            {
                ChangeAnimationState(NPC_ATTACK_1);
                isAttacking = true;
                Invoke("AttackDone", animator.GetCurrentAnimatorStateInfo(0).length);
            }
            else if (distanceToPlayer < attributes.lookRange && !isAttacking)
            {
                ChangeAnimationState(NPC_RUN);
                Vector2 target = new(player.position.x, rb.position.y);
                Vector2 newpos = Vector2.MoveTowards(rb.position, target, attributes.speed * Time.fixedDeltaTime);
                rb.MovePosition(newpos);
            }
            else if (!isAttacking)
            {
                ChangeAnimationState(NPC_IDLE);
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

        void ChangeAnimationState(string newAnimation)
        {
            if (currentAnimaton == newAnimation) return;

            animator.Play(newAnimation);
            Debug.Log(newAnimation+" is being played");
            currentAnimaton = newAnimation;
        }

        void AttackDone()
        {
            isAttacking = false;
        }
    }

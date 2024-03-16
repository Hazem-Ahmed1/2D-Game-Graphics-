using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovememt : MonoBehaviour
{
    private enum MovementState { Idle, Move, Attack }
    private Rigidbody2D rb;
    private SpriteRenderer SR;
    Animator boss_animator;
    public float shootingRange = 15;
    public float middleRange = 8;
    public float attackRange = 3;
    public float speed = 5.0f;
    public float jumpForce = 10.0f;
    public Transform Player;
    public float fireRate = 1;
    private float nextFireTime;
    public GameObject bullet;
    public GameObject BulletParent;

    // Start is called before the first frame update
    void Start()
    {
        boss_animator = GetComponent<Animator>();
        SR = GetComponent <SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(Player.position, this.transform.position);
        if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time && distanceFromPlayer > middleRange)
        {
            Instantiate(bullet, BulletParent.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
        else if (distanceFromPlayer <= middleRange && distanceFromPlayer > attackRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, Player.position, speed * Time.deltaTime);
        }


            UpdateAnimationState(distanceFromPlayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, shootingRange);
        Gizmos.DrawWireSphere(this.transform.position, middleRange);
        Gizmos.DrawWireSphere(this.transform.position, attackRange);
    }

    private void UpdateAnimationState(float Distance)
    {
        MovementState state;
        if (Distance < middleRange && Distance > attackRange && Player.position.x > this.transform.position.x)
        {
            state = MovementState.Move;
            SR.flipX = false;
        }
        else if (Distance < middleRange && Distance > attackRange && Player.position.x < this.transform.position.x)
        {
            state = MovementState.Move;
            SR.flipX = true;
        }
        else if (Distance <= attackRange)
        {
            state = MovementState.Attack;
        }
        else
        {
            state = MovementState.Idle;
        }
        boss_animator.SetInteger("State", (int)state);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Prevent player from moving through the enemy
            //Vector2 direction = transform.position - collision.transform.position;
            //direction.Normalize();
            //transform.position = (Vector2)collision.transform.position + direction * 1.0f; // Adjust this value as needed
            rb.bodyType = RigidbodyType2D.Static;
        }
    }
}

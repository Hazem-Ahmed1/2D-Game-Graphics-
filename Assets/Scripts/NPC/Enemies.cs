using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Enemies : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float lineOfSite = 5;
    [SerializeField]
    private float attackRange = 3;
    [SerializeField]
    private int EnemyHealth = 10;
    [SerializeField]
    private Transform Player;
    private Animator anim;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private enum MovementState {idle, running, attack}
    MovementState state = MovementState.idle;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    
    void FixedUpdate()
    {
        float distanceFromPlayer = Vector2.Distance(Player.position, this.transform.position);
        if(distanceFromPlayer < lineOfSite && distanceFromPlayer >= attackRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, Player.position, speed* Time.deltaTime);
        }
        UpdateAnimationState(distanceFromPlayer , EnemyHealth);
    }

    private void UpdateAnimationState(float distanceFromPlayer, int EnemyHealth)
    {
        if(distanceFromPlayer < lineOfSite && distanceFromPlayer > attackRange && Player.position.x > this.transform.position.x && EnemyHealth > 0)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            state = MovementState.running;
            sr.flipX = false;
        }
        else if(distanceFromPlayer < lineOfSite && distanceFromPlayer > attackRange && Player.position.x < this.transform.position.x && EnemyHealth > 0)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            state = MovementState.running;
            sr.flipX = true;
        }
        else if(distanceFromPlayer <= attackRange && EnemyHealth > 0)
        {
            rb.bodyType = RigidbodyType2D.Static;
            state = MovementState.attack;
        }
        else
        {
            // rb.bodyType = RigidbodyType2D.Dynamic;
            state = MovementState.idle;
        }
        anim.SetInteger("state", (int)state);
    }

    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Bullet") && EnemyHealth > 0)
        {
            EnemyHealth--;
            anim.SetTrigger("hurt");
            Debug.Log(EnemyHealth);
            StartCoroutine("Hurt");
        }
        else if(collision.gameObject.name.Equals("Bullet") && EnemyHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator Hurt()
    {
        yield return new WaitForSeconds(0.5f);
    }
    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
        // destroyEnemyObject();
    }
    public void destroyEnemyObject()
    {
        Destroy(this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, lineOfSite);
        Gizmos.DrawWireSphere(this.transform.position, attackRange);
    }
}



// private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Bullet"))
    //     {
    //         TakeDamage();
    //     }
    //     if (EnemyHealth == 0)
    //     {
    //         Die();
    //     }
    // }

// take damage
    // public void TakeDamage()
    // {
    //     EnemyHealth--;
    //     Debug.Log(EnemyHealth);
    //     // hurt = true;
    // }
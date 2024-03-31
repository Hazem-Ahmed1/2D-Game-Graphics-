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
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(8,8);
        speed = 1;
    }

    // Update is called once per frame
    private void FixedUpdate()
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
            state = MovementState.running;
            sr.flipX = false;
        }
        else if(distanceFromPlayer < lineOfSite && distanceFromPlayer > attackRange && Player.position.x < this.transform.position.x && EnemyHealth > 0)
        {
            state = MovementState.running;
            sr.flipX = true;
        }
        else if(distanceFromPlayer <= attackRange && EnemyHealth > 0)
        {
            state = MovementState.attack;
        }
        else
        {
            state = MovementState.idle;
        }
        anim.SetInteger("state", (int)state);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Bullet") && EnemyHealth >= 0)
        {
            TakeDamage(1);
            if (EnemyHealth == 0)
            {
                anim.ResetTrigger("hurt");
                Die();
            }
        }
    }
    private void TakeDamage(int damage)
    {
        EnemyHealth = EnemyHealth - damage;
        anim.SetTrigger("hurt");
        Debug.Log(EnemyHealth);
        StartCoroutine("Hurt");
    }
    IEnumerator Hurt()
    {
        yield return new WaitForSeconds(0.5f);
    }
    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
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
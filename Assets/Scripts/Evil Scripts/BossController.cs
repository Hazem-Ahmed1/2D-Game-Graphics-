using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private enum MovementState { Idle, Move, Attack }
    private Rigidbody2D rb;
    private SpriteRenderer SR;
    Animator boss_animator;
    public Transform player;
    public bool isFlipped = false;
    public int Damage = 10;
    private bool heal = true;
    public int BossHealth = 5;
    // Start is called before the first frame update
    void Start()
    {
        boss_animator = GetComponent<Animator>();
        SR = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;

    }
    private void FixedUpdate()
    {
        if (heal && HealthBarBoss.Health <= 35)
        {
            boss_animator.SetTrigger("Heal");
            HealthBarBoss.Healing(25);
            Debug.Log("Before");
            heal = false;
            Debug.Log("After");

        }
    }
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
        else if (transform.position.x < player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bullet") && HealthBarBoss.Health >= 0)
        {
            HealthBarBoss.TakeDamage(Damage);
            boss_animator.SetTrigger("Take Hit");
            Debug.Log(HealthBarBoss.Health);
            StartCoroutine("Hurt");
            if (HealthBarBoss.Health <= 0)

                if (collision.gameObject.CompareTag("bullet") && BossHealth >= 0)
                {
                    BossHealth--;
                    boss_animator.SetTrigger("Take Hit");
                    Debug.Log(BossHealth);
                    StartCoroutine("Hurt");
                    if (BossHealth == 0)
                    {
                        boss_animator.ResetTrigger("Take Hit");
                        Die();
                    }
                }

        }

    }


    IEnumerator Hurt()
    {
        yield return new WaitForSeconds(0.5f);
    }
    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        boss_animator.SetTrigger("Death");
        // destroyEnemyObject();
    }
    public void destroyEnemyObject()
    {
        Destroy(this.gameObject);
    }

}
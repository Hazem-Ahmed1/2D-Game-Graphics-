using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float MC_health;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private GameObject UIhealthBar;
    private Animator animator;
    public static bool isStatic = false;
    private Rigidbody2D rb;
    [SerializeField] GameObject Bloodvfx;
    [SerializeField] Transform bloodPos;
    [SerializeField] MC_HealthBar healthBar;
    // public AudioSource audioSource;
    // public AudioClip hurt,death;
    void Start()
    {
        MC_health = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        healthBar = UIhealthBar.GetComponentInChildren<MC_HealthBar>();
        healthBar.UpdateHealthBar(MC_health, maxHealth);
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("damage") && MC_health >= 0)
        {
            TakeDamage(15);
            if (MC_health <= 0)
            {
                Death();
            }
            else
            {
                StartCoroutine(TriggerHurtAnimation());
            }
        }
        else if (collision.CompareTag("Arrow") && MC_health >= 0)
        {
            TakeDamage(10);
            if (MC_health <= 0)
            {
                Death();
            }
            else
            {
                StartCoroutine(TriggerHurtAnimation());
            }
        }
        else if (collision.CompareTag("Lava") && MC_health >= 0)
        {
            TakeDamage(10);
            if (MC_health <= 0)
            {
                Death();
            }
            else
            {
                StartCoroutine(TriggerHurtAnimation());
            }
        }
    }
    public void TakeDamage(int damage)
    {
        // audioSource.clip = hurt;
        // audioSource.Play();
        MC_health -= damage;
        healthBar.UpdateHealthBar(MC_health, maxHealth);
        var blood = Instantiate(Bloodvfx,bloodPos.transform.position, Quaternion.identity);
        Destroy(blood,0.5f);
    }
    IEnumerator TriggerHurtAnimation()
    {
        animator.SetTrigger("isHurt");
        yield return new WaitForSeconds(2.0f);
    }
    public void Death()
    {
        isStatic = true;
        animator.SetTrigger("isDead");
        rb.bodyType = RigidbodyType2D.Static;
        //audioSource.clip = death;
        //audioSource.Play();
    }
    public void MC_Destroy()
    {
        Destroy(this.gameObject);
    }
    

}

// Done -- hurt animations
// shooting
// UI
// FIXED -- attacking animations 
// FIXED -- Death animation dosen't work while attacking 

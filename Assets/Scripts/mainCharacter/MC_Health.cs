using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float MC_health;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private MC_HealthBar healthBar;
    private Animator animator;
    public static bool isStatic = false;
    private Rigidbody2D rb;
    void Start()
    {
        MC_health = maxHealth;
        Debug.Log("MC_health = " + MC_health);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        healthBar = GetComponentInChildren<MC_HealthBar>();
        healthBar.UpdateHealthBar(MC_health, maxHealth);
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("damage") || collision.CompareTag("Arrow")) && MC_health >= 0)
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
    }
    public void TakeDamage(int damage)
    {
        MC_health -= damage;
        healthBar.UpdateHealthBar(MC_health, maxHealth);
        Debug.Log("player_health = "+MC_health);
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
    }
    public void MC_Destroy()
    {
       // Debug.Log("isStatic = "+isStatic);
        Destroy(this.gameObject);
    }
    

}

// Done -- hurt animations
// shooting
// UI
// FIXED -- attacking animations 
// FIXED -- Death animation dosen't work while attacking 

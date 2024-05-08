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
    [SerializeField] public AudioSource deathSound;
    [SerializeField] public AudioSource hitSound;
    [SerializeField] public float health;
    [SerializeField] public bool meleeAttack = false;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spikes"))
        {
            Death();
        }

        if (collision.gameObject.CompareTag("Lava") && MC_health >= 0)
        {
            TakeDamage(40);
            if (MC_health <= 0)
            {
                Death();
            }
            else
            {
                StartCoroutine(TriggerHurtAnimation());
            }
        }
        if (collision.gameObject.CompareTag("health") && MC_health < maxHealth)
        {
            
            MC_health += health;
            healthBar.UpdateHealthBar(MC_health, maxHealth);
        }
    }
    void Start()
    {
        MC_health = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        healthBar = UIhealthBar.GetComponentInChildren<MC_HealthBar>();
        healthBar.UpdateHealthBar(MC_health, maxHealth);
        health = 10f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("damage") && MC_health >= 0 && !meleeAttack)
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
        else if (collision.CompareTag("Arrow") && MC_health >= 0 && !meleeAttack)
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
        hitSound.Play();

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
        deathSound.Play();
        isStatic = true;
        animator.SetTrigger("isDead");
    }
    public void MC_Destroy()
    {
        isStatic= false;
    }

    public void meleeAttackTrue(){
        meleeAttack = true;
    }

    public void meleeAttackFalse(){
        meleeAttack = false;
    }
}

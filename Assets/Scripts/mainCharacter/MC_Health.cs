using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int MC_health;
    [SerializeField] private int maxHealth = 100;
    private Animator animator;
    public static bool isStatic = false;
    private Rigidbody2D rb;
    void Start()
    {
        MC_health = maxHealth;
        Debug.Log("MC_health = " + MC_health);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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
        }
    }
    public void TakeDamage(int damage)
    {
        MC_health -= damage;
        Debug.Log("player_health = "+MC_health);
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

// hurt animations
// shooting 
// attacking animations 
// FIXED -- Death animation dosen't work while attacking 

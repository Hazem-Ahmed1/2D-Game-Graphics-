using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int MC_health;
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private bool isDead = false;
    private Animator animator;
    private Rigidbody2D rb;
    void Start()
    {
        MC_health = maxHealth;   
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
            TakeDamage(1);
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
        //isDead = true;
        rb.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("isDead");
    }
    public void MC_Destroy()
    {
        Destroy(this.gameObject);
    }
    

}

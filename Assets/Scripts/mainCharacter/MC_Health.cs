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
    
    void Start()
    {
        MC_health = maxHealth;   
    }

    void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        MC_health -= damage;
        if (MC_health <= 0) 
        {
            Death();
        }
    }
    public void Death()
    {
        isDead = true;
        animator.SetBool("isDead", isDead);
    }
    public void MC_Destroy()
    {
        Destroy(gameObject);
    }
    

}

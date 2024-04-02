using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneNPC : NPC
{
    public Transform firePoint;
    public Transform firePoint2;
    public GameObject stonePrefap;
    public LineRenderer lineRenderer;
    public healthBar healthBar;
    AnimatorStateInfo currentState;
    //private Rigidbody2D rb;

    void Start()
    {
        currHealth = attributes.healthPoints;
        healthBar.SetMaxHealth(currHealth);
        rb = GetComponent<Rigidbody2D>();
        boss = true;
        currentState = animator.GetCurrentAnimatorStateInfo(0);
    }

    void Update()
    {
        LookAtPlayer();
        currentState = animator.GetCurrentAnimatorStateInfo(0); // Update currentState here
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BossTakeDamage(15);
        }
    }

    public void shoot()
    {
        Instantiate(stonePrefap, firePoint.position, firePoint.rotation);
    }

    public void lazer()
    {
		RaycastHit2D hitInfo = Physics2D.Raycast(firePoint2.position, -firePoint2.right); // Cast ray in the opposite direction
        Player p = hitInfo.transform.GetComponent<Player>();
        if (p != null)
        {
            Debug.Log("boobs");
            p.TakeDamage(20);
            lineRenderer.SetPosition(0, firePoint2.position);
            lineRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            Debug.Log("boooooobs");
            lineRenderer.SetPosition(0, firePoint2.position);
            lineRenderer.SetPosition(1, firePoint2.position - firePoint2.right * 100); // Extend the line in the opposite direction
        }
    }

    public void BossTakeDamage(int damage)
    {
        if (!currentState.IsName("immune"))
        {
            currHealth -= damage;
            healthBar.SetHealth(currHealth);
            if (currHealth <= 0)
            {
                Die();
            }
        }
    }

    void Die ()
    {
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("death");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    private enum MovementState { Idle, Move, Attack }
    private Rigidbody2D rb;
    public GameObject NPCS1;
    public GameObject NPCS2;
    public GameObject NPCS3;
    public GameObject NPCS4;
    public bool Clone = true;
    private float nextFireTime;
    public float fireRate = 1;
    GameObject Better_Call_NPC;
    Animator boss_animator;
    public Transform player;
    public bool isFlipped = false;
    public int DamageSowrd = 10;
    public int DamageBullet = 2;
    private bool heal = true;
    public AudioSource audioSource;
    public AudioClip laugh,death,hurt,fire;
    public GameObject VictoryPanel;

    // Start is called before the first frame update
    void Start()
    {
        Better_Call_NPC = GameObject.FindWithTag("NPC_EvilBoss").gameObject;
        boss_animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource.clip = laugh;
        audioSource.Play();
    }

    private void FixedUpdate()
    {
        if (heal && HealthBarBoss.Health <= 100)
        {
            boss_animator.SetTrigger("Heal");
            HealthBarBoss.Healing(80);
            heal = false;
        }
        else if (nextFireTime < Time.time && HealthBarBoss.Health <= 120 && Clone)
        {
            Instantiate(NPCS1, Better_Call_NPC.transform.position, Quaternion.identity);
            Instantiate(NPCS2, Better_Call_NPC.transform.position, Quaternion.identity);
            Instantiate(NPCS3, Better_Call_NPC.transform.position, Quaternion.identity);
            Instantiate(NPCS4, Better_Call_NPC.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
            Clone = false;
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
        string nameClip = boss_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        if (collision.gameObject.CompareTag("Player_Sowrd") && HealthBarBoss.Health >= 0 && nameClip != "Idle")
        {
            audioSource.clip = hurt;
            audioSource.Play();
            HealthBarBoss.TakeDamage(DamageSowrd);
            boss_animator.SetTrigger("Take Hit");
            StartCoroutine("Hurt");
            if (HealthBarBoss.Health <= 0)
            {
                boss_animator.ResetTrigger("Take Hit");
                Die();
            }
        }
        else if (collision.gameObject.CompareTag("Player_Bullet") && HealthBarBoss.Health >= 0 && nameClip != "Idle")
        {
            audioSource.clip = hurt;
            audioSource.Play();
            HealthBarBoss.TakeDamage(DamageBullet);
            boss_animator.SetTrigger("Take Hit");
            StartCoroutine("Hurt");
            if (HealthBarBoss.Health <= 0)
            {
                
                boss_animator.ResetTrigger("Take Hit");
                Die();
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
        audioSource.clip = death;
        audioSource.Play();
    }
    public void destroyEnemyObject()
    {
        Destroy(this.gameObject);
    }
    public void appearVictoryPanel()
    {
        VictoryPanel.SetActive(true);
    }
}

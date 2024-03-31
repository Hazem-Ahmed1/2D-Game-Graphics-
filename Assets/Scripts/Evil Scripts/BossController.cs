using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private enum MovementState { Idle, Move, Attack }
    private Rigidbody2D rb;
    private BoxCollider2D BOX;
    public GameObject NPCS1;
    public GameObject NPCS2;
    public GameObject NPCS3;
    public GameObject NPCS4;
    public bool Clone = true;
    private float nextFireTime;
    public float fireRate = 1;
    public GameObject Better_Call_NPC;
    GameObject ChooseColider;
    Animator boss_animator;
    public Transform player;
    public bool isFlipped = false;
    public int Damage = 10;
    private bool heal = true;
    // Start is called before the first frame update
    void Start()
    {
        Better_Call_NPC = GameObject.FindWithTag("NPC").gameObject;
        ChooseColider = GetComponent<GameObject>();
        boss_animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;


        // Get the reference to the child object
         ChooseColider = this.transform.Find("AttackDamage").gameObject;

        // Get the component you want to disable on the child object
         BOX = ChooseColider.GetComponent<BoxCollider2D>();

        // Disable the component
         BOX.enabled = false;
    }

    private void FixedUpdate()
    {
        if (heal && HealthBarBoss.Health <= 35)
        {
            boss_animator.SetTrigger("Heal");
            HealthBarBoss.Healing(25);
            heal = false;

        }

        if (nextFireTime < Time.time && HealthBarBoss.Health <= 50 && Clone)
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
            BOX.enabled = true;
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
        if (collision.gameObject.CompareTag("bullet") && HealthBarBoss.Health >= 0 && nameClip != "Idle")
        {
            HealthBarBoss.TakeDamage(Damage);
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
        // destroyEnemyObject();
    }
    public void destroyEnemyObject()
    {
        Destroy(this.gameObject);
    }

}

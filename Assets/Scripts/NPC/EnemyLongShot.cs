using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyLongShot : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float lineOfSite = 5;
    [SerializeField]
    private float RunRange = 5;
    [SerializeField]
    private float attackRange = 3;
    [SerializeField]
    private int EnemyHealth = 10;
    [SerializeField]
    private Transform Player;
    [SerializeField]
    private GameObject Blood;
    [SerializeField]
    public GameObject Arrow;
    [SerializeField]
    public float fireRate = 1;
    public GameObject ArrowPoint;
    private Animator anim;
    private Rigidbody2D rb;
    private CircleCollider2D Circle;
    private GameObject ChooseColider;
    private enum MovementState {idle, running, attack,SpecialAttack}
    MovementState state = MovementState.idle;
    public bool isFlipped = false;

    // Start is called before the first frame update
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        ArrowPoint = GameObject.FindWithTag("bulletparent").gameObject;
        ChooseColider = this.transform.Find("AttackPoint").gameObject;
        Circle = ChooseColider.GetComponent<CircleCollider2D>();
        Physics2D.IgnoreLayerCollision(7,7);
        speed = 1;
        Circle.enabled = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float distanceFromPlayer = Vector2.Distance(Player.position, this.transform.position);
        if(distanceFromPlayer <= RunRange && distanceFromPlayer >= attackRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, Player.position, speed* Time.deltaTime);
        }
        UpdateAnimationState(distanceFromPlayer , EnemyHealth);
    }
    private void UpdateAnimationState(float distanceFromPlayer, int EnemyHealth)
    {
        if(distanceFromPlayer <= lineOfSite && distanceFromPlayer > RunRange && EnemyHealth > 0)
        {
            state = MovementState.attack;
            Circle.enabled = false;
        }
        else if(distanceFromPlayer <= RunRange && distanceFromPlayer > attackRange && EnemyHealth > 0)// && Player.position.x > this.transform.position.x
        {
            LookAtPlayer();
            state = MovementState.running;
            Circle.enabled = false;
        }
        else if(distanceFromPlayer <= attackRange && EnemyHealth > 0)
        {
            state = MovementState.SpecialAttack;
            Circle.enabled = true;
        }
        else
        {
            state = MovementState.idle;
            Circle.enabled = false;
        }
        anim.SetInteger("state", (int)state);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Bullet") && EnemyHealth >= 0)
        {
            TakeDamage(1);
            if (EnemyHealth <= 0)
            {
                anim.ResetTrigger("hurt");
                Die();
            }
        }
    }
    public void createArrowShot()
    {
            Instantiate(Arrow, ArrowPoint.transform.position, Quaternion.identity);
    }
    private void TakeDamage(int damage)
    {
        Instantiate(Blood, this.transform.position,Quaternion.identity);
        EnemyHealth = EnemyHealth - damage;
        anim.SetTrigger("hurt");
        Debug.Log(EnemyHealth);
        StartCoroutine("Hurt");
    }
    IEnumerator Hurt()
    {
        yield return new WaitForSeconds(0.5f);
    }
    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
    }
    public void destroyEnemyObject()
    {
        Destroy(this.gameObject);
    }
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > Player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
        else if (transform.position.x < Player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, lineOfSite);
        Gizmos.DrawWireSphere(this.transform.position, attackRange);
        Gizmos.DrawWireSphere(this.transform.position, RunRange);
    }
}
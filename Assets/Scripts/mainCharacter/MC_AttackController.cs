using UnityEngine;

public class MC_AttackController : MonoBehaviour
{
    public float comboTime = 0.5f;
    private float lastAttackTime;
    private int comboCount;
    private Animator animator;
    private bool isAttacking;
    [Header("Damage")]
    [SerializeField] private int enemyDamage = 15;
    public Enemies enemies;

    void Start()
    {
        lastAttackTime = -comboTime;
        comboCount = 0;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        isAttacking = false;
        if (Input.GetMouseButtonDown(0))
        {
            float timeSinceLastAttack = Time.time - lastAttackTime;

            if (timeSinceLastAttack > comboTime)
            {
                comboCount = 0;
            }

            comboCount++;
            lastAttackTime = Time.time;

            if (comboCount % 2 == 1)
            {
                animator.SetTrigger("Attack1");
                isAttacking = true;
                // Send a Damage to the enemy

            }
            else
            {
                animator.SetTrigger("Attack2");
                isAttacking = true;
                // Send a Damage to the enemy
            }

            if (comboCount > 2)
            {
                comboCount = 1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAttacking && collision.gameObject.tag == "enemy")
        {
            //enemies.TakeDamage(enemyDamage);
        }
    }
}

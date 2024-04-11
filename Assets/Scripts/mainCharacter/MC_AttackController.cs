using UnityEngine;

public class MC_AttackController : MonoBehaviour
{
    public float comboTime = 0.5f;
    private float lastAttackTime;
    private int comboCount;
    private Animator animator;
    [SerializeField] AudioSource Sword1;
    [SerializeField] AudioSource Sword2;


    void Start()
    {
        lastAttackTime = -comboTime;
        comboCount = 0;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
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
                Sword1.Play();

            }
            else
            {
                animator.SetTrigger("Attack2");
                Sword2.Play();
            }

            if (comboCount > 2)
            {
                comboCount = 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {

            animator.SetTrigger("isShoot");
        }
    }

}

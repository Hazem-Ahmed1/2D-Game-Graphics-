using UnityEngine;

public class MC_AttackController : MonoBehaviour
{
    public float comboTime = 0.5f;
    private float lastAttackTime;
    private int comboCount;
    private Animator animator;
    //public AudioSource audioSource;
    //public AudioClip Pistol,Sword;

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
            //audioSource.clip = Sword;
            //audioSource.Play();
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
                //audioSource.Play();
            }
            else
            {
                animator.SetTrigger("Attack2");
            }

            if (comboCount > 2)
            {
                comboCount = 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            //audioSource.clip = Pistol;
            //audioSource.Play();
            animator.SetTrigger("isShoot");
        }
    }

}

using UnityEngine;
using UnityEngine.InputSystem;

public class MC_AttackController : MonoBehaviour
{
    public float comboTime = 0.5f;
    private float lastAttackTime;
    private int comboCount;
    private Animator animator;
    [SerializeField] AudioSource Sword1;
    [SerializeField] AudioSource Sword2;
    public PlayerInput playerControls;
    private InputAction slash;
    private InputAction fire;


    void Awake()
    {
        playerControls = new PlayerInput();
    }

    void OnEnable()
    {
        slash = playerControls.Player.Slash;
        fire = playerControls.Player.Fire;

        slash.Enable();
        fire.Enable();
    }

    void OnDisable()
    {
        slash.Disable();
        fire.Disable();
    }

    void Start()
    {
        lastAttackTime = -comboTime;
        comboCount = 0;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (slash.IsPressed())
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
        else if (fire.IsPressed())
        {

            animator.SetTrigger("isShoot");
        }
    }

}

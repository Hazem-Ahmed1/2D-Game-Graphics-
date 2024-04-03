using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossMove : StateMachineBehaviour
{
    private Rigidbody2D rb;
    Transform player;
    private BoxCollider2D BOX;
    GameObject ChooseColider;
    BossController bossmove;
    public float speed = 5.0f;
    public float StandTime = 5;
    public float attackRange = 3f;
    public float LongShot = 5f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        bossmove = animator.GetComponent<BossController>();

        // Get the reference to the child object
        ChooseColider = animator.transform.Find("AttackDamage").gameObject;

        // Get the component you want to disable on the child object
        BOX = ChooseColider.GetComponent<BoxCollider2D>();

        // Disable the component
        BOX.enabled = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            BOX.enabled = true;
            animator.SetTrigger("Attack");
        }
        bossmove.LookAtPlayer();

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        BOX.enabled = false;
    }



}

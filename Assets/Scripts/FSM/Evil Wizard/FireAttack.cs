using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttack : StateMachineBehaviour
{
    
    public GameObject bullet;
    public GameObject BulletParent;
    private float fireRate = 0.5f;
    private float nextFireTime;
    BossController bossmove;
    private Rigidbody2D rb;
    Transform player;
    public float attackRange = 3f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        BulletParent = GameObject.FindWithTag("bulletparent").gameObject;
        bossmove = animator.GetComponent<BossController>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


        if (nextFireTime < Time.time)
        {
            Instantiate(bullet, BulletParent.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
         
        bossmove.LookAtPlayer();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this.rb.bodyType = RigidbodyType2D.Static;
    }


}

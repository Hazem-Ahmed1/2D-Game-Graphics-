using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_run : StateMachineBehaviour
{
    Transform player;
    public float speed = 2f;
    public bool isFlipped = false;
    Rigidbody2D rb;
    //Boss_s boss;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       player = GameObject.FindGameObjectWithTag("pip").transform;
       rb = animator.GetComponent<Rigidbody2D>();
       //boss = animator.GetComponent<Boss_s>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        animator.GetComponent<Boss_s>
        Vector2 target = new Vector2(player.position.x,rb.position.y);
        Vector2 newpos = Vector2.MoveTowards(rb.position,target,speed *Time.fixedDeltaTime );
        rb.MovePosition(newpos);
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
    
}

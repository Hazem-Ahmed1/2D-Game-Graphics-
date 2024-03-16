using UnityEngine;
public class BossRun : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D _rb;
    public Boss boss;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       player = GameObject.FindGameObjectWithTag("Player").transform;
       _rb = animator.GetComponent<Rigidbody2D>();
       boss = animator.GetComponent<Boss>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        if (Vector2.Distance(player.position, _rb.position) <= boss._atkRange)
        {
            animator.Play("attack");
        }
        else if (Vector2.Distance(player.position, _rb.position) <= boss._lookRange)
		{
			animator.Play("walk");
            boss.LookAtPlayer();
            Vector2 target = new(player.position.x,_rb.position.y);
            Vector2 newpos = Vector2.MoveTowards(_rb.position, target, boss._speed * Time.fixedDeltaTime );
            _rb.MovePosition(newpos);  
		}
        else animator.Play("idle");   
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}

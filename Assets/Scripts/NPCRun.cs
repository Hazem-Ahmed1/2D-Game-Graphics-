using UnityEngine;
public class NPCRun : StateMachineBehaviour
{
    private Transform player;
    private Rigidbody2D rb;
    public NPC npc;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       player = GameObject.FindGameObjectWithTag("Player").transform;
       rb = animator.GetComponent<Rigidbody2D>();
       npc = animator.GetComponent<NPC>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        Debug.Log(npc.attributes);
        if (Vector2.Distance(player.position, rb.position) <= npc.attributes.atkRange)
        {
            animator.Play("attack");
        }
        else if (Vector2.Distance(player.position, rb.position) <= npc.attributes.lookRange)
		{
			animator.Play("walk");
            npc.LookAtPlayer();
            Vector2 target = new(player.position.x,rb.position.y);
            Vector2 newpos = Vector2.MoveTowards(rb.position, target, npc.attributes.speed * Time.fixedDeltaTime);
            rb.MovePosition(newpos);  
		}
        else animator.Play("idle");   
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}

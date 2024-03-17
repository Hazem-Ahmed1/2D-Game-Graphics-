using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMove : StateMachineBehaviour
{
    private Transform player;
    private Rigidbody2D rb;
    public NPC npc;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        npc = animator.GetComponent<NPC>();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        npc.LookAtPlayer();
        Vector2 target = new(player.position.x,rb.position.y);
        Vector2 newpos = Vector2.MoveTowards(rb.position, target, npc.attributes.speed * Time.fixedDeltaTime);
        rb.MovePosition(newpos);
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

}

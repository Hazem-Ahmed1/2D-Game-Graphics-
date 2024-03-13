using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public float speed;
    public float lineOfSite = 5;
    public float attackRange = 3;
    private float nextAttackTime;
    public float attackRate = 1;
    private BoxCollider2D BC;
    public float sizeBC = 0;
    // private float dirX = 0f;
    private Transform Player;
    private Animator anim;
    private enum MovementState {idle, running, attack}
    private MovementState state = MovementState.idle;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        BC = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(Player.position, this.transform.position);
        if(distanceFromPlayer < lineOfSite && distanceFromPlayer > attackRange)
        {
            anim.SetInteger("state", 1);
            transform.position = Vector2.MoveTowards(this.transform.position, Player.position, speed* Time.deltaTime);
        }
        else if(distanceFromPlayer <= attackRange && nextAttackTime < Time.time)
        {
            anim.SetInteger("state", 2);
            // BC.size = new Vector2(BC.size.x, BC.size.y + sizeBC);
        }
        else if(distanceFromPlayer > lineOfSite)
        {
            anim.SetInteger("state", 0);
        }

    }

    // private void UpdateAnimationState()
    // {
        
    // }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, lineOfSite);
        Gizmos.DrawWireSphere(this.transform.position, attackRange);
    }
}

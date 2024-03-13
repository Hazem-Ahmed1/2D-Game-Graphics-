using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public float speed;
    public float lineOfSite = 5;
    public float attackRange = 3;
    // private float nextAttackTime;
    [SerializeField]
    private Transform Player;
    private Animator anim;
    private SpriteRenderer sr;
    private enum MovementState {idle, running, attack}
    
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(Player.position, this.transform.position);
        if(distanceFromPlayer < lineOfSite && distanceFromPlayer > attackRange - 1)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, Player.position, speed* Time.deltaTime);
        }
        UpdateAnimationState(distanceFromPlayer);
    }

    private void UpdateAnimationState(float distanceFromPlayer)
    {
        MovementState state;
        if(distanceFromPlayer < lineOfSite && distanceFromPlayer > attackRange && Player.position.x > this.transform.position.x)
        {
            state = MovementState.running;
            sr.flipX = false;
        }
        else if(distanceFromPlayer < lineOfSite && distanceFromPlayer > attackRange && Player.position.x < this.transform.position.x)
        {
            state = MovementState.running;
            sr.flipX = true;
        }
        else if(distanceFromPlayer <= attackRange)
        {
            state = MovementState.attack;
        }
        else
        {
            state = MovementState.idle;
        }
        anim.SetInteger("state", (int)state);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, lineOfSite);
        Gizmos.DrawWireSphere(this.transform.position, attackRange);
    }
}

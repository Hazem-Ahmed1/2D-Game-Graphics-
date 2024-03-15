using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovememt : MonoBehaviour
{
    private enum MovementState { Idle, Move, Attack }
    private SpriteRenderer SR;
    Animator boss_animator;
    public float lineOfSite = 5;
    public float attackRange = 3;
    public float speed = 5.0f;
    public float jumpForce = 10.0f;
    public Transform Player;
    // Start is called before the first frame update
    void Start()
    {
        boss_animator = GetComponent<Animator>();
        SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {


        float distanceFromPlayer = Vector2.Distance(Player.position, this.transform.position);
        if (distanceFromPlayer < lineOfSite && distanceFromPlayer > attackRange - 1)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, Player.position, speed * Time.deltaTime);
        }
        UpdateAnimationState(distanceFromPlayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, lineOfSite);
        Gizmos.DrawWireSphere(this.transform.position, attackRange);
    }

    private void UpdateAnimationState(float Distance)
    {
        MovementState state;
        if (Distance < lineOfSite && Distance > attackRange && Player.position.x > this.transform.position.x)
        {
            state = MovementState.Move;
            SR.flipX = false;
        }
        else if (Distance < lineOfSite && Distance > attackRange && Player.position.x < this.transform.position.x)
        {
            state = MovementState.Move;
            SR.flipX = true;
        }
        else if (Distance <= attackRange)
        {
            state = MovementState.Attack;
        }
        else
        {
            state = MovementState.Idle;
        }
        boss_animator.SetInteger("State", (int)state);

    }
}

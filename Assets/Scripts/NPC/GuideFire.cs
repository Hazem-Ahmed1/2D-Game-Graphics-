using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GuideFire : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float lineOfSite;
    Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private Transform Target;
    private enum MovementState {idle, walk}
    MovementState state = MovementState.idle;
    // Start is called before the first frame update
    private void Start()
    {
        Target = GameObject.FindGameObjectWithTag("LightingPoint").transform;
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        lineOfSite = 2f;

    }
    private void Update()
    {
        float distanceFromPlayer = Vector2.Distance(Target.position, this.transform.position);
        if(distanceFromPlayer > lineOfSite)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, Target.position, speed* Time.deltaTime);
        }
        UpdateAnimationState(distanceFromPlayer);
    }
    private void UpdateAnimationState(float distanceFromPlayer)
    {
        if(Target.position.x > this.transform.position.x && distanceFromPlayer > lineOfSite)
        {
            state = MovementState.walk;
            sr.flipX = false;
        }
        else if(Target.position.x < this.transform.position.x && distanceFromPlayer > lineOfSite)
        {
            state = MovementState.walk;
            sr.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }
        anim.SetInteger("state", (int)state);
    }
    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
    }
    public void destroyEnemyObject()
    {
        Destroy(this.gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, lineOfSite);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageShootingPlayer : MonoBehaviour //not used yet
{
    [SerializeField] private Transform player;
    private bool grounded;
    public float lineOfSite;
    //mageMovement mageMovement;
    public Animator anim;
    public bool Charged = false;


    private void Start()
    {
        anim =  GetComponent<Animator>();
    }
    
    
    // Update is called once per frame
    void Update()
    {
        anim.SetBool("grounded", grounded);

        float distanceFromThePlayer = Vector2.Distance(player.position, transform.position);
        attackPlayer(distanceFromThePlayer);

       
    }

    void attackPlayer(float distanceFromThePlayer)
    {
        if (distanceFromThePlayer < lineOfSite)
        {
           anim.SetTrigger("light-charge");
           Charged = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
            grounded = true;
    }

}

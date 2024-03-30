using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageShootingPlayer : MonoBehaviour //not used yet
{
    private Transform player;
    public float linOfSite;
    mageMovement mageMovement;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("player").transform;
    }
    
    
    // Update is called once per frame
    void Update()
    {
        float distanceFromThePlayer = Vector2.Distance(player.position, transform.position);
        attackPlayer(distanceFromThePlayer);
       
    }

    void attackPlayer(float distanceFromThePlayer)
    {
        if (distanceFromThePlayer < linOfSite)
        {
            mageMovement.anim.SetTrigger("light-charge");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, linOfSite);
    }
}

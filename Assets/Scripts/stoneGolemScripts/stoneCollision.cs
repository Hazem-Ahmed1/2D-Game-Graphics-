using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stoneCollision : MonoBehaviour
{
    public GameObject impact;
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.TakeDamage(15);
            effect();
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Ground")
        {   
            effect();
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "StoneGolem")
        {
            StoneNPC stone = collision.gameObject.GetComponent<StoneNPC>();
            stone.BossTakeDamage(15);
            effect();
            Destroy(gameObject);
        }
        
            
    }

    private void effect()
    {
        Instantiate(impact,transform.position,transform.rotation);
    }
}

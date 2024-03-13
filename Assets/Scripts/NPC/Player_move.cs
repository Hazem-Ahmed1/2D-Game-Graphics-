using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    private SpriteRenderer mySprite;
    public int playerHealth = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetAxis("Horizontal") > 0)
        // {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                // transform.position =new Vector3(transform.position.x + 2 , transform.position.y, transform.position.z);
                transform.Translate(new Vector3(-0.07f,0,0));
            }
            else if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                // mySprite.flipX = false;
                transform.Translate(new Vector3(0.07f,0,0));
            } 
            // if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            // {
            //     // transform.position =new Vector3(transform.position.x + 2 , transform.position.y, transform.position.z);
            //     transform.Translate(new Vector3(0,0.07f,0));
            // }
            // else if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            // {
            //     // mySprite.flipX = false;
            //     transform.Translate(new Vector3(0,-0.07f,0));
            // } 
        // }
    }

    // take damage
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerHealth--;
            Debug.Log(playerHealth);
        }

        if (playerHealth == 0)
        {
            Debug.Log("Game Over");
        }
    }
}

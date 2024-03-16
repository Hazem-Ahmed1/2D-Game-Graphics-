using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_s : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rg;
    private bool facingRight = true;
    [SerializeField]public float speed = 100f;
    [SerializeField]public float jumpvalue = 100;
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {   
        float lr = Input.GetAxisRaw("Horizontal");
        rg.velocity = new Vector2(speed*lr,rg.velocity.y);
        if(Input.GetButtonDown("Jump")){
            rg.velocity = new Vector2(rg.velocity.x,jumpvalue);
        }

        if(facingRight && (lr < 0)){
            filp();
        }
        else if (!facingRight && (lr > 0)){
            filp();
        }
    }
    void filp(){
        Vector3 currentSclae = gameObject.transform.localScale;
        currentSclae.x *= -1;
        gameObject.transform.localScale = currentSclae;
        facingRight = !facingRight;
    }
}

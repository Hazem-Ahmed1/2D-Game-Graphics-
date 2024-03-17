using UnityEngine;

public class NPC : MonoBehaviour
{
    private Transform player;
    private bool isFlipped = false;
    public NPCAttributes attributes;
    public int currHealth;



    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currHealth = attributes.healthPoints;
    }

    void Update()
    {   
        // LookAtPlayer();
    }

    void TakeDamage(int damage)
    {
        currHealth -= damage;
        if(currHealth <= 0)
        {
            Destroy(gameObject);
        }
    }


    public void LookAtPlayer()
    {
        if ((transform.position.x > player.position.x && isFlipped) || 
            (transform.position.x < player.position.x && !isFlipped))
        {
            transform.Rotate(0f, 180f, 0f);
            isFlipped = !isFlipped;
        }
    }






}

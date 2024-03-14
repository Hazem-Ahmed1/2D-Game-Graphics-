using UnityEngine;

public class Boss : MonoBehaviour
{
    private Transform player;
    private bool _isFlipped = false;
    private readonly int maxHealth = 100;
    public int _currHealth;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {   
        // LookAtPlayer();
    }

    void TakeDamage(int damage)
    {
        _currHealth -= damage;
    }


    public void LookAtPlayer()
    {
        if ((transform.position.x > player.position.x && _isFlipped) || 
            (transform.position.x < player.position.x && !_isFlipped))
        {
            transform.Rotate(0f, 180f, 0f);
            _isFlipped = !_isFlipped;
        }
    }
}

using UnityEngine;

public class NPC : MonoBehaviour
{
    private Transform _player;
    private bool _isFlipped = false;
    public NPCAttributes attributes;
    public int _currHealth;

    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
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
        if ((transform.position.x > _player.position.x && _isFlipped) || 
            (transform.position.x < _player.position.x && !_isFlipped))
        {
            transform.Rotate(0f, 180f, 0f);
            _isFlipped = !_isFlipped;
        }
    }
}

using UnityEngine;

public class Boss : MonoBehaviour
{
    private Transform _player;
    private bool _isFlipped = false;
    private readonly int maxHealth = 100;
    public int _currHealth;
    public float _speed = 2f;
    public float _lookRange = 10f;
    public float _atkRange = 1.5f;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _currHealth = maxHealth;
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

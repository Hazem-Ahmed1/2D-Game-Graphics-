using UnityEngine;

public class StoneHit : MonoBehaviour
{
    public GameObject impact;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.TakeDamage(15);  
        }
        else if(collision.gameObject.CompareTag("StoneGolem"))
        {
            StoneNPC stone = collision.gameObject.GetComponent<StoneNPC>();
            stone.BossTakeDamage(15);
        }
        Effect();
        Destroy(gameObject);
            
    }

    private void Effect()
    {
        Destroy(Instantiate(impact,transform.position,transform.rotation),0.5f);
    }
}

using UnityEngine;

public class StoneHit : MonoBehaviour
{
    public GameObject impact;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Effect();
            Destroy(this.gameObject);
            Player player = collision.gameObject.GetComponent<Player>();
            player.TakeDamage(15);
        }
        else if(collision.gameObject.CompareTag("ground") || collision.gameObject.CompareTag("oneWayPlatform"))
        {
            Effect();
            Destroy(gameObject);
        }
    }

    private void Effect()
    {
        Destroy(Instantiate(impact,transform.position,transform.rotation),0.5f);
    }
}

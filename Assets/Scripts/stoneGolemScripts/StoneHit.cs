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
            MC_Health player = collision.gameObject.GetComponent<MC_Health>();
            player.TakeDamage(10);
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

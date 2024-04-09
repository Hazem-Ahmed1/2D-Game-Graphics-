using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gems : MonoBehaviour
{
    [SerializeField]
    private GameObject Light;
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(8,10);
    }
    void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(Light, this.transform.position,Quaternion.identity);
            Destroy(this.gameObject);
            ScoreManager.instance.IncrementScore(50);
        }
    }
}

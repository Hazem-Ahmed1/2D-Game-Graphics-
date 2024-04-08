using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coins : MonoBehaviour
{
    [SerializeField]
    private GameObject Light;
    void Start()
    {
        Physics2D.IgnoreLayerCollision(8,12);
    }
    void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(Light, this.transform.position,Quaternion.identity);
            Destroy(this.gameObject);
            ScoreManager.instance.IncrementScore(20);
        }
    }
}

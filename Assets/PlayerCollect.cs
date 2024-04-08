using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerCollect : MonoBehaviour
{
    private int Score;
    private int Health;
    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        Health = 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("gems"))
        {
            Score += 30;
        }
        else if (collision.gameObject.CompareTag("Coin"))
        {
            Score += 5;
        }
        else if (collision.gameObject.CompareTag("treasure"))
        {
            Score += 45;
        }
        else if (collision.gameObject.CompareTag("Health"))
        {
            Health += 20;
        }
    }
}

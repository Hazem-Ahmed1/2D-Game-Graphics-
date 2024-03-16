using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace SamuraiBoss
{
    public class Boss : MonoBehaviour
    {
        public Transform player;
	    public bool isFlipped = false;
        public int maxHealth = 100;
        public int currentHealth;
        public SamuraiBoss.health_bar healthBar;

        void Start()
        {
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }

        void Update()
        {   
            //GetComponent<Animator>().SetBool("hurt",false);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TakeDamage(20);
                GetComponent<Animator>().SetBool("hurt",true);
            }
            
        }

        void TakeDamage(int damage)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
        }


        public void LookAtPlayer()
        {
            Vector3 flipped = transform.localScale;
            flipped.z *= -1f;

            if (transform.position.x > player.position.x && isFlipped)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                isFlipped = false;
            }
            else if (transform.position.x < player.position.x && !isFlipped)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                isFlipped = true;
            }
        }
    }
}
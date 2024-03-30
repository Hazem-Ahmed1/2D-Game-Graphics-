using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image health_bar;
    float max_health = 100f;
    public static float Health;

    // Start is called before the first frame update
    void Start()
    {
        health_bar = GetComponent<Image>();
        Health = max_health;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            TakeDamage(20);
        }
/*        health_bar.fillAmount = Health/max_health;
*/        
    }
    public void TakeDamage(float Damage)
    {
        Health -= Damage;
        health_bar.fillAmount = Health / max_health;


    }
}

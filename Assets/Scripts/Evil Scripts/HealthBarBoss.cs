using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBoss : MonoBehaviour
{
    Animator animator;
    public static Image healthBar;
    public static float Health;
    public static bool Boss_heal = true;
    public static float max_Health = 100f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        healthBar = GetComponent<Image>();
        Health = max_Health;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public static void TakeDamage(float damage)
    {
        Health -= damage;
        healthBar.fillAmount = Health / max_Health;
    }
    public static void Healing(float healingAmount)
    {
        Health += healingAmount;
        Health = Mathf.Clamp(Health, 0, max_Health);

        healthBar.fillAmount = Health / max_Health;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MC_HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private GameObject filed;
    public void UpdateHealthBar(float currentValue , float maxValue)
    {
        slider.value = currentValue / maxValue;

        if (currentValue <= 0)
        {
            filed.SetActive(false);
        }
    }
    
}

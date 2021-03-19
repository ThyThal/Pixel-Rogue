using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image image;

    [SerializeField] public GameObject pDamage;
    [SerializeField] public GameObject pSpeed;
    [SerializeField] public GameObject pShield;
    
    public void MaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        image.color = gradient.Evaluate(1f);
    }
    
    public void SetHealth(int health)
    {
        slider.value = health;
        image.color = gradient.Evaluate(slider.normalizedValue);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    //public Image damagedFill;

    public Slider damageSlider;
    public Image damageFill;
    private const float FADE_TIMER_MAX = 1f;
    private float fadeTimer;
    private Color damagedColor;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        if (damageSlider != null && damageFill != null)
        {
            damageSlider.maxValue = health;
            damageSlider.value = health;
            damagedColor = damageFill.color;
            damagedColor.a = 0f;
            damageFill.color = damagedColor;
        }

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetDamagedHealth(int currentHealth, int damage)
    {
        slider.value = currentHealth - damage;
        damageSlider.value = currentHealth;
        damagedColor.a = 1f;
        damageFill.color = damagedColor;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private void Start()
    {
        HealthSystem healthComponent = GetComponent<HealthSystem>();
        if (healthComponent != null)
        {
            healthComponent.OnHealthChanged += UpdateHealthBar;
        }
    }

    private void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        float healthPercentage = currentHealth / maxHealth;
        // Update health bar UI based on the current health percentage
        Debug.Log("Health Bar Updated: " + healthPercentage);
    }
}

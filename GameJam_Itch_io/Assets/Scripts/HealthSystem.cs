using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private CurrentEnemies currentEnemies;

    void Start()
    {
        currentHealth = maxHealth;
        currentEnemies = FindObjectOfType<CurrentEnemies>();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        currentEnemies.RemoveEnemy(this.gameObject);
        Destroy(gameObject);
    }
}

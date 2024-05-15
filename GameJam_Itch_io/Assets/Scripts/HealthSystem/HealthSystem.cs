using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : Ab_HealthManager
{
    public override void Start()
    {
        base.Start();
    }

    public override void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
    }

    public override void Kill()
    {
        //Put logic for playing death animation and anything else deemed necessary here
        SpawnEnemies spawnEnemies = FindObjectOfType<SpawnEnemies>();
        spawnEnemies.RemoveEnemy(this.gameObject);
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : Ab_HealthManager
{
    private HealthBar _healthBar;

    public override void Start()
    {
        base.Start();
        _healthBar = GetComponentInChildren<HealthBar>();
    }

    public override void TakeDamage(float amount)
    {
        CurrentHealth -= Mathf.RoundToInt(amount);
        _healthBar.UpdateHealthBar(CurrentHealth, maxHealth);
    }

    public void HealthUpdate()
    {
        _healthBar.UpdateHealthBar(CurrentHealth, maxHealth);
    }

    public override void Kill()
    {
        //Put logic for playing death animation and anything else deemed necessary here
        StartCoroutine(WaitWithRemoving());
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    //Wait a couple of seconds before removing the enemy so the death animation can play
    private IEnumerator WaitWithRemoving()
    {
        yield return new WaitForSeconds(2f);
        SpawnEnemies spawnEnemies = FindObjectOfType<SpawnEnemies>();
        spawnEnemies.RemoveEnemy(this.gameObject);
    }
}

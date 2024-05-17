using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Ab_Enemy
{
    public enum TankState
    {
        Attacking,
        Shielding,
        Taunting,
        Waiting
    }

    [SerializeField, Range(3, 60)] private int damageDealt; //Update in prefab if you change the value(s)

    private TankState state;

    private PlayerStats playerStats;

    private List<GameObject> targetList = new List<GameObject>();

    protected override void Start()
    {
        base.Start();

        playerStats = FindFirstObjectByType<PlayerStats>();
    }

    public override void OnAction()
    {
        RandomizeAction();
    }

    private void RandomizeAction()
    {
        float randomActionNumber = UnityEngine.Random.value;
        if (randomActionNumber < 0.5f)
        {
            Attack();
        }

        else if (randomActionNumber < 0.8333f) // 0.5 + (1/6)
        {
            RandomizeShielding();
        }

        else if (randomActionNumber < 1.1666f) // 0.5 + (2/6)
        {
            Taunt();
        }
    }

    private void Attack()
    {
        state = TankState.Attacking;
        playerStats.TakeDamage(damageDealt);
        UnityEngine.Debug.Log("Should attack player and then end turn");
        state = TankState.Waiting;
    }

    private void Taunt()
    {
        state = TankState.Taunting;
        UnityEngine.Debug.Log("Taunts... Player can only attack tank next turn!");
        state = TankState.Waiting;
    }

    private void RandomizeShielding()
    {
        int index = UnityEngine.Random.Range(0, 2);

        switch (index)
        {
            case 0:
                ShieldSelf();
                break;

            case 1:
                ShieldEnemy();
                break;
        }
    }

    private void ShieldSelf()
    {
        state = TankState.Shielding;
        UnityEngine.Debug.Log(this.gameObject.name + " puts a shield on itself");
        state = TankState.Shielding;
    }

    private void ShieldEnemy()
    {
        state = TankState.Shielding;
        LookForOtherEnemies();
        state = TankState.Shielding;
    }

    private void LookForOtherEnemies()
    {
        for (int i = 0; i < _spawnEnemies.spawnedEnemies.Count; i++)
        {
            HealthSystem enemyHealth = _spawnEnemies.spawnedEnemies[i].GetComponent<HealthSystem>();

            if (enemyHealth.CurrentHealth > 0 && enemyHealth.CurrentHealth < enemyHealth.GetMaxHealth())
            {
                targetList.Add(_spawnEnemies.spawnedEnemies[i]);
            }
        }

        if (targetList.Count > 0)
        {

            float currentLowesthealth = 0f;
            GameObject shieldTarget = null; //temporary storage
            GameObject currentShieldTarget = null;// The actual target who we will put a shield on
            for (int i = 0; i < targetList.Count; i++)
            {
                float lowestHealth = targetList[i].GetComponent<HealthSystem>().CurrentHealth;
                shieldTarget = targetList[i];
                //Sets currentlowest equal to lowesthealth in the first loop, and after that compares every value after it to the current lowest health
                if (i == 0)
                {
                    currentLowesthealth = lowestHealth;
                    currentShieldTarget = shieldTarget;
                }

                //replaces current shielding target if new target has less health than the last one
                if (lowestHealth < currentLowesthealth)
                {
                    currentLowesthealth = lowestHealth;
                    currentShieldTarget = shieldTarget;
                }

            }
            //healTargetMaxHealth = currentHealTarget.GetComponent<HealthSystem>().GetMaxHealth();
            //currentHealTarget.GetComponent<HealthSystem>().Heal(healTargetMaxHealth * healPercentage); //Heals target's health based on % of maxHp
            UnityEngine.Debug.Log(this.gameObject.name + " puts a shield on " + currentShieldTarget);
        }
        if (targetList.Count == 0)
        {
            RandomizeAction();
        }

        targetList.Clear(); // Clears list after turn is over to prevent potentially dead enemies from remaining in the list
    }
}

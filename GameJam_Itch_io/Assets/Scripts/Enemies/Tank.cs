using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Ab_Enemy
{
    [SerializeField, Range(3, 60)] private int damageDealt; //Update in prefab if you change the value(s)

    private List<GameObject> targetList = new List<GameObject>();

    protected override void Start()
    {
        base.Start();
    }

    public override void OnAction()
    {
        base.OnAction();

        RandomizeAction();
    }

    private void RandomizeAction()
    {
        float randomActionNumber = UnityEngine.Random.value;
        if (randomActionNumber < 0.5f)
        {
            Attack();
        }

        else if (randomActionNumber < 0.8f)
        {
            RandomizeShielding();
        }

        else if (randomActionNumber < 1)
        {
            Taunt();
        }
    }

    private void Attack()
    {
        _playerStats.TakeDamage(damageDealt);
        UnityEngine.Debug.Log("Should attack player and then end turn");
    }

    private void Taunt()
    {
        if (ReferenceInstance.refInstance.clickManager.HasSelectedEnemy() == true)
        {
            RandomizeAction();
            return;
        }
        ReferenceInstance.refInstance.clickManager.Taunt(this.gameObject);
        UnityEngine.Debug.Log("Taunts... Player can only attack tank next turn!");
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
        if (_healthSystem.GetComponent<HealthSystem>().hasShield == true)
        {
            RandomizeAction();
        }
        else
        {
            UnityEngine.Debug.Log(this.gameObject.name + " puts a shield on itself");
            _healthSystem.GetComponent<HealthSystem>().hasShield = true;
        }
    }

    private void ShieldEnemy()
    {
        LookForOtherEnemies();
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
            if (currentShieldTarget.GetComponent<HealthSystem>().hasShield == true)
            {
                RandomizeAction();
            }
            else
            {
                currentShieldTarget.GetComponent<HealthSystem>().hasShield = true;
                UnityEngine.Debug.Log(this.gameObject.name + " puts a shield on " + currentShieldTarget);
            }
        }
        if (targetList.Count == 0)
        {
            RandomizeAction();
        }

        targetList.Clear(); // Clears list after turn is over to prevent potentially dead enemies from remaining in the list
    }
}

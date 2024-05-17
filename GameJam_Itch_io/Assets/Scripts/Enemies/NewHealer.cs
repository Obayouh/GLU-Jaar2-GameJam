using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class NewHealer : Ab_Enemy
{
    [SerializeField, Range(0.1f, 1f)] private float healPercentage;
    [SerializeField, Range(3, 60)] private int damageDealt; //Update in prefab if you change the value(s)

    private PlayerStats _playerStats;
    public enum HealerState
    {
        Healing,
        Buffing,
        Attacking,
        Waiting
    }

    HealerState healerState;

    private float halfHealth;
    private float healTargetMaxHealth;

    private List<GameObject> targetList = new List<GameObject>();
    protected override void Start()
    {
        base.Start();
        //Array arrayElement = Enum.GetValues(typeof(Typing));
        //Typing randomType = (Typing)arrayElement.GetValue(UnityEngine.Random.Range(0, arrayElement.Length));
        _playerStats = _player.GetComponent<PlayerStats>();

    }

    void Update()
    {
        //if (healerState == HealerState.Healing)
        //{

        //}

        //if (healerState == HealerState.Buffing)
        //{

        //}

        //if (healerState == HealerState.Attacking)
        //{

        //}

        if (healerState == HealerState.Waiting)
        {
            //End own turn
        }
    }

    public override void OnAction()
    {
        RandomizeAction();
    }

    private void HealEnemy()
    {
        healerState = HealerState.Healing;
        LookForOtherEnemies();
        healerState = HealerState.Waiting;
    }

    private void HealSelf()
    {
        healerState = HealerState.Healing;
        _healthSystem.Heal(_healthSystem.CurrentHealth * healPercentage);
        UnityEngine.Debug.Log(this.gameObject.name + "healed itself");
        healerState = HealerState.Waiting;
    }

    private void BuffOthers()
    {
        healerState = HealerState.Buffing;
        UnityEngine.Debug.Log("Should buff an enemy and then end turn");
        //logic for buffing others
        healerState = HealerState.Waiting;
    }

    private void Attack()
    {
        healerState = HealerState.Attacking;
        _playerStats.TakeDamage(damageDealt);
        UnityEngine.Debug.Log("Should attack player and then end turn");
        healerState = HealerState.Waiting;
    }

    public override void SpawnAnim()
    {
        //Play intro animation
    }

    public override void DeadAnim()
    {
        //Play Death animation
    }

    /// <summary>
    /// Cycles through list of current enemies that are alive, checks which has the lowest health, and then heals that enemy by a set amount
    /// </summary>
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
            GameObject healTarget = null; //temporary storage
            GameObject currentHealTarget = null;// The actual target who we will be healing
            for (int i = 0; i < targetList.Count; i++)
            {
                float lowestHealth = targetList[i].GetComponent<HealthSystem>().CurrentHealth;
                healTarget = targetList[i];
                //Sets currentlowest equal to lowesthealth in the first loop, and after that compares every value after it to the current lowest health
                if (i == 0)
                {
                    currentLowesthealth = lowestHealth;
                    currentHealTarget = healTarget;
                }

                //replaces current healing target if new target has less health than the last one
                if (lowestHealth < currentLowesthealth)
                {
                    currentLowesthealth = lowestHealth;
                    currentHealTarget = healTarget;
                }

            }
            healTargetMaxHealth = currentHealTarget.GetComponent<HealthSystem>().GetMaxHealth();
            currentHealTarget.GetComponent<HealthSystem>().Heal(healTargetMaxHealth * healPercentage); //Heals target's health based on % of maxHp
            UnityEngine.Debug.Log(this.gameObject.name + " healed " + currentHealTarget);
        }
        if (targetList.Count == 0)
        {
            RandomizeAction();
        }

        targetList.Clear(); // Clears list after turn is over to prevent potentially dead enemies from remaining in the list
    }
    /// <summary>
    /// Randomizes what the enemy does in order to give a feeling of variety to every encounter and every turn
    /// </summary>
    private void RandomizeAction()
    {
        float randomActionNumber = UnityEngine.Random.value;
        if (randomActionNumber < 0.5f)
        {
            RandomizeHealing();
        }

        else if (randomActionNumber < 0.8333f) // 0.5 + (1/6)
        {
            RandomizeOffense();
        }

        else if (randomActionNumber < 1.1666f) // 0.5 + (2/6)
        {
            Attack();
        }
    }

    private void RandomizeHealing()
    {
        int index = UnityEngine.Random.Range(0, 2);

        switch (index)
        {
            case 0:
                HealSelf();
                break;

            case 1:
                HealEnemy();
                break;
        }
    }

    private void RandomizeOffense()
    {
        int index = UnityEngine.Random.Range(0, 2);

        switch (index)
        {
            case 0:
                BuffOthers();
                break;

            case 1:
                Attack();
                break;
        }
    }
}

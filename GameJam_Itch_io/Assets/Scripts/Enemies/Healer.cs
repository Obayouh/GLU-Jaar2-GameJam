using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Healer : Ab_Enemy
{
    [SerializeField, Range(0.1f, 1f)] private float healPercentage;
    [SerializeField] private GameObject healEffect1;
    //[SerializeField] private ParticleSystem healEffect2;

    private float halfHealth;
    private float healTargetMaxHealth;

    private List<GameObject> targetList = new List<GameObject>();

    private Animator _healerAnim;
    private Coroutine _currentCoroutine = null;
    private HealthSystem _currentHealth;

    protected override void Start()
    {
        base.Start();
        if (healEffect1 == null  /*|| healEffect2 == null*/)
        {
            UnityEngine.Debug.Log("Fill in healEffect on Healer please!");
        }
        _currentHealth = GetComponent<HealthSystem>();
        _healerAnim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (_currentHealth.CurrentHealth <= 0)
        {
            DeadAnim();
        }
    }

    public override void OnAction()
    {
        RandomizeAction();
    }

    private void HealEnemy()
    {
        LookForOtherEnemies();
    }

    private void HealSelf()
    {
        if (_healthSystem.CanHeal() == false)
        {
            RandomizeAction();
            return;
        }

        _healthSystem.Heal(_healthSystem.GetMaxHealth() * healPercentage);
        Instantiate(healEffect1, transform.position, Quaternion.identity);
        StartCoroutine(ActivateHealEffect(transform.position));
        _healthSystem.HealthUpdate();
        UnityEngine.Debug.Log(this.gameObject.name + "healed itself");
    }

    private void Attack()
    {
        if (_currentCoroutine == null)
        {
            _currentCoroutine = StartCoroutine(AttackPlayer());
        }
    }

    private IEnumerator AttackPlayer()
    {
        _healerAnim.SetInteger("HealerState", 1);
        yield return new WaitForSeconds(0.8f);
        _playerStats.TakeDamage(damage);
        UnityEngine.Debug.Log("Should attack player and then end turn");
        yield return new WaitForSeconds(0.2f);
        _currentCoroutine = null;
        if (_currentCoroutine == null)
        {
            _healerAnim.SetInteger("HealerState", 0);
        }
    }

    public override void SpawnAnim()
    {
        //Play intro animation
    }

    public override void DeadAnim()
    {
        StartCoroutine(DeadAnimation());
        _currentHealth.Kill();
    }

    private IEnumerator DeadAnimation()
    {
        _healerAnim.SetInteger("HealerState", 2);
        yield return new WaitForSeconds(1f);
    }

    public override void HitAnim()
    {
        StartCoroutine(HitAnimation());
    }

    private IEnumerator HitAnimation()
    {
        _healerAnim.SetTrigger("Hit");
        yield return new WaitForSeconds(2.1f);
        _healerAnim.SetInteger("HealerState", 0);
    }

    IEnumerator ActivateHealEffect(Vector3 healPosition)
    {
        healEffect1.SetActive(true);
        healEffect1.transform.position = healPosition;
        yield return new WaitForSeconds(2f);
        healEffect1.SetActive(false);
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
            HealthSystem currentTargetHS = currentHealTarget.GetComponent<HealthSystem>();

            if (currentTargetHS.CanHeal() == false)
            {
                RandomizeAction();
                return;
            }

            currentTargetHS.Heal(healTargetMaxHealth * healPercentage); //Heals target's health based on % of maxHp
            StartCoroutine(ActivateHealEffect(currentHealTarget.transform.position));
            currentHealTarget.GetComponent<HealthSystem>().HealthUpdate(); //Updates healtarget's healthbar visuals
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

        else if (randomActionNumber < 1f)
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

    public override void UpdateDamage()
    {
        base.UpdateDamage();
    }
}

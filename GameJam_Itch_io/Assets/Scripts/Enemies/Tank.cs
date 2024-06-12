using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Warrior;

public class Tank : Ab_Enemy
{
    [SerializeField, Range(3, 60)] private int damageDealt; //Update in prefab if you change the value(s)

    private List<GameObject> targetList = new List<GameObject>();

    private Animator _tankAnim;
    private Coroutine _currentCoroutine = null;

    [SerializeField] private GameObject _HipAxe;
    [SerializeField] private GameObject _ArmAxe;
    [SerializeField] private GameObject _BowlHelmet;
    [SerializeField] private GameObject _DiscHelmet;
    [SerializeField] private GameObject _SpaceHelmet;

    [SerializeField] private GameObject[] ElementalIcons;

    protected override void Start()
    {
        base.Start();
        CheckForELementalIcon();
        _tankAnim = GetComponent<Animator>();
        _ArmAxe.SetActive(false);
        HelmetRandomizer();
        StartCoroutine(SwitchWeapon());
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
        if (_currentCoroutine == null)
        {
            _currentCoroutine = StartCoroutine(AttackPlayer());
            Debug.Log("Should attack player and then end turn");
        }
        else
        {
            Debug.Log("Current coroutine was already filled");
        }
    }

    private IEnumerator AttackPlayer()
    {
        _tankAnim.SetInteger("TankState", 2);
        yield return new WaitForSeconds(1f);
        _playerStats.TakeDamage(damageDealt);
        yield return new WaitForSeconds(1f);
        _currentCoroutine = null;
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

    private IEnumerator SwitchWeapon()
    {
        int _stateOfSkeleton = _tankAnim.GetInteger("TankState");

        if (_stateOfSkeleton == 0)
        {
            yield return new WaitForSeconds(4.3f);
            _HipAxe.SetActive(false);
            _ArmAxe.SetActive(true);
            _tankAnim.SetInteger("TankState", 1);
        }
    }

    private void HelmetRandomizer()
    {
        float randomActionNumber = UnityEngine.Random.value;
        
        if (randomActionNumber >= 0.0f && randomActionNumber <= 0.4f)
        {
            _BowlHelmet.SetActive(true);
            _DiscHelmet.SetActive(false);
            _SpaceHelmet.SetActive(false);
        }
        else if (randomActionNumber > 0.4f && randomActionNumber <= 0.8f)
        {
            _BowlHelmet.SetActive(false);
            _DiscHelmet.SetActive(true);
            _SpaceHelmet.SetActive(false);
        }
        else if (randomActionNumber > 0.8f)
        {
            _BowlHelmet.SetActive(false);
            _DiscHelmet.SetActive(false);
            _SpaceHelmet.SetActive(true);
        }
    }

    private void CheckForELementalIcon()
    {
        if (elementalType == E_ElementalTyping.Neutral)
        {
            ElementalIcons[0].SetActive(true);
            ElementalIcons[1].SetActive(false);
            ElementalIcons[2].SetActive(false);
            ElementalIcons[3].SetActive(false);
            ElementalIcons[4].SetActive(false);
        }

        if (elementalType == E_ElementalTyping.Fire)
        {
            ElementalIcons[0].SetActive(false);
            ElementalIcons[1].SetActive(true);
            ElementalIcons[2].SetActive(false);
            ElementalIcons[3].SetActive(false);
            ElementalIcons[4].SetActive(false);
        }

        if (elementalType == E_ElementalTyping.Lightning)
        {
            ElementalIcons[0].SetActive(false);
            ElementalIcons[1].SetActive(false);
            ElementalIcons[2].SetActive(true);
            ElementalIcons[3].SetActive(false);
            ElementalIcons[4].SetActive(false);
        }

        if (elementalType == E_ElementalTyping.Rock)
        {
            ElementalIcons[0].SetActive(false);
            ElementalIcons[1].SetActive(false);
            ElementalIcons[2].SetActive(false);
            ElementalIcons[3].SetActive(true);
            ElementalIcons[4].SetActive(false);
        }

        if (elementalType == E_ElementalTyping.Water)
        {
            ElementalIcons[0].SetActive(false);
            ElementalIcons[1].SetActive(false);
            ElementalIcons[2].SetActive(false);
            ElementalIcons[3].SetActive(false);
            ElementalIcons[4].SetActive(true);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Tank : Ab_Enemy
{
    [SerializeField, Range(3, 60)] private int damageDealt; //Update in prefab if you change the value(s)

    private List<GameObject> targetList = new List<GameObject>();

    private Animator _tankAnim;
    private HealthSystem _currentHealth;
    private Coroutine _currentCoroutine = null;

    [SerializeField] private GameObject _HipSword;
    [SerializeField] private GameObject _ArmSword;
    [SerializeField] private GameObject _BackShield;
    [SerializeField] private GameObject _ArmShield;

    [SerializeField] private GameObject _BowlHelmet;
    [SerializeField] private GameObject _DiscHelmet;
    [SerializeField] private GameObject _SpaceHelmet;

    [SerializeField] private GameObject _ShieldVFX;

    protected override void Start()
    {
        base.Start();
        if (_ShieldVFX == null)
        {
            Debug.Log("Fill in ShieldVFX on Tank prefab please!");
        }
        CheckForElementalIcon();
        _currentHealth = GetComponent<HealthSystem>();
        _tankAnim = GetComponentInChildren<Animator>();
        _ArmSword.SetActive(false);
        _ArmShield.SetActive(false);
        HelmetRandomizer();
        StartCoroutine(SwitchWeapon());
    }

    private void Update()
    {
        if (_currentHealth.CurrentHealth <= 0 && !dead)
        {
            DeadAnim();

            dead = true;
        }
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
            Debug.Log("The current coroutine was already filled");
        }
    }

    private IEnumerator AttackPlayer()
    {
        _tankAnim.SetInteger("TankState", 2);
        yield return new WaitForSeconds(.3f);
        AudioManager.Instance.Play("Sword Attack");
        yield return new WaitForSeconds(.5f);
        _playerStats.TakeDamage(damageDealt);
        yield return new WaitForSeconds(1f);
        _currentCoroutine = null;
        if (_currentCoroutine == null)
        {
            _tankAnim.SetInteger("TankState", 1);
        }
    }

    private void Taunt()
    {
        if (ReferenceInstance.refInstance.clickManager.HasSelectedEnemy() == true)
        {
            RandomizeAction();
            return;
        }

        if (_currentCoroutine == null)
        {
            StartCoroutine(PlayTauntAnim());
            BuffIcons[1].SetActive(true);
        }
    }

    private IEnumerator PlayTauntAnim()
    {
        _tankAnim.SetInteger("TankState", 5);
        AudioManager.Instance.Play("Taunt");
        ReferenceInstance.refInstance.clickManager.Taunt(this.gameObject);
        Debug.Log("Taunts... Player can only attack tank next turn!");
        yield return new WaitForSeconds(1f);
        _currentCoroutine = null;
        if (_currentCoroutine == null)
        {
            _tankAnim.SetInteger("TankState", 1);
        }
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
            StartCoroutine(PlayShieldAnim());
            BuffIcons[2].SetActive(true);
        }
    }

    private IEnumerator PlayShieldAnim()
    {
        Debug.Log(this.gameObject.name + " puts a shield on itself");
        _tankAnim.SetInteger("TankState", 3);
        StartCoroutine(ActivateShieldEffect(this.transform.position));
        _healthSystem.GetComponent<HealthSystem>().hasShield = true;
        yield return new WaitForSeconds(.1f);
        AudioManager.Instance.Play("Shielding");
        yield return new WaitForSeconds(1f);
        _currentCoroutine = null;
        if (_currentCoroutine == null)
        {
            _tankAnim.SetInteger("TankState", 1);
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
                currentShieldTarget.GetComponent<Ab_Enemy>().SetShieldIcon();
                AudioManager.Instance.Play("Shielding Others");
                StartCoroutine(ActivateShieldEffect(currentShieldTarget.transform.position));
                Debug.Log(this.gameObject.name + " puts a shield on " + currentShieldTarget);
            }
        }
        if (targetList.Count == 0)
        {
            RandomizeAction();
        }

        targetList.Clear(); // Clears list after turn is over to prevent potentially dead enemies from remaining in the list
    }

    IEnumerator ActivateShieldEffect(Vector3 shieldPosition)
    {
        _ShieldVFX.SetActive(true);
        _ShieldVFX.transform.position = shieldPosition;
        yield return new WaitForSeconds(2f);
        _ShieldVFX.SetActive(false);
    }

    public override void DeadAnim()
    {
        StartCoroutine(DeadAnimation());
        _currentHealth.Kill();
    }

    private IEnumerator DeadAnimation()
    {
        _tankAnim.SetInteger("TankState", 4);
        yield return new WaitForSeconds(1f);
    }

    public override void HitAnim()
    {
        StartCoroutine(HitAnimation());
    }

    private IEnumerator HitAnimation()
    {
        _tankAnim.SetTrigger("Hit");
        yield return new WaitForSeconds(2.1f);
        _tankAnim.SetInteger("TankState", 1);
    }

    private IEnumerator SwitchWeapon()
    {
        int _stateOfSkeleton = _tankAnim.GetInteger("TankState");

        if (_stateOfSkeleton == 0)
        {
            yield return new WaitForSeconds(4.3f);
            _HipSword.SetActive(false);
            _BackShield.SetActive(false);
            _ArmSword.SetActive(true);
            _ArmShield.SetActive(true);
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

    protected override void CheckForElementalIcon()
    {
        base.CheckForElementalIcon();
    }
}

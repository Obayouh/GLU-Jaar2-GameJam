using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Healer : Ab_Enemy
{
    public enum StateOfHealer
    {
        //BuffOthers,
        HealOthers,
        HealMe,
    }

    public StateOfHealer State;

    public float HealOtherRange;
    [HideInInspector] public float HealSelfRange;
    public float HealthBack;

    private GameObject _player;
    private TurnManager _turnManager;
    private SpawnSlot _spawnSlot;
    private HealthSystem _healthSystem;
    private SpawnEnemies _spawnEnemies;

    private HealthSystem FirstEnemyHealth;
    private HealthSystem SecondEnemyHealth;
    private HealthSystem ThirdEnemyHealth;

    private bool HealFirst;
    private bool HealSecond;
    private bool HealThird;

    private float _countdown;
    private float _resetTimer = 2f;
    private float _maxHealth;

    void Start()
    {
        _player = FindObjectOfType<PlayerStats>().gameObject;
        _turnManager = FindObjectOfType<TurnManager>();
        _spawnSlot = GetComponentInParent<SpawnSlot>();
        _healthSystem = GetComponent<HealthSystem>();
        _spawnEnemies = FindObjectOfType<SpawnEnemies>();

        Array arrayElement = Enum.GetValues(typeof(Element));
        Elements = (Element)arrayElement.GetValue(UnityEngine.Random.Range(0, arrayElement.Length));
        
        _countdown = _resetTimer;
        Invoke(nameof(GetHealRange), 0.25f);
        Invoke(nameof(GetSelfHealRange), 0.255f);
        Invoke(nameof(GetHealthBack), 0.26f);
    }

    void Update()
    {
        Transform parentPos = _spawnSlot.transform;

        if (_turnManager.FirstEnemyGo && parentPos.position.x == _spawnEnemies.FirstSpawnPointPos)
        {
            SecondEnemyHealth = _spawnEnemies.CurrentSpawnedEnemies[1].GetComponent<HealthSystem>();
            ThirdEnemyHealth = _spawnEnemies.CurrentSpawnedEnemies[2].GetComponent<HealthSystem>();

            if (SecondEnemyHealth.CurrentHealth < HealOtherRange && ThirdEnemyHealth.CurrentHealth > HealOtherRange)
            {
                HealSecond = true;
            }
            
            if (ThirdEnemyHealth.CurrentHealth < HealOtherRange && SecondEnemyHealth.CurrentHealth > HealOtherRange)
            {
                HealThird = true;
            }

            StartCountdown();
            if (_countdown <= 0f)
            {
                SwitchState();
                _countdown = _resetTimer;
                _turnManager.FirstEnemyGo = false;
                _turnManager.SecondEnemyGo = true;
            }
        }

        if (_turnManager.SecondEnemyGo && parentPos.position.x == _spawnEnemies.SecondSpawnPointPos)
        {
            FirstEnemyHealth = _spawnEnemies.CurrentSpawnedEnemies[0].GetComponent<HealthSystem>();
            ThirdEnemyHealth = _spawnEnemies.CurrentSpawnedEnemies[2].GetComponent<HealthSystem>();

            if (FirstEnemyHealth.CurrentHealth < HealOtherRange && ThirdEnemyHealth.CurrentHealth > HealOtherRange)
            {
                HealFirst = true;
            }

            if (ThirdEnemyHealth.CurrentHealth < HealOtherRange && FirstEnemyHealth.CurrentHealth > HealOtherRange)
            {
                HealThird = true;
            }

            StartCountdown();
            if (_countdown <= 0f)
            {
                SwitchState();
                _countdown = _resetTimer;
                _turnManager.SecondEnemyGo = false;
                _turnManager.LastEnemyGo = true;
            }
        }

        if (_turnManager.LastEnemyGo && parentPos.position.x == _spawnEnemies.ThirdSpawnPointPos)
        {
            FirstEnemyHealth = _spawnEnemies.CurrentSpawnedEnemies[0].GetComponent<HealthSystem>();
            SecondEnemyHealth = _spawnEnemies.CurrentSpawnedEnemies[1].GetComponent<HealthSystem>();

            if (FirstEnemyHealth.CurrentHealth < HealOtherRange && SecondEnemyHealth.CurrentHealth > HealOtherRange)
            {
                HealFirst = true;
            }

            if (SecondEnemyHealth.CurrentHealth < HealOtherRange && FirstEnemyHealth.CurrentHealth > HealOtherRange)
            {
                HealSecond = true;
            }

            StartCountdown();
            if (_countdown <= 0f)
            {
                SwitchState();
                _countdown = _resetTimer;
                _turnManager.LastEnemyGo = false;
                Debug.Log("testical");
                //_turnManager.AddNewCards = true;
                //_turnManager.ChangeState(TurnState.PickCard);
            }
        }
    }

    private void StartCountdown()
    {
        transform.LookAt(_player.transform);
        _countdown -= Time.deltaTime;
    }

    private void GetHealRange()
    {
        _maxHealth = _healthSystem.CurrentHealth;
        HealOtherRange = _maxHealth * 50 / 100;
    }

    private void GetSelfHealRange()
    {
        HealSelfRange = _maxHealth * 30 / 100;
    }

    private void GetHealthBack()
    {
        HealthBack = _maxHealth * 25 / 100;
    }
    private void OtherLowOnHealth(HealthSystem enemyHealth)
    {
        enemyHealth.Heal(HealthBack - 1);
    }

    private void SwitchState()
    {
        //if (_healthSystem.CurrentHealth > HealOtherRange)
        //{
        //    State = StateOfHealer.BuffOthers;
        //    ChooseState();
        //}
        
        if (_healthSystem.CurrentHealth >= HealOtherRange /*&& _healthSystem.CurrentHealth > HealSelfRange*/)
        {
            State = StateOfHealer.HealOthers;
            ChooseState();
        }
        
        if (_healthSystem.CurrentHealth <= HealOtherRange)
        {
            State = StateOfHealer.HealMe;
            ChooseState();
        }
    }

    private void ChooseState()
    {
        switch(State)
        {
            //case StateOfHealer.BuffOthers:

            //    break;

            case StateOfHealer.HealOthers:
                if (HealFirst)
                {
                    OtherLowOnHealth(FirstEnemyHealth);
                    HealFirst = false;
                }

                if (HealSecond)
                {
                    OtherLowOnHealth(SecondEnemyHealth);
                    HealSecond = false;
                }

                if (HealThird)
                {
                    OtherLowOnHealth(ThirdEnemyHealth);
                    HealThird = false;
                }
                break;

            case StateOfHealer.HealMe: 
                _healthSystem.Heal(HealthBack);
                break;
        }
    }
}

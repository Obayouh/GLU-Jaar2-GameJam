using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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

    private HealthSystem FirstEnemyHealth;
    private HealthSystem SecondEnemyHealth;
    private HealthSystem ThirdEnemyHealth;

    private bool HealFirst;
    private bool HealSecond;
    private bool HealThird;

    private float _countdown;
    private float _resetTimer = 2f;

    protected override void Start()
    {
        base.Start();

        _countdown = _resetTimer;
        Invoke(nameof(GetHealRange), 0.25f);
        Invoke(nameof(GetSelfHealRange), 0.255f);
    }

    public override void OnAction()
    {
        base.OnAction();

        FirstEnemyHealth = _spawnEnemies.spawnedEnemies[0].GetComponent<HealthSystem>();
        SecondEnemyHealth = _spawnEnemies.spawnedEnemies[1].GetComponent<HealthSystem>();
        ThirdEnemyHealth = _spawnEnemies.spawnedEnemies[2].GetComponent<HealthSystem>();

        if (FirstEnemyHealth.CurrentHealth < HealOtherRange && SecondEnemyHealth.CurrentHealth > HealOtherRange)
        {
            HealFirst = true;
        }
        else if (SecondEnemyHealth.CurrentHealth < HealOtherRange && FirstEnemyHealth.CurrentHealth > HealOtherRange)
        {
            HealSecond = true;
        }
        else if (FirstEnemyHealth.CurrentHealth < HealOtherRange && ThirdEnemyHealth.CurrentHealth > HealOtherRange)
        {
            HealFirst = true;
        }
        else if (ThirdEnemyHealth.CurrentHealth < HealOtherRange && FirstEnemyHealth.CurrentHealth > HealOtherRange)
        {
            HealThird = true;
        }
        else if (SecondEnemyHealth.CurrentHealth < HealOtherRange && ThirdEnemyHealth.CurrentHealth > HealOtherRange)
        {
            HealSecond = true;
        }
        else if (ThirdEnemyHealth.CurrentHealth < HealOtherRange && SecondEnemyHealth.CurrentHealth > HealOtherRange)
        {
            HealThird = true;
        }

        StartCountdown();
        if (_countdown <= 0f)
        {
            SwitchState();
            _countdown = _resetTimer;
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
        if (_healthSystem.CurrentHealth >= HealOtherRange /*&& _healthSystem.CurrentHealth > HealSelfRange*/)
        {
            State = StateOfHealer.HealOthers;
            ChooseState();
        }
        else if (_healthSystem.CurrentHealth < HealOtherRange)
        {
            State = StateOfHealer.HealMe;
            ChooseState();
        }
    }

    private void ChooseState()
    {
        switch (State)
        {
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

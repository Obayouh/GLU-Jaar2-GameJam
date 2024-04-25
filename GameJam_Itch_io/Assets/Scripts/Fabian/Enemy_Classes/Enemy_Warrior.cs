using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy_Healer;

public class Enemy_Warrior : Ab_Enemy
{
    public enum StateOfWarior
    {
        AttackPlayer,
        BuffItself,
    }

    public StateOfWarior WarriorsState;

    public float HalfHealth;

    private GameObject _player;
    private TurnManager _turnManager;
    private SpawnSlot _spawnSlot;
    private HealthSystem _healthSystem;
    private SpawnEnemies _spawnEnemies;

    private float _countdown;
    private float _resetTimer = 2f;
    private float _maxHealth;

    void Start()
    {
        //_player = FindObjectOfType<PlayerStats>().gameObject;
        //_turnManager = FindObjectOfType<TurnManager>();
        //_spawnSlot = GetComponentInParent<SpawnSlot>();
        //_healthSystem = GetComponent<HealthSystem>();
        //_spawnEnemies = FindObjectOfType<SpawnEnemies>();
        FindAll();

        Array arrayElement = Enum.GetValues(typeof(Element));
        Elements = (Element)arrayElement.GetValue(UnityEngine.Random.Range(0,arrayElement.Length));

        _countdown = _resetTimer;
        //Invoke(GetUseAbiltyRange(_healthSystem, HalfHealth), 0.25f);
    }

    void Update()
    {
        Transform parentPos = _spawnSlot.transform;

        if (_turnManager.FirstEnemyGo && parentPos.position.x == _spawnEnemies.FirstSpawnPointPos)
        {

            BeginCountdown(_player.transform, _countdown);
            if (_countdown <= 0f)
            {
                SwitchState();
                _countdown = _resetTimer;
                _turnManager.FirstEnemyGo = false;
                _turnManager.SecondEnemyGo = true;
            }
        }
    }

    public override void FindAll()
    {
        base.FindAll();
    }

    public override void BeginCountdown(Transform transform, float countdown)
    {
        base.BeginCountdown(transform, countdown);
    }

    private void GetAbiltyRange()
    {
        _maxHealth = _healthSystem.CurrentHealth;
        HalfHealth = _maxHealth * 50 / 100;
    }

    public override void GetUseAbiltyRange(HealthSystem currentHP, float halfHP)
    {
        base.GetUseAbiltyRange(currentHP, halfHP);
    }

    private void SwitchState()
    {
        //if (_healthSystem.CurrentHealth > HealOtherRange)
        //{
        //    State = StateOfHealer.BuffOthers;
        //    ChooseState();
        //}

        //if (_healthSystem.CurrentHealth >= HealOtherRange /*&& _healthSystem.CurrentHealth > HealSelfRange*/)
        //{
        //    State = StateOfHealer.HealOthers;
        //    ChooseState();
        //}

        //if (_healthSystem.CurrentHealth <= HealOtherRange)
        //{
        //    State = StateOfHealer.HealMe;
        //    ChooseState();
        //}
    }
}

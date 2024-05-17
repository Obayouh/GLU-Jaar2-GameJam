using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Ab_Enemy
{
    public enum TheStateOfTheWarior
    {
        Attack,
        BuffThemself,
        Waiting,
    }

    [SerializeField, Range(1, 50)] private int _MinAttackPower;
    [SerializeField, Range(2, 100)] private int _MaxAttackPower;

    public TheStateOfTheWarior StateOfTheWarior;

    private Animator WarriorAnim;
    private PlayerStats _playerStats;

    private float _maxLife;
    private float _halfLife;

    private bool _attackBuff;
    private int _detectHalfLife;

    protected override void Start()
    {
        base.Start();

        //Array arrayElement = Enum.GetValues(typeof(Typing));
        //Typing randomType = (Typing)arrayElement.GetValue(UnityEngine.Random.Range(0, arrayElement.Length));

        _playerStats = _player.GetComponent<PlayerStats>();
        _maxLife = GetComponent<HealthSystem>().CurrentHealth;
        _halfLife = _maxLife / 2;
    }

    void Update()
    {
        if (StateOfTheWarior == TheStateOfTheWarior.Attack)
        {
            AttackPlayer();
        }

        if (StateOfTheWarior == TheStateOfTheWarior.BuffThemself)
        {
            BuffItself();
        }

        //if (StateOfTheWarior == TheStateOfTheWarior.Waiting)
        //{
        //    CurrentlyWaiting();
        //}
    }

    public override void OnAction()
    {
        if (_healthSystem.CurrentHealth <= _halfLife && _detectHalfLife == 0)
        {
            _detectHalfLife++;
            StateOfTheWarior = TheStateOfTheWarior.BuffThemself;
        }
        else
        {
            StateOfTheWarior = TheStateOfTheWarior.Attack;
        }
    }

    private void AttackPlayer()
    {
        _playerStats.TakeDamage(UnityEngine.Random.Range(_MinAttackPower, _MaxAttackPower));
        
        StateOfTheWarior = TheStateOfTheWarior.Waiting;
    }

    private void BuffItself()
    {
        _MinAttackPower *= 2;
        _MaxAttackPower *= 2;

        StateOfTheWarior = TheStateOfTheWarior.Waiting;
    }

    //private void CurrentlyWaiting()
    //{
    //    return;
    //}
}

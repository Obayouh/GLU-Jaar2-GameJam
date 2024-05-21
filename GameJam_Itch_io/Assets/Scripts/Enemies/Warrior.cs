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

    private Animator _warriorAnim;
    private PlayerStats _playerStats;
    private HealthSystem _currentHealth;

    private float _maxLife;
    private float _halfLife;

    private bool _attackBuff;
    private int _detectHalfLife;
    private Coroutine currentCoroutine = null;

    protected override void Start()
    {
        base.Start();

        //Array arrayElement = Enum.GetValues(typeof(Typing));
        //Typing randomType = (Typing)arrayElement.GetValue(UnityEngine.Random.Range(0, arrayElement.Length));

        _playerStats = _player.GetComponent<PlayerStats>();
        _currentHealth = _player.GetComponent<HealthSystem>();
        _maxLife = GetComponent<HealthSystem>().CurrentHealth;
        _warriorAnim = GetComponent<Animator>();
        _halfLife = _maxLife / 2;
    }

    void Update()
    {
        if (StateOfTheWarior == TheStateOfTheWarior.Attack && currentCoroutine == null)
        {
            //AttackPlayer();
            currentCoroutine = StartCoroutine(AttackPlayer());
        }

        if (StateOfTheWarior == TheStateOfTheWarior.BuffThemself)
        {
            BuffItself();
        }

        if (StateOfTheWarior == TheStateOfTheWarior.Waiting)
        {
            CurrentlyWaiting();
        }
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

    private IEnumerator AttackPlayer()
    {
        _warriorAnim.SetInteger("SkeletonState", 1);
        Debug.Log("Attack animation");
        yield return new WaitForSeconds(1f);
        _playerStats.TakeDamage(UnityEngine.Random.Range(_MinAttackPower, _MaxAttackPower));
        yield return new WaitForSeconds(1f);
        StateOfTheWarior = TheStateOfTheWarior.Waiting;
        currentCoroutine = null;
    }

    private void BuffItself()
    {
        _MinAttackPower *= 2;
        _MaxAttackPower *= 2;

        StateOfTheWarior = TheStateOfTheWarior.Waiting;
    }

    private void CurrentlyWaiting()
    {
        int _stateOfSkeleton = _warriorAnim.GetInteger("SkeletonState");

        if (_stateOfSkeleton == 1 || _stateOfSkeleton == 2)
        {
            _warriorAnim.SetInteger("SkeletonState", 0);
            Debug.Log("Idle animation");
        }
    }

    //public override void DeadAnim()
    //{
    //    if (_currentHealth.CurrentHealth <= 0)
    //    {

    //    }
    //}
}

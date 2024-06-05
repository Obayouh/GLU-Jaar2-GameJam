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
    private HealthSystem _currentHealth;

    private float _maxLife;
    private float _halfLife;

    private bool _attackBuff;
    private int _detectHalfLife;
    private Coroutine currentCoroutine = null;

    [SerializeField] private GameObject _HipAxe;
    [SerializeField] private GameObject _ArmAxe;

    protected override void Start()
    {
        base.Start();

        _currentHealth = GetComponent<HealthSystem>();
        _maxLife = GetComponent<HealthSystem>().CurrentHealth;
        _warriorAnim = GetComponent<Animator>();
        _halfLife = _maxLife / 2;
        _ArmAxe.SetActive(false);
        StartCoroutine(SwitchWeapon());
    }

    void Update()
    {
        if (StateOfTheWarior == TheStateOfTheWarior.Attack && currentCoroutine == null)
        {
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

        if (_currentHealth.CurrentHealth <= 0)
        {
            DeadAnim();
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
        _warriorAnim.SetInteger("WarriorState", 2);
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
        int _stateOfWarrior = _warriorAnim.GetInteger("WarriorState");

        if (_stateOfWarrior == 2 || _stateOfWarrior == 3)
        {
            _warriorAnim.SetInteger("WarriorState", 1);
        }
    }

    public override void DeadAnim()
    {
        StartCoroutine(DeadAnimation());
        _currentHealth.Kill();
    }

    private IEnumerator DeadAnimation()
    {
        _warriorAnim.SetInteger("WarriorState", 4);
        yield return new WaitForSeconds(1f);
    }

    private IEnumerator SwitchWeapon()
    {
        int _stateOfWarrior = _warriorAnim.GetInteger("WarriorState");
        
        if (_stateOfWarrior == 0)
        {
            yield return new WaitForSeconds(4.3f);
            _HipAxe.SetActive(false);
            _ArmAxe.SetActive(true);
            _warriorAnim.SetInteger("WarriorState", 1);
        }
    }
}

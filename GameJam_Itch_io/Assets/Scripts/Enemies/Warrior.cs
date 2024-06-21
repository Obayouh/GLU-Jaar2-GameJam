using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Warrior : Ab_Enemy
{
    public enum TheStateOfTheWarior
    {
        Attack,
        BuffThemself,
        Waiting,
        Nothing
    }

    [SerializeField, Range(1, 50)] private int _MinAttackPower;
    [SerializeField, Range(2, 100)] private int _MaxAttackPower;

    public TheStateOfTheWarior StateOfTheWarior;

    private Animator _warriorAnim;
    private HealthSystem _currentHealth;

    private float _maxLife;
    private float _halfLife;

    private int _detectHalfLife;
    private Coroutine _currentCoroutine = null;

    [SerializeField] private GameObject _HipAxe;
    [SerializeField] private GameObject _ArmAxe;
    [SerializeField] private GameObject _BuffVFX;

    protected override void Start()
    {
        base.Start();
        if (_BuffVFX == null)
        {
            Debug.Log("Fill in BuffVFX on Warrior please!");
        }
        CheckForElementalIcon();
        _currentHealth = GetComponent<HealthSystem>();
        _maxLife = GetComponent<HealthSystem>().CurrentHealth;
        _warriorAnim = GetComponent<Animator>();
        _halfLife = _maxLife / 2;
        _ArmAxe.SetActive(false);
        StartCoroutine(SwitchWeapon());
    }

    void Update()
    {
        if (StateOfTheWarior == TheStateOfTheWarior.Attack && _currentCoroutine == null)
        {
            _currentCoroutine = StartCoroutine(AttackPlayer());
        }

        if (StateOfTheWarior == TheStateOfTheWarior.BuffThemself)
        {
            BuffItself();
        }

        if (StateOfTheWarior == TheStateOfTheWarior.Waiting)
        {
            CurrentlyWaiting();
        }

        if (_currentHealth.CurrentHealth <= 0 && !dead)
        {
            DeadAnim();

            dead = true;
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
        yield return new WaitForSeconds(.5f);
        AudioManager.Instance.Play("Axe Attack");
        yield return new WaitForSeconds(.5f);
        _playerStats.TakeDamage(UnityEngine.Random.Range(_MinAttackPower, _MaxAttackPower));
        yield return new WaitForSeconds(1f);
        StateOfTheWarior = TheStateOfTheWarior.Waiting;
        _currentCoroutine = null;
    }

    private void BuffItself()
    {
        StateOfTheWarior = TheStateOfTheWarior.Nothing;
        Debug.Log("Warrior buff");
        _MinAttackPower *= 2;
        _MaxAttackPower *= 2;
        BuffIcons[0].SetActive(true);
        StartCoroutine(WaitForVFX());
    }

    private IEnumerator WaitForVFX()
    {
        AudioManager.Instance.Play("Buff");
        _warriorAnim.SetInteger("WarriorState", 3);
        StartCoroutine(BuffEffectActivation());
        yield return new WaitForSeconds(0.1f);
    }

    private IEnumerator BuffEffectActivation()
    {
        _BuffVFX.SetActive(true);
        yield return new WaitForSeconds(2f);
        _BuffVFX.SetActive(false);
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

    public override void HitAnim()
    {
        StartCoroutine(HitAnimation());
    }

    private IEnumerator HitAnimation()
    {
        _warriorAnim.SetTrigger("Hit");
        yield return new WaitForSeconds(2.1f);
        _warriorAnim.SetInteger("WarriorState", 1);
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

    protected override void CheckForElementalIcon()
    {
        base.CheckForElementalIcon();
    }
}

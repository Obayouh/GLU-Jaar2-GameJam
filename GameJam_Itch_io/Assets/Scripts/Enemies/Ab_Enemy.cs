using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ab_Enemy : MonoBehaviour
{
    protected GameObject _player;
    protected TurnManager _turnManager;
    protected SpawnSlot _spawnSlot;
    protected HealthSystem _healthSystem;
    protected SpawnEnemies _spawnEnemies;
    protected EnemyController _enemyController;
    protected PlayerStats _playerStats;

    [SerializeField, Range(1, 60)] protected int damage;
    protected float _maxHealth;

    [field: SerializeField] public E_ElementalTyping elementalType { get; private set; }

    protected virtual void Start()
    {
        FindAll();
        Array arrayElement = Enum.GetValues(typeof(E_ElementalTyping));
        elementalType = (E_ElementalTyping)arrayElement.GetValue(UnityEngine.Random.Range(0, arrayElement.Length));
    }

    protected virtual void FindAll()
    {
        _player = FindObjectOfType<PlayerStats>().gameObject;
        _turnManager = FindObjectOfType<TurnManager>();
        _spawnSlot = GetComponentInParent<SpawnSlot>();
        _healthSystem = GetComponent<HealthSystem>();
        _spawnEnemies = FindObjectOfType<SpawnEnemies>();
        _enemyController = FindObjectOfType<EnemyController>();
        _playerStats = FindFirstObjectByType<PlayerStats>();
    }

    public virtual void OnAction()
    {
        //Perform action of Enemy script
    }

    public virtual void CheckForStatusEffects()
    {
        _healthSystem.hasShield = false;

        ReferenceInstance.refInstance.clickManager.RemoveTaunt();
    }

    /// <summary>
    /// At the start of the battle play animation
    /// </summary>
    public virtual void SpawnAnim() { }

    /// <summary>
    /// Before HealthSystem Kill() play animation
    /// </summary>
    public virtual void DeadAnim() { }

    /// <summary>
    /// If enemy gets hit play HitAnim()
    /// </summary>
    public virtual void HitAnim() { }


    public virtual void LookAtPlayer() { }

    public virtual void UpdateDamage()
    {
        damage++;
    }

    public virtual void BeginCountdown(Transform transform, float countdown) 
    {
        this.transform.LookAt(transform);
        countdown -= Time.deltaTime;
    }

    public virtual void GetUseAbiltyRange(HealthSystem currentHP, float halfHP)
    {
        _maxHealth = currentHP.CurrentHealth;
        halfHP = _maxHealth * 50 / 100;
    }
}

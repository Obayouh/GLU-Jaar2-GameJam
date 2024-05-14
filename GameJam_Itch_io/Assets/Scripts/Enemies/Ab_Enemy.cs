using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ab_Enemy : MonoBehaviour
{
    public enum Element
    {
        Heat,
        Water,
        Volt,
        Gaia,
        Neutral,
    }

    public Element Elements;

    [SerializeField]
    protected GameObject _player;
    [SerializeField]
    protected TurnManager _turnManager;
    [SerializeField]
    protected SpawnSlot _spawnSlot;
    [SerializeField]
    protected HealthSystem _healthSystem;
    [SerializeField]
    protected SpawnEnemies _spawnEnemies;
    [SerializeField]
    protected EnemyController _enemyController;

    protected float _maxHealth;

    protected virtual void Start()
    {
        FindAll();
    }

    protected virtual void FindAll()
    {
        _player = FindObjectOfType<PlayerStats>().gameObject;
        _turnManager = FindObjectOfType<TurnManager>();
        _spawnSlot = GetComponentInParent<SpawnSlot>();
        _healthSystem = GetComponent<HealthSystem>();
        _spawnEnemies = FindObjectOfType<SpawnEnemies>();
        _enemyController = FindObjectOfType<EnemyController>();
    }

    public virtual void OnAction()
    {
        //Perform action of Enemy script
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
    /// Give specifics on when to do an action
    /// </summary>
    //public virtual void TurnToAct() { }

    /// <summary>
    /// Give out an reward to the player, currency for instance
    /// </summary>
    //public virtual void Reward() { }

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

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

    private GameObject _player;
    private TurnManager _turnManager;
    private SpawnSlot _spawnSlot;
    private HealthSystem _healthSystem;
    private SpawnEnemies _spawnEnemies;

    private float _maxHealth;

    public virtual void FindAll()
    {
        _player = FindObjectOfType<PlayerStats>().gameObject;
        _turnManager = FindObjectOfType<TurnManager>();
        _spawnSlot = GetComponentInParent<SpawnSlot>();
        _healthSystem = GetComponent<HealthSystem>();
        _spawnEnemies = FindObjectOfType<SpawnEnemies>();
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ab_HealthManager : MonoBehaviour
{

    public event Action<float, float> OnHealthChanged;

    [SerializeField, Range(0, 100)] protected float currentHealth;
    [SerializeField, Range(10, 100)] protected float maxHealth;

    public bool hasShield;

    public virtual void Start()
    {
        currentHealth = maxHealth;
    }

    //public float that manages the currenthealth, maxhealth, and prevents health from exceeding the max value given
    public float CurrentHealth
    {
        get => currentHealth;
        protected set
        {
            currentHealth = Mathf.Clamp(value, 0f, maxHealth);
            // Trigger the OnHealthChanged event with the new and max health values
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }
    }

    public virtual bool CanHeal()
    {
        if (currentHealth >= maxHealth)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public virtual void Heal(float amount)
    {
        CurrentHealth += Mathf.Abs(amount);
        CurrentHealth = Mathf.RoundToInt(CurrentHealth);    
    }

    public virtual void TakeDamage(float amount)
    {
        if (hasShield)
        {
            amount *= 0.5f;
        }

        CurrentHealth -= Mathf.Abs(amount);
    }

    public virtual void Kill()
    {
        Debug.Log(gameObject.name + " has died!");
    }
}

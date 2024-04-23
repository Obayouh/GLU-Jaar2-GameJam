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
    public virtual void TurnToAct() { }

    /// <summary>
    /// Give out an reward to the player, currency for instance
    /// </summary>
    public virtual void Reward() { }
}

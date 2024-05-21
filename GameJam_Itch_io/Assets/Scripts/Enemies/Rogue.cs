using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Tank;

public class Rogue : Ab_Enemy
{
    public enum RogueState
    {
        Attacking,
        Stealh,
        StealthAttack,
        Waiting
    }

    [SerializeField, Range(3, 60)] private int damageDealt; //Update in prefab if you change the value(s)

    public RogueState state;

    protected override void Start()
    {
        base.Start();
    }

    public override void OnAction()
    {
        base.OnAction();

        RandomizeAction();
    }

    private void RandomizeAction()
    {
        float randomActionNumber = UnityEngine.Random.value;
        if (randomActionNumber < 0.8f)
        {
            Attack();
        }

        else if (randomActionNumber < 1)
        {
            Stealth();
        }

        else if (state == RogueState.Stealh)
        {
            StealthAttack();
        }
    }

    private void Attack()
    {
        state = RogueState.Attacking;
        _playerStats.TakeDamage(damageDealt);
        UnityEngine.Debug.Log("Should attack player and then end turn");
        state = RogueState.Waiting;
    }

    private void Stealth()
    {
        state = RogueState.Stealh;
        UnityEngine.Debug.Log("Rogue disappeared; can't be attacked next round");
    }

    private void StealthAttack()
    {
        state = RogueState.StealthAttack;
        _playerStats.TakeDamage(damageDealt * 1.5f);
        UnityEngine.Debug.Log("Rogue RETURNED and attacks player");
        state = RogueState.Waiting;
    }
}

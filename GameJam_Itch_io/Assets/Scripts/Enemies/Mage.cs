using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Ab_Enemy
{
    [SerializeField, Range(3, 60)] private int damageDealt; //Update in prefab if you change the value(s)

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
        if (randomActionNumber < 0.5f)
        {
            Attack();
        }

        else if (randomActionNumber < 0.8f)
        {

        }

        else if (randomActionNumber < 1)
        {

        }
    }

    private void Attack()
    {
        _playerStats.TakeDamage(damageDealt);
        UnityEngine.Debug.Log("Should attack player and then end turn");
    }
}

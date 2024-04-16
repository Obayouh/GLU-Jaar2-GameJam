using System.Collections.Generic;
using UnityEngine;

public class TypeChart : MonoBehaviour
{
    public enum Typing
    {
        Heat,
        Water,
        Volt,
        Gaia,
        Neutral,
    }

    public Typing Type;
    public float AmountOfDamage;

    private bool _neutralDamage;
    private bool _veryEffective;
    private bool _resistedDamage;

    struct Effectiveness
    {
        public Typing Card { get; }
        public Typing EnemiesType { get; }

        public Effectiveness(Typing card, Typing enemy)
        {
            Card = card;
            EnemiesType = enemy;
        }
    }

    private Dictionary<Effectiveness, int> _checkPotency = new()
    {
        //Heat combinations:
        {new Effectiveness(Typing.Heat, Typing.Heat), 0 },
        {new Effectiveness(Typing.Heat, Typing.Water), 2 },
        {new Effectiveness(Typing.Heat, Typing.Volt), 0 },
        {new Effectiveness(Typing.Heat, Typing.Gaia), 1 },
        {new Effectiveness(Typing.Heat, Typing.Neutral), 0 },

        //Water combinations:
        {new Effectiveness(Typing.Water, Typing.Heat), 1 },
        {new Effectiveness(Typing.Water, Typing.Water), 0 },
        {new Effectiveness(Typing.Water, Typing.Volt), 2 },
        {new Effectiveness(Typing.Water, Typing.Gaia), 0 },
        {new Effectiveness(Typing.Water, Typing.Neutral), 0 },

        //Volt combinations:
        {new Effectiveness(Typing.Volt, Typing.Heat), 0 },
        {new Effectiveness(Typing.Volt, Typing.Water), 1 },
        {new Effectiveness(Typing.Volt, Typing.Volt), 0 },
        {new Effectiveness(Typing.Volt, Typing.Gaia), 2 },
        {new Effectiveness(Typing.Volt, Typing.Neutral), 0 },

        //Gaia combinations:
        {new Effectiveness(Typing.Gaia, Typing.Heat), 2 },
        {new Effectiveness(Typing.Gaia, Typing.Water), 0 },
        {new Effectiveness(Typing.Gaia, Typing.Volt), 1 },
        {new Effectiveness(Typing.Gaia, Typing.Gaia), 0 },
        {new Effectiveness(Typing.Gaia, Typing.Neutral), 0 },

        //Neutral combinations:
        {new Effectiveness(Typing.Neutral, Typing.Heat), 0 },
        {new Effectiveness(Typing.Neutral, Typing.Water), 0 },
        {new Effectiveness(Typing.Neutral, Typing.Volt), 0 },
        {new Effectiveness(Typing.Neutral, Typing.Gaia), 0 },
        {new Effectiveness(Typing.Neutral, Typing.Neutral), 0 },
    };

    public string GetType(Typing first, Typing second)
    {
        Effectiveness pair = new(first, second);

        if (_checkPotency.TryGetValue(pair, out int action))
        {
            if (action == 0)
            {
                //Normal damage
                _neutralDamage = true;
                return "0";
            }

            if (action == 1)
            {
                //Super effective
                _veryEffective = true;
                return "1";
            }

            if (action == 2)
            {
                //Resist damage
                _resistedDamage = true;
                return "2";
            }

            else
            {
                return null;
            }
        }
        else
        {
            return "Geen actie gevonden";
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Typing type = collision.gameObject.GetComponent<TypeChart>().Type;
        HealthSystem enemyHealth = collision.gameObject.GetComponent<HealthSystem>();

        GetType(this.Type, type);

        if (_neutralDamage)
        {
            //Do neutral damage
            enemyHealth.TakeDamage(AmountOfDamage);
        }

        if (_veryEffective)
        {
            //Do 1.5x damage
            enemyHealth.TakeDamage(AmountOfDamage * 1.5f);
        }

        if (_resistedDamage)
        {
            //Do 0.5x damage
            enemyHealth.TakeDamage(AmountOfDamage * 0.5f);
        }
    }
}

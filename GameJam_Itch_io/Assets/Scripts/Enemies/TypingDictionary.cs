using System.Collections.Generic;
using UnityEngine;

public class TypingDictionary : MonoBehaviour
{
    struct Effectiveness
    {
        public E_ElementalTyping Card { get; }
        public E_ElementalTyping enemyTyping { get; }

        public Effectiveness(E_ElementalTyping card, E_ElementalTyping enemy)
        {
            Card = card;
            enemyTyping = enemy;
        }
    }
    //Returning a 0 = neutral damage, 1 is effective damage, and 2 is resistant damage
    private static Dictionary<Effectiveness, int> _checkPotency = new()
    {
        //Fire combinations:
        {new Effectiveness( E_ElementalTyping.Fire,  E_ElementalTyping.Fire), 0 },
        {new Effectiveness( E_ElementalTyping.Fire,  E_ElementalTyping.Water), 2 },
        {new Effectiveness( E_ElementalTyping.Fire,  E_ElementalTyping.Lightning), 0 },
        {new Effectiveness( E_ElementalTyping.Fire,  E_ElementalTyping.Rock), 1 },
        {new Effectiveness( E_ElementalTyping.Fire,  E_ElementalTyping.Neutral), 0 },

        //Water combinations:
        {new Effectiveness( E_ElementalTyping.Water,  E_ElementalTyping.Fire), 1 },
        {new Effectiveness( E_ElementalTyping.Water,  E_ElementalTyping.Water), 0 },
        {new Effectiveness( E_ElementalTyping.Water,  E_ElementalTyping.Lightning), 2 },
        {new Effectiveness( E_ElementalTyping.Water,  E_ElementalTyping.Rock), 0 },
        {new Effectiveness( E_ElementalTyping.Water,  E_ElementalTyping.Neutral), 0 },

        //Lightning combinations:
        {new Effectiveness( E_ElementalTyping.Lightning,  E_ElementalTyping.Fire), 0 },
        {new Effectiveness( E_ElementalTyping.Lightning,  E_ElementalTyping.Water), 1 },
        {new Effectiveness( E_ElementalTyping.Lightning,  E_ElementalTyping.Lightning), 0 },
        {new Effectiveness( E_ElementalTyping.Lightning,  E_ElementalTyping.Rock), 2 },
        {new Effectiveness( E_ElementalTyping.Lightning,  E_ElementalTyping.Neutral), 0 },

        //Rock combinations:
        {new Effectiveness( E_ElementalTyping.Rock,  E_ElementalTyping.Fire), 2 },
        {new Effectiveness( E_ElementalTyping.Rock,  E_ElementalTyping.Water), 0 },
        {new Effectiveness( E_ElementalTyping.Rock,  E_ElementalTyping.Lightning), 1 },
        {new Effectiveness( E_ElementalTyping.Rock,  E_ElementalTyping.Rock), 0 },
        {new Effectiveness( E_ElementalTyping.Rock,  E_ElementalTyping.Neutral), 0 },

        //Neutral combinations:
        {new Effectiveness( E_ElementalTyping.Neutral,  E_ElementalTyping.Fire), 0 },
        {new Effectiveness( E_ElementalTyping.Neutral,  E_ElementalTyping.Water), 0 },
        {new Effectiveness( E_ElementalTyping.Neutral,  E_ElementalTyping.Lightning), 0 },
        {new Effectiveness( E_ElementalTyping.Neutral,  E_ElementalTyping.Rock), 0 },
        {new Effectiveness( E_ElementalTyping.Neutral,  E_ElementalTyping.Neutral), 0 },
    };

    /// <summary>
    /// Grabs playercard and enemy elemental typing, compares values in the dictionary, and returns the corresponding value in the dictiionary; 
    /// that value is used later (In ClickManagerPrime script) to check how much damage needs to be dealt to the enemy
    /// </summary>
    /// <param name="playerCard"></param>
    /// <param name="targetEnemy"></param>
    /// <returns></returns>
    public static int GetEffectivenessModifier(E_ElementalTyping playerCard, E_ElementalTyping targetEnemy)
    {
        foreach (KeyValuePair<Effectiveness, int> compareEffectiveness in _checkPotency)
        {
            if (compareEffectiveness.Key.Card == playerCard && compareEffectiveness.Key.enemyTyping == targetEnemy)
            {
                return compareEffectiveness.Value;
            }
        }
        Debug.LogError("The playercard and targetEnemy combo has no match!");
        return 0;
    }
}

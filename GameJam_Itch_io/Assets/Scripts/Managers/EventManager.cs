using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void OnCardPlayed(CardStats cardStats, HealthSystem enemyTarget);

    public event OnCardPlayed useCardEvent;

    public delegate void OnNextCardPlayed(CardStats cardStats, HealthSystem enemyTarget);

    public event OnNextCardPlayed useNextCardEvent;

    public void CardPlayed(CardStats cardStats, HealthSystem enemyTarget)
    {
        useCardEvent?.Invoke(cardStats, enemyTarget);
    }

    public void NextCardPlayed(CardStats cardStats, HealthSystem enemyTarget)
    {
        useNextCardEvent?.Invoke(cardStats, enemyTarget);

    }
}

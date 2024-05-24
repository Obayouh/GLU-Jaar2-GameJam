using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void OnCardPlayed(CardStats cardStats, HealthSystem enemyTarget);

    public event OnCardPlayed useCardEvent;

    public void CardPlayed(CardStats cardStats, HealthSystem enemyTarget)
    {
        useCardEvent?.Invoke(cardStats, enemyTarget);
    }
}

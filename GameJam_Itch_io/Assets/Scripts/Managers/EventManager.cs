using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void OnCardPlayed(CardStats cardStats, HealthSystem enemyTarget);

    public event OnCardPlayed useCardEvent;
}

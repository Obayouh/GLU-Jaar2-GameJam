using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceInstance : MonoBehaviour
{
    public static ReferenceInstance refInstance { get; private set; }

    public PlayerStats playerStats;
    public TurnManager turnManager;
    public CardManager cardManager;

    void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        turnManager = GetComponent<TurnManager>();
        cardManager = GetComponent<CardManager>();
    }
}

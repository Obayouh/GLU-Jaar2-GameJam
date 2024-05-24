using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceInstance : MonoBehaviour
{
    public static ReferenceInstance refInstance;

    public PlayerStats playerStats;
    public TurnManager turnManager;
    public CardManager cardManager;
    public ClickManagerPrime clickManager;
    public EnemyController enemyController;
    public EventManager eventManager;
    public CameraMovement cam;

    void Awake()
    {
        refInstance = this;

        playerStats = FindFirstObjectByType<PlayerStats>();
        turnManager = GetComponentInChildren<TurnManager>();
        cardManager = GetComponentInChildren<CardManager>();
        clickManager = GetComponentInChildren<ClickManagerPrime>();
        enemyController = GetComponentInChildren<EnemyController>();
        eventManager = GetComponentInChildren<EventManager>();
        cam = FindFirstObjectByType<CameraMovement>();
    }
}

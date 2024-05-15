using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceInstance : MonoBehaviour
{
    public static ReferenceInstance refInstance;

    public PlayerStats playerStats;
    public TurnManager turnManager;
    public CardManager cardManager;
    public EnemyController enemyController;
    public CameraMovement cam;

    void Awake()
    {
        refInstance = this;

        playerStats = FindFirstObjectByType<PlayerStats>();
        turnManager = GetComponentInChildren<TurnManager>();
        cardManager = GetComponentInChildren<CardManager>();
        enemyController = GetComponentInChildren<EnemyController>();
        cam = FindFirstObjectByType<CameraMovement>();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnState
{
    PlayerTurn,
    PickCard,
    Waiting,
    Attack,
    EnemyTurn
}

public class TurnManager : MonoBehaviour
{
    private TurnState state;

    public bool AddNewCards;

    public GameObject endTurnButton;

    private SpawnEnemies spawnEnemiesScript;

    private void Start()
    {
        StartCoroutine(StartPlayerTurn(1f));
        spawnEnemiesScript = FindFirstObjectByType<SpawnEnemies>();
    }

    private void Update()
    {

    }

    private IEnumerator StartPlayerTurn(float amount)
    {
        ReferenceInstance.refInstance.playerStats.RefillMana();
        yield return new WaitForSeconds(amount);
        ChangeState(TurnState.PickCard);
        ReferenceInstance.refInstance.cardManager.AddCards();
    }

    private void StartEnemyTurn()
    {
        for (int i = 0; i < spawnEnemiesScript.spawnedEnemies.Count; i++)
        {
            spawnEnemiesScript.spawnedEnemies[i].GetComponent<Ab_Enemy>().CheckForStatusEffects();
        }

        ChangeState(TurnState.EnemyTurn);
        ReferenceInstance.refInstance.enemyController.StartEnemyActions();
    }

    //Button to end player turn
    public void EndTurn()
    {
        StartEnemyTurn();
    }

    public void ChangeState(TurnState newState)
    {
        state = newState;
        ReferenceInstance.refInstance.cam.CheckState();
        if (state == TurnState.PickCard)
        {
            endTurnButton.SetActive(true);
        }
        else if (state == TurnState.PlayerTurn)
        {
            AddNewCards = true;

            //If all enemies are killed, spawn new wave
            if (spawnEnemiesScript.spawnedEnemies.Count < 1)
            {
                StartCoroutine(spawnEnemiesScript.InstantiateEnemies());
            }

            StartCoroutine(StartPlayerTurn(2f));
        }
        else
        {
            endTurnButton.SetActive(false);
        }
    }

    public TurnState ReturnState()
    {
        return state;
    }
}

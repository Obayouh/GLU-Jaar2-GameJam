using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [SerializeField] private TextMeshProUGUI _CurrentWaveText;
    [SerializeField] private TextMeshProUGUI _CurrentTurnText;
    private int _waveNumber = 1;
    private int _turnNumber = 1;

    private void Start()
    {
        StartCoroutine(StartPlayerTurn(1f));
        spawnEnemiesScript = FindFirstObjectByType<SpawnEnemies>();
        UpdateWave();
    }

    private void UpdateWave()
    {
        if (_CurrentWaveText == null)
            return;

        _CurrentWaveText.text = "Current wave:  " + _waveNumber++;
    }

    private void UpdateTurn()
    {
        if (_CurrentTurnText == null)
            return;

        _CurrentTurnText.text = "Current turn:    " + _turnNumber++;
    }

    private IEnumerator StartPlayerTurn(float amount)
    {
        ReferenceInstance.refInstance.playerStats.RefillMana();
        UpdateTurn();
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
                UpdateWave();
                StartCoroutine(spawnEnemiesScript.InstantiateEnemies());
            }

            StartCoroutine(StartPlayerTurn(1f));
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

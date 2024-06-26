using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static EventManager;

public enum TurnState
{
    PlayerTurn,
    PickCard,
    Waiting,
    Attack,
    EnemyTurn,
    PickNewCard
}

public class TurnManager : MonoBehaviour
{
    private TurnState state;

    public bool AddNewCards;
    public bool playerTurn;


    private int _floorNumber = 1;
    private int _turnNumber = 1;

    private SpawnEnemies spawnEnemiesScript;
    private Ab_Enemy enemyStats;
    [SerializeField] private CanvasCollector _CanvasCollector;

    private void Start()
    {
        StartCoroutine(StartPlayerTurn(1f));
        spawnEnemiesScript = FindFirstObjectByType<SpawnEnemies>();
        _CanvasCollector = FindFirstObjectByType<CanvasCollector>();
        enemyStats = spawnEnemiesScript.spawnedEnemies[0].GetComponent<Ab_Enemy>();
        UpdateFloor();
    }

    private void UpdateFloor()
    {
        enemyStats.UpdateDamage();

        if (_CanvasCollector.CurrentFloor == null)
            return;
        _CanvasCollector.CurrentFloor.text = "Current floor:  " + _floorNumber++;
    }

    private void UpdateTurn()
    {

        if (_CanvasCollector.CurrentTurn == null)
            return;
        _CanvasCollector.CurrentTurn.text = "Current turn:   " + _turnNumber++;
    }

    private IEnumerator StartPlayerTurn(float amount)
    {
        playerTurn = true;
        ReferenceInstance.refInstance.playerStats.RefillMana();
        ReferenceInstance.refInstance.playerStats.hasShield = false;
        UpdateTurn();
        yield return new WaitForSeconds(amount);
        ChangeState(TurnState.PickCard);
        ReferenceInstance.refInstance.cardManager.AddCards();
    }

    private void StartEnemyTurn()
    {
        playerTurn = false;
        ReferenceInstance.refInstance.clickManager.UnsubscribeCardPlayed();
        ReferenceInstance.refInstance.clickManager.UnsubscribeExtraCardEffects();

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
            _CanvasCollector.EndTurnButton.SetActive(true);
        }
        else
        {
            _CanvasCollector.EndTurnButton.SetActive(false);
        }

        if (state == TurnState.Attack)
        {
            ReferenceInstance.refInstance.spawnEnemiesScript.StartEnemiesSOH();
        }
        else
        {
            ReferenceInstance.refInstance.spawnEnemiesScript.StopEnemiesSOH();
        }

        if (state == TurnState.PlayerTurn)
        {
            AddNewCards = true;

            StartCoroutine(StartPlayerTurn(1f));
        }
    }

    public void StartNextWave()
    {
        StartCoroutine(LoadNextWave());
    }

    public TurnState ReturnState()
    {
        return state;
    }

    public IEnumerator LoadNextWave()
    {
        _CanvasCollector.CrossfadeAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        _CanvasCollector.CrossfadeAnimator.SetTrigger("End");
        yield return new WaitForSeconds(0.25f);
        UpdateFloor();
        StartCoroutine(spawnEnemiesScript.InstantiateEnemies());
        ChangeState(TurnState.PlayerTurn);
    }
}

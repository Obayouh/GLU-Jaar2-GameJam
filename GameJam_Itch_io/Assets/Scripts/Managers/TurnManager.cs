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
    PickNewCard,
    GameOver
}

public class TurnManager : MonoBehaviour
{
    private TurnState state;

    public bool AddNewCards;
    public bool playerTurn;

    public int _floorNumber = 1;

    private SpawnEnemies spawnEnemiesScript;
    private Ab_Enemy enemyStats;
    PickNewCardSystem pickNewCard;
    BattleText battleText;
    [SerializeField] private CanvasCollector _CanvasCollector;

    private void Start()
    {
        StartCoroutine(StartPlayerTurn(1f));
        spawnEnemiesScript = FindFirstObjectByType<SpawnEnemies>();
        pickNewCard = FindFirstObjectByType<PickNewCardSystem>();
        battleText = FindFirstObjectByType<BattleText>();
        enemyStats = spawnEnemiesScript.spawnedEnemies[0].GetComponent<Ab_Enemy>();
        UpdateFloor();
        _CanvasCollector = FindFirstObjectByType<CanvasCollector>();
    }

    private void UpdateFloor()
    {
        enemyStats.UpdateDamage();

        if (_CanvasCollector.CurrentFloor == null)
            return;
        _CanvasCollector.CurrentFloor.text = "Current floor:  " + _floorNumber++;
    }

    private IEnumerator StartPlayerTurn(float amount)
    {
        playerTurn = true;
        ReferenceInstance.refInstance.playerStats.RefillMana();
        ReferenceInstance.refInstance.playerStats.hasShield = false;
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

        battleText.ChangeText(state);

        CheckForTurnState();
    }

    private void CheckForTurnState()
    {
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
        else if (state == TurnState.PickNewCard)
        {
            pickNewCard.PickCard();
        }

        if (state == TurnState.GameOver)
        {
            GameOver gameOver = FindFirstObjectByType<GameOver>();
            gameOver.ShowGameOver();
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
        _CanvasCollector.StairSFX.Play();
        yield return new WaitForSeconds(3f);
        _CanvasCollector.CrossfadeAnimator.SetTrigger("End");
        yield return new WaitForSeconds(0.25f);
        UpdateFloor();
        ReferenceInstance.refInstance.floorManager.ChangeRoom();
        StartCoroutine(spawnEnemiesScript.InstantiateEnemies());
        ChangeState(TurnState.PlayerTurn);
    }
}

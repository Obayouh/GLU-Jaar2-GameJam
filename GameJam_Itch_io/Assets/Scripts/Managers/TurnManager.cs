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

    private void Start()
    {
        StartCoroutine(StartPlayerTurn(1f));
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

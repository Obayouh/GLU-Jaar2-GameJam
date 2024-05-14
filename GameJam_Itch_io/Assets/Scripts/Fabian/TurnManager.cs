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

    public bool FirstEnemyGo;
    public bool SecondEnemyGo;
    public bool LastEnemyGo;

    public bool AddNewCards;

    public GameObject endTurnButton;

    private EnemyController enemyController;

    private void Awake()
    {
        
    }

    private void Start()
    {
        enemyController = FindAnyObjectByType<EnemyController>();

        StartCoroutine(StartPlayerTurn(1f));
    }

    private IEnumerator StartPlayerTurn(float amount)
    {
        ReferenceInstance.refInstance.playerStats.RefillMana();
        yield return new WaitForSeconds(amount);
        ChangeState(TurnState.PickCard);
        if (AddNewCards)
        {
            //cardManager.AddCards();
            ReferenceInstance.refInstance.cardManager.AddCards();
            AddNewCards = false;
        }
    }

    private void StartEnemyTurn()
    {
        ChangeState(TurnState.EnemyTurn);
        enemyController.StartEnemyActions();
    }

    public void EndTurn()
    {
        StartEnemyTurn();
    }

    public void ChangeState(TurnState newState)
    {
        state = newState;
        CameraMovement cam = FindAnyObjectByType<CameraMovement>();
        cam.CheckState();
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

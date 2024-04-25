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
    public TurnState state;

    public bool FirstEnemyGo;
    public bool SecondEnemyGo;
    public bool LastEnemyGo;

    public bool AddNewCards;

    public GameObject endTurnButton;

    private CardManager cardManager;
    private ClickManagerPrime clickMananger;

    private void Start()
    {
        cardManager = FindObjectOfType<CardManager>();
        clickMananger = FindObjectOfType<ClickManagerPrime>();

        StartCoroutine(StartPlayerTurn(1f));
    }

    private IEnumerator StartPlayerTurn(float amount)
    {
        yield return new WaitForSeconds(amount);
        ChangeState(TurnState.PickCard);
        if (AddNewCards)
        {
            cardManager.AddCards();
            AddNewCards = false;
        }
    }

    private void StartEnemyTurn()
    {
        ChangeState(TurnState.EnemyTurn);
        FirstEnemyGo = true;
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
}

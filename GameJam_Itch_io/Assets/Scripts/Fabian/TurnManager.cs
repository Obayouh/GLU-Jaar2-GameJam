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

    //[SerializeField] private CardManager cardManager;
    //[SerializeField] private PlayerStats playerStats;

    private void Awake()
    {
        
    }

    private void Start()
    {
        //if (cardManager == null)
        //{
        //    Debug.Log("Fill in the CardManager field in the TurnManager next time!");
        //    cardManager = FindFirstObjectByType<CardManager>();
        //}
        //if (playerStats == null)
        //{
        //    Debug.Log("Fill in the PlayerStats field in the ClickManager next time!");
        //    playerStats = FindFirstObjectByType<PlayerStats>();
        //}
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnState
{
    PlayerTurn,
    Waiting,
    EnemyTurn
}

public class TurnManager : MonoBehaviour
{
    public TurnState state;

    public bool FirstEnemyGo;
    public bool SecondEnemyGo;
    public bool LastEnemyGo;

    public bool AddNewCards;

    [SerializeField] private CardManager cardManager;

    void Update()
    {
        if (state == TurnState.EnemyTurn)
        {
            FirstEnemyGo = true;
        }

        if (state == TurnState.PlayerTurn && AddNewCards)
        {
            cardManager.AddCards();
            AddNewCards = false;
        }
    }
}

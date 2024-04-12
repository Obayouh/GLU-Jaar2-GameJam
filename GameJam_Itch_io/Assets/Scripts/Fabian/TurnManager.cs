using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public bool PlayersTurn = true;
    public bool EnemiesTurn = false;

    public bool FirstEnemyGo;
    public bool SecondEnemyGo;
    public bool LastEnemyGo;

    public bool AddNewCards;

    [SerializeField] private CardManager cardManager;

    void Update()
    {
        if (EnemiesTurn == true)
        {
            FirstEnemyGo = true;
            EnemiesTurn = false;
        }

        if (PlayersTurn && AddNewCards)
        {
            cardManager.AddCards();
            AddNewCards = false;
        }
    }
}

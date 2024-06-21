using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;

    public TextMeshProUGUI scoreText;

    TurnManager turnManager;

    private void Start()
    {
        gameOverScreen.SetActive(false);

        turnManager = ReferenceInstance.refInstance.turnManager;
    }

    public void ShowGameOver()
    {
        gameOverScreen.SetActive(true);

        scoreText.text = "You Cleared " + (turnManager._floorNumber - 2) + " Floor(s)";
    }

    //Restart button
    public void Restart()
    {
        //Code om naar het main menu te gaan
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Debug.Log("Restart Game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

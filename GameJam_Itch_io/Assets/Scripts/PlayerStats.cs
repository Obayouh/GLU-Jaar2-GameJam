using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public GameObject gameOverScreen;

    CameraMovement cam;

    void Start()
    {
        cam = FindAnyObjectByType<CameraMovement>();

        currentHealth = maxHealth;

        gameOverScreen.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        cam.startShaking = true;

        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        cam.gameOver = true;

        gameOverScreen.SetActive(true);
    }
}

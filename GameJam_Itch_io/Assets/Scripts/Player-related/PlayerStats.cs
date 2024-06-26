using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerStats : Ab_HealthManager
{
    [SerializeField] private int currentMana;
    [SerializeField, Range(6, 100)] private int totalMana;

    [SerializeField] private int score;
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private GameObject gameOverScreen;

    private CameraMovement cam;

    [SerializeField] private PlayerHealthBar healthBar;

    public override void Start()
    {
        base.Start();
        cam = FindObjectOfType<CameraMovement>();
        RefillMana();

        if (healthBar == null)
        {
            Debug.Log("Fill playerhealthbar in on Player");
        }
    }

    public override void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        cam.StartShaking();
        healthBar.UpdateHealthBar(CurrentHealth, maxHealth);
    }

    public override void Kill()
    {
        base.Kill();
        //Execute gameover screen
        if (gameOverScreen != null)
        {
            Instantiate(gameOverScreen);
        }
    }

    public void RefillMana()
    {
        currentMana = totalMana;
    }

    public void LoseMana(int cardManaCost)
    {
        currentMana -= cardManaCost;
        //Debug.Log("You have " + currentMana + " mana remaining");
    }

    public int ReturnPlayerMana()
    {
        return currentMana;
    }
}

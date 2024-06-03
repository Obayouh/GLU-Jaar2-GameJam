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

    public override void Start()
    {
        base.Start();
        cam = FindObjectOfType<CameraMovement>();
        RefillMana();
    }

    public override void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        cam.StartShaking();
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

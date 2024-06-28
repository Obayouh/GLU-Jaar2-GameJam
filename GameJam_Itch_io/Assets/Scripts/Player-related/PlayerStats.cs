using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerStats : Ab_HealthManager
{
    [SerializeField] private int currentMana;
    [SerializeField, Range(6, 100)] private int totalMana;

    private CameraMovement cam;

    bool dead;

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
        if (dead) return;
        CurrentHealth -= amount;
        cam.StartShaking();
        healthBar.UpdateHealthBar(CurrentHealth, maxHealth);

        if (CurrentHealth <= 0)
        {
            Kill();
        }
    }

    public override void Kill()
    {
        base.Kill();

        dead = true;

        AudioManager.Instance.Play("Player Died");

        ReferenceInstance.refInstance.turnManager.ChangeState(TurnState.GameOver);
    }

    public void RefillMana()
    {
        currentMana = totalMana;
    }

    public void LoseMana(int cardManaCost)
    {
        currentMana -= cardManaCost;

        if (currentMana <= 0)
        {
            BattleText battleText = FindFirstObjectByType<BattleText>();
            battleText.OutOfManaText(2);
        }
    }

    public int ReturnPlayerMana()
    {
        return currentMana;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerStats : Ab_HealthManager
{

    private int manaCost;
    [SerializeField] private int currentMana;
    [SerializeField, Range(6, 10)] private int totalMana;

    [SerializeField] private int score;
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private GameObject gameOverScreen;

    private CameraMovement cam;

    private CardStats cardStats;

    public override void Start()
    {
        base.Start();
        cam = FindObjectOfType<CameraMovement>();
        RefillMana();
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Rooms cleared: " + score.ToString();
        Animator anim = scoreText.GetComponent<Animator>();
        anim.SetBool("PlayAnim", true);
        StartCoroutine(Stop());
    }

    public override void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
    }

    public override void Kill()
    {
        base.Kill();
        //Execute gameover screen
    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(.5f);
        Animator anim = scoreText.GetComponent<Animator>();
        anim.SetBool("PlayAnim", false);
    }

    public void RefillMana()
    {
        currentMana = totalMana;
    }

    public void LoseMana(int cardManaCost)
    {
        currentMana -= cardManaCost;
        Debug.Log("You have " + currentMana + " mana remaining");
    }

    public int ReturnPlayerMana()
    {
        return currentMana;
    }
}

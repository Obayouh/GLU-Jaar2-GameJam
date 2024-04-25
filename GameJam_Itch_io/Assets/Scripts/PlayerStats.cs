using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    [SerializeField] private int currentCost;
    private int costAmount;
    [SerializeField, Range(8, 10)] private int totalCost;

    public int score;
    public TextMeshProUGUI scoreText;

    public GameObject gameOverScreen;

    CameraMovement cam;

    private CardStats cardStats;

    void Start()
    {
        cam = FindAnyObjectByType<CameraMovement>();

        currentHealth = maxHealth;

        currentCost = totalCost;
    }

    void Update()
    {

    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Rooms cleared: " + score.ToString();
        Animator anim = scoreText.GetComponent<Animator>();
        anim.SetBool("PlayAnim", true);
        StartCoroutine(Stop());
    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(.5f);
        Animator anim = scoreText.GetComponent<Animator>();
        anim.SetBool("PlayAnim", false);
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

    public void CheckIfUsable()
    {
        if (cardStats.ReturnCost() > currentCost)
        {
            Debug.Log("You don't have enough points to use that card");
            return;
        }
    }

    public void ThrowCard()
    {
        
    }
}

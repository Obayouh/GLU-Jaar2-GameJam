using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public int score;
    public TextMeshProUGUI scoreText;

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

        if (Input.GetKeyDown(KeyCode.W))
        {
            AddScore(100);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
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
}

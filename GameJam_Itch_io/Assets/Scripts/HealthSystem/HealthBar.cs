using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    //[SerializeField] private Slider healthSlider;
    //[SerializeField] private Slider easeHealthSlider;
    [SerializeField] private Image HealthFill;
    [SerializeField] private Image HealthFull;
    [SerializeField, Range(0.001f, 0.05f)] private float lerpSpeed = 0.05f;
    private HealthSystem healthComponent;
    [SerializeField] private TextMeshProUGUI healthText;

    private void Start()
    {
        healthComponent = GetComponentInParent<HealthSystem>();

        if (healthComponent != null)
        {
            InitializeHealthBar();
        }
        else
        {
            Debug.Log("Enemy health bar component is not filled");
        }
    }

    private void Update()
    {
        if (HealthFull.fillAmount != HealthFill.fillAmount)
        {
            HealthFull.fillAmount = Mathf.Lerp(HealthFull.fillAmount,HealthFill.fillAmount, lerpSpeed); //Visually update this over time to showcase damage taken
        }
    }

    private void InitializeHealthBar()
    {
        HealthFill.fillAmount = healthComponent.GetMaxHealth();
        HealthFull.fillAmount = healthComponent.GetMaxHealth();
        healthText.text = healthComponent.GetMaxHealth() + " / " + healthComponent.GetMaxHealth();
    }

    //Call this function or the "HealthUpdate" function in HealthSystem anytime you need to update the healthbar visually
    public void UpdateHealthBar(float currentHealth, float maxHealth) 
    {
        float healthPercentage = currentHealth / maxHealth;
        if (currentHealth != HealthFill.fillAmount)
        {
            if (healthComponent.GetMaxHealth() == 10)
            {
                HealthFill.fillAmount = currentHealth / 10;
            }

            if (healthComponent.GetMaxHealth() == 15)
            {
                HealthFill.fillAmount = currentHealth / 15;
            }
        }
        healthText.text = currentHealth + " / " + maxHealth;
    }
}

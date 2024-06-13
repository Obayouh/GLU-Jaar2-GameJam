using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{

    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider easeHealthSlider;
    [SerializeField, Range(0.001f, 0.05f)] private float lerpSpeed = 0.05f;
    [SerializeField] private PlayerStats healthComponent;
    [SerializeField] private TextMeshProUGUI healthText;

    [SerializeField] private Image healthFillImage;

    [SerializeField] private Color fullHealth;

    [SerializeField] private Color halfHealth;

    [SerializeField] private Color nearDeath;

    private void Start()
    {

        if (healthComponent != null)
        {
            StartCoroutine(IniPlayerHealth());
            //healthComponent.OnHealthChanged += UpdateHealthBar;
        }
        else
        {
            Debug.Log("Player health bar component is not filled");
        }
    }

    private void Update()
    {
        if (easeHealthSlider.value != healthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, healthSlider.value, lerpSpeed); //Visually update this over time to showcase damage taken
        }

        if ((healthSlider.value / healthSlider.maxValue) * 100 > 90)
        {
            healthFillImage.color = fullHealth;
        }
        else if ((healthSlider.value / healthSlider.maxValue) * 100 < 90 && (healthSlider.value / healthSlider.maxValue) * 100 > 50)
        {
            healthFillImage.color = halfHealth;
        }
        else if ((healthSlider.value / healthSlider.maxValue) * 100 <= 50)
        {
            healthFillImage.color = nearDeath;
        }
    }

    private void InitializeHealthBar()
    {
        healthSlider.maxValue = healthComponent.CurrentHealth;
        easeHealthSlider.maxValue = healthComponent.CurrentHealth;
        healthSlider.value = healthComponent.CurrentHealth;
        easeHealthSlider.value = healthComponent.CurrentHealth;
        healthText.text = "Player: " + healthComponent.CurrentHealth + " / " + healthComponent.CurrentHealth;
    }

    //Has minor delay to avoid having wrong amount of health on startup due to currnthealth vs maxhealth calculations
    IEnumerator IniPlayerHealth()
    {
        yield return new WaitForSeconds(1.5f);

        InitializeHealthBar();
    }

    //Call this function or the "HealthUpdate" function in HealthSystem anytime you need to update the healthbar visually
    public void UpdateHealthBar(float currentHealth, float maxHealth) 
    {
        float healthPercentage = currentHealth / maxHealth;
        if (currentHealth != healthSlider.value)
        {
            healthSlider.value = currentHealth;
        }
        healthText.text = currentHealth + " / " + maxHealth;
        //Debug.Log("Health Bar Updated: " + healthPercentage);
    }
}

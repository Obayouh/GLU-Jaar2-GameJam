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
    [SerializeField, Range(0.001f, 0.05f)] private float lerpSpeed = 0.05f;
    private HealthSystem healthComponent;
    [SerializeField] private TextMeshProUGUI healthText;

    private void Start()
    {
        healthComponent = GetComponentInParent<HealthSystem>();

        if (healthComponent != null)
        {
            InitializeHealthBar();
            //healthComponent.OnHealthChanged += UpdateHealthBar;
        }
        else
        {
            Debug.Log("Enemy health bar component is not filled");
        }
    }

    private void Update()
    {
        //if (easeHealthSlider.value != /*healthSlider.value*/ HealthFill.fillAmount)
        //{
        //    easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, /*healthSlider.value*/ HealthFill.fillAmount, lerpSpeed); //Visually update this over time to showcase damage taken
        //}
    }

    private void InitializeHealthBar()
    {
        //healthSlider.maxValue = healthComponent.GetMaxHealth();
        HealthFill.fillAmount = healthComponent.GetMaxHealth();
        //easeHealthSlider.maxValue = healthComponent.GetMaxHealth();
        //healthSlider.value = healthComponent.GetMaxHealth();
        //easeHealthSlider.value = healthComponent.GetMaxHealth();
        healthText.text = healthComponent.GetMaxHealth() + " / " + healthComponent.GetMaxHealth();
    }

    //Call this function or the "HealthUpdate" function in HealthSystem anytime you need to update the healthbar visually
    public void UpdateHealthBar(float currentHealth, float maxHealth) 
    {
        float healthPercentage = currentHealth / maxHealth;
        if (currentHealth != /*healthSlider.value*/ HealthFill.fillAmount)
        {
            /*healthSlider.value*/ HealthFill.fillAmount = currentHealth / 10;
        }
        healthText.text = currentHealth + " / " + maxHealth;
        //Debug.Log("Health Bar Updated: " + healthPercentage);
    }
}

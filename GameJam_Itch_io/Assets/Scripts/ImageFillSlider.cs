using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class ImageFillSlider : MonoBehaviour
{
    [SerializeField] private Image manaImage;

    private float playerMana;

    //public Slider slider;

    //[SerializeField] private float value;

    private TextMeshProUGUI manaText;

    private void Awake()
    {
        manaText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        playerMana = ReferenceInstance.refInstance.playerStats.ReturnPlayerMana();
        UpdateFillAmount();
    }
    private void Update()
    {
        playerMana = ReferenceInstance.refInstance.playerStats.ReturnPlayerMana();
        //playerMana = slider.value;
        manaText.text = playerMana.ToString();
    }


    public void UpdateFillAmount()
    {
        manaImage.fillAmount = (float)playerMana / 100;
    }
}

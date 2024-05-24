using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeckUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI drawPileAmount;
    [SerializeField] private TextMeshProUGUI discardPileAmount;

    public void UpdateDrawPile(int amount)
    {
        drawPileAmount.text = amount.ToString();
    }

    public void UpdateDiscardPile(int amount)
    {
        discardPileAmount.text = amount.ToString();
    }
}

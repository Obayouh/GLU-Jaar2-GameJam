using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ab_PlayerCards : MonoBehaviour
{
    [SerializeField, Range(1,10)] protected int cardManaCost;
    [SerializeField] protected int cardSellCost;
    [SerializeField] protected int cardBuyCost;
    protected E_ElementalTyping cardTyping;

    public virtual void Start()
    {
        cardSellCost = Mathf.RoundToInt(cardBuyCost / 2);
    }

    public virtual void Update()
    {
        
    }
    /// <summary>
    /// Certain cost limits depended on the card's strength and extra effects
    /// </summary>
    public virtual void CardCostLimits()
    {
        if (cardTyping == E_ElementalTyping.Fire)
        {
            cardManaCost = Mathf.Clamp(cardManaCost, 2, 4);
        }
        else if (cardTyping == E_ElementalTyping.Water)
        {
            cardManaCost = Mathf.Clamp(cardManaCost, 1, 3);
        }
        else if (cardTyping == E_ElementalTyping.Lightning)
        {
            cardManaCost = Mathf.Clamp(cardManaCost, 3, 5);
        }
        else if (cardTyping == E_ElementalTyping.Rock)
        {
            cardManaCost = Mathf.Clamp(cardManaCost, 2, 5);
        }
    }
}

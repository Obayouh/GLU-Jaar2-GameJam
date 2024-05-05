using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class CardShop : MonoBehaviour
{
    [SerializeField, Range(10, 100)] private int attackCardCost;
    [SerializeField, Range(10, 100)] private int supportCardCost;
    [SerializeField, Range(10, 100)] private int discardCardCost;

    private int playerCurrencyAmount; //mostly here as placeholder until actual player currency exists

    [SerializeField] private List<GameObject> attackCardsList = new();
    [SerializeField] private List<GameObject> supportCardsList = new();

    private bool isShopActive;
    void Start()
    {
        //null ref check to see if the shop is filled with cards or not
        if (attackCardsList == null || supportCardsList == null)
        {
            Debug.Log("Fill in the shop card lists asap please");
        }
    }

    void Update()
    {
        if (!isShopActive)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void PressedContinue()
    {
        if (isShopActive)
        {
            isShopActive = false;
        }
    }

    public void BuyRandomAttackCard()
    {
        if (playerCurrencyAmount >= attackCardCost)
        {
            //Buy random attack card from premade list
            int randomCard = Random.Range(0, attackCardsList.Count);
            playerCurrencyAmount -= attackCardCost;
            attackCardCost += 10; //Make buying more expensive to prevent player from spam-buying new cards
            //add that card to player deck
        }
        else
        {
            Debug.Log("Sorry, you're too broke to afford this rn, try again later!");
        }
    }

    public void BuyRandomSupportCard()
    {
        if (playerCurrencyAmount >= supportCardCost)
        {
            //Buy random support card from premade list
            int randomCard = Random.Range(0, supportCardsList.Count);
            playerCurrencyAmount -= supportCardCost;
            supportCardCost += 10; //Make buying more expensive to prevent player from spam-buying new cards
            //add that card to player deck
        }
        else
        {
            Debug.Log("Sorry, you're too broke to afford this rn, try again later!");
        }
    }

    public void SellRandomPlayerCard()
    {
        if (playerCurrencyAmount >= discardCardCost)
        {
            //Sell specific player card from the player deck list        
            playerCurrencyAmount -= discardCardCost;
            //remove that card from player deck
        }
        else
        {
            Debug.Log("Sorry, you're too broke to sell stuff rn, try again later!");
        }
    }

}

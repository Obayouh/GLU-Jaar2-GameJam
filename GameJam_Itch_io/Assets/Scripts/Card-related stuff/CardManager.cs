using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<GameObject> spawnedCards = new List<GameObject>();

    [Header("Positions")]
    [SerializeField] private Transform[] cardPositions;
    [SerializeField] private GameObject handPrefab;
    [SerializeField] private Transform[] handPositions;

    int maxAmountOfCards = 4;

    public Transform hand;

    TurnManager turnManager;
    Deck deck;

    void Start()
    {
        turnManager = FindFirstObjectByType<TurnManager>();
        deck = GetComponent<Deck>();

        for (int i = 0; i < maxAmountOfCards; i++)
        {
            GameObject card = Instantiate(deck.DrawCard(), cardPositions[i]);
            spawnedCards.Add(card);
        }
        Instantiate(handPrefab, handPositions[3]);
    }

    public void RemoveCard(GameObject card)
    {
        spawnedCards.Remove(card);
        deck.DiscardCard(card);
    }

    public void AddCards()
    {
        if (spawnedCards.Count < maxAmountOfCards)
        {
            for (int i = 0; i < maxAmountOfCards; i++)
            {
                if (cardPositions[i].childCount <= 0)
                {
                    GameObject card = Instantiate(deck.DrawCard(), cardPositions[i]);
                    spawnedCards.Add(card);
                }
            }
        }
    }

    public void SelectedCard(GameObject chosenCard)
    {
        turnManager.ChangeState(TurnState.Attack);
        ScaleOnHover soh = chosenCard.GetComponent<ScaleOnHover>();
        Destroy(soh);
        GameObject card = chosenCard.transform.parent.parent.gameObject;
        card.transform.parent = hand;
        card.transform.position = hand.position;
        card.transform.rotation = hand.rotation;
    }
}

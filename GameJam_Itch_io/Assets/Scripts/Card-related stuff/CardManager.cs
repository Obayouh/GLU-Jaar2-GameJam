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
            GameObject card = deck.DrawCard();
            card.transform.position = cardPositions[i].position;
            card.transform.rotation = cardPositions[i].rotation;
            card.transform.parent = cardPositions[i];
            card.transform.localScale = new Vector3(.5f, .5f, .5f);
            ScaleOnHover soh = card.GetComponentInChildren<ScaleOnHover>();
            soh.StartHovering();
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
                    GameObject card = deck.DrawCard();
                    card.transform.position = cardPositions[i].position;
                    card.transform.rotation = cardPositions[i].rotation;
                    card.transform.parent = cardPositions[i];
                    card.transform.localScale = new Vector3(.5f, .5f, .5f);
                    ScaleOnHover soh = card.GetComponentInChildren<ScaleOnHover>();
                    soh.StartHovering();
                    spawnedCards.Add(card);
                }
            }
        }
    }

    public void SelectedCard(GameObject chosenCard)
    {
        turnManager.ChangeState(TurnState.Attack);
        ScaleOnHover soh = chosenCard.GetComponent<ScaleOnHover>();
        soh.StopHovering();
        GameObject card = chosenCard.transform.parent.parent.gameObject;
        card.transform.parent = hand;
        card.transform.position = hand.position;
        card.transform.rotation = hand.rotation;
    }
}

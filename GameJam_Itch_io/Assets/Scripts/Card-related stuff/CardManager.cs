using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject[] cards;
    public List<GameObject> spawnedCards = new List<GameObject>();
    public Transform[] cardPositions;

    int maxAmountOfCards = 4;

    void Start()
    {
        for (int i = 0; i < maxAmountOfCards; i++)
        {
            int rdm = Random.Range(0, cards.Length);
            GameObject card = Instantiate(cards[rdm], cardPositions[i]);
            spawnedCards.Add(card);
        }
    }

    public void RemoveCard(GameObject card)
    {
        spawnedCards.Remove(card);
    }

    public void AddCards()
    {
        if (spawnedCards.Count < maxAmountOfCards)
        {
            for (int i = 0; i < maxAmountOfCards; i++)
            {
                if (cardPositions[i].childCount >= 0)
                {
                    int rdm = Random.Range(0, cards.Length);
                    GameObject card = Instantiate(cards[rdm], cardPositions[i]);
                    spawnedCards.Add(card);
                }
            }
        }
    }
}

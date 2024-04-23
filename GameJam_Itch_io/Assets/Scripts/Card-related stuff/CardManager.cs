using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject[] cardPrefabs;
    public List<GameObject> spawnedCards = new List<GameObject>();
    public Transform[] cardPositions;

    int maxAmountOfCards = 4;

    public GameObject currentCard;
    Vector3 startPos;

    void Start()
    {
        for (int i = 0; i < maxAmountOfCards; i++)
        {
            int rdm = Random.Range(0, cardPrefabs.Length);
            GameObject card = Instantiate(cardPrefabs[rdm], cardPositions[i]);
            spawnedCards.Add(card);
        }
        SelectedCard(spawnedCards[1]);

        Invoke("SwitchCard", 5f);
    }

    public void SwitchCard()
    {
        SelectedCard(spawnedCards[2]);
    }

    public void RemoveCard(GameObject card)
    {
        spawnedCards.Remove(card);
        Destroy(card);
    }

    public void AddCards()
    {
        if (spawnedCards.Count < maxAmountOfCards)
        {
            for (int i = 0; i < maxAmountOfCards; i++)
            {
                if (cardPositions[i].childCount <= 0)
                {
                    int rdm = Random.Range(0, cardPrefabs.Length);
                    GameObject card = Instantiate(cardPrefabs[rdm], cardPositions[i]);
                    spawnedCards.Add(card);
                }
            }
        }
    }

    public void SelectedCard(GameObject card)
    {
        Debug.Log("test");

        if (currentCard == null)
        {
            currentCard = card;
        }

        if (currentCard != card)
        {
            currentCard.transform.position = startPos;
        }

        currentCard = card;

        startPos = card.transform.position;
        card.transform.position += new Vector3(card.transform.position.x, card.transform.position.y, 1);
    }
}

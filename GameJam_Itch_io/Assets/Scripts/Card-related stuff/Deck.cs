using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private GameObject[] cardPrefabs;
    public List<GameObject> drawPile;
    public List<GameObject> discardPile;

    [SerializeField] private Transform deckPos;

    private int startAmount = 2;

    private void Start()
    {
        drawPile = new List<GameObject>();

        for (int i = 0; i < cardPrefabs.Length; i++)
        {
            for (int y = 0; y < startAmount; y++)
            {
                GameObject card = Instantiate(cardPrefabs[i], deckPos);
                card.SetActive(false);
                drawPile.Add(card);
            }
        }
    }

    public GameObject DrawCard()
    {
        int rdm = Random.Range(0, drawPile.Count);
        if (drawPile.Count == 0)
        {
            Debug.Log("Out of cards");
            ShuffleCards();
        }
        GameObject card = drawPile[rdm];
        drawPile.Remove(card);
        card.SetActive(true);
        return card;
    }

    public void DiscardCard(GameObject card)
    {
        discardPile.Add(card);
        card.transform.parent = deckPos;
        card.SetActive(false);
    }

    private void ShuffleCards()
    {
        for (int i = 0; i < discardPile.Count; i++)
        {
            GameObject card = discardPile[i];
            drawPile.Add(card);
            discardPile.Remove(card);
        }
    }
}

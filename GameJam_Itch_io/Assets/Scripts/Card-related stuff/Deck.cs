using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public GameObject[] cardPrefabs;
    public List<GameObject> drawPile;
    public List<GameObject> discardPile;

    [SerializeField] private Transform deckPos;

    private int startAmount = 2;

    DeckUI deckUI;

    private void Start()
    {
        //AudioManager.Instance.Play("Healing test");

        deckUI = FindFirstObjectByType<DeckUI>();
        if (deckUI == null)
        {
            Debug.LogError("Need to add DeckUI to scene!");
            return;
        }

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
        deckUI.UpdateDrawPile(drawPile.Count);
    }

    public void AddNewCard(GameObject card)
    {
        GameObject newCard = Instantiate(card, deckPos);
        newCard.SetActive(false);
        drawPile.Add(newCard);
        deckUI.UpdateDrawPile(drawPile.Count);
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
        deckUI.UpdateDrawPile(drawPile.Count);
        card.SetActive(true);
        return card;
    }

    public void DiscardCard(GameObject card)
    {
        discardPile.Add(card);
        deckUI.UpdateDiscardPile(discardPile.Count);
        card.transform.parent = deckPos;
        card.SetActive(false);
    }

    private void ShuffleCards()
    {
        for (int i = 0; i < discardPile.Count; i++)
        {
            GameObject card = discardPile[i];
            drawPile.Add(card);
            deckUI.UpdateDrawPile(drawPile.Count);
        }
        discardPile.Clear();
        deckUI.UpdateDiscardPile(discardPile.Count);
    }
}

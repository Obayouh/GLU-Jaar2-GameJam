using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private GameObject[] cardPrefabs;
    public List<GameObject> drawPile;
    public List<GameObject> discardPile;

    private int startAmount = 2;

    private void Start()
    {
        drawPile = new List<GameObject>();

        for (int i = 0; i < cardPrefabs.Length; i++)
        {
            for (int y = 0; y < startAmount; y++)
            {
                drawPile.Add(cardPrefabs[i]);
            }
        }
    }

    public GameObject DrawCard()
    {
        int rdm = Random.Range(0, drawPile.Count);
        if (drawPile.Count == 0)
        {
            Debug.Log("Out of cards");
            return null;
        }
        GameObject card = drawPile[rdm];
        drawPile.Remove(card);
        card.SetActive(true);
        return card;
    }

    public void DiscardCard(GameObject card)
    {
        discardPile.Add(card);
        card.SetActive(false);
    }
}

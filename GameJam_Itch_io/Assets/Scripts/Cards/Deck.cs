using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Represents a card deck and also governs the discard pile and works in coincordance with the Hand script
/// </summary>
public class Deck : MonoBehaviour
{
    public static Deck Instance { get; private set; }

    private List<AllCards> deckPile;
    private List<AllCards> discardPile;

    public List<AllCards> HandCards { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        InstantiateDeck();
    }

    private void InstantiateDeck()
    {

    }
}

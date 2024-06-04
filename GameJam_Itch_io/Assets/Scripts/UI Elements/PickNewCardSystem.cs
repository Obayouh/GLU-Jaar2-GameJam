using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickNewCardSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] cardUI;
    [SerializeField] private Image[] cardPos;

    [SerializeField] private List<GameObject> cards;

    [SerializeField] private GameObject chooseNewCardsUI;

    Deck deck;

    private void Start()
    {
        deck = FindFirstObjectByType<Deck>();

        chooseNewCardsUI.SetActive(false);
    }

    public void PickCard()
    {
        chooseNewCardsUI.SetActive(true);

        for (int i = 0; i < cardPos.Length; i++)
        {
            GameObject newCard = Instantiate(cardUI[Random.Range(0, cardUI.Length)], cardPos[i].transform);
            Sprite card = newCard.GetComponent<SpriteRenderer>().sprite;
            cardPos[i].sprite = card;
            cards.Add(newCard);
        }
    }

    public void Card1()
    {
        Transform cardUI = cardPos[0].transform.GetChild(0);
        Transform card = cardUI.GetChild(0);

        StartCoroutine(CardChoosen(card.gameObject));
    }

    public void Card2()
    {
        Transform cardUI = cardPos[1].transform.GetChild(0);
        Transform card = cardUI.GetChild(0);

        StartCoroutine(CardChoosen(card.gameObject));
    }

    public void Card3()
    {
        Transform cardUI = cardPos[2].transform.GetChild(0);
        Transform card = cardUI.GetChild(0);

        StartCoroutine(CardChoosen(card.gameObject));
    }

    private IEnumerator CardChoosen(GameObject card)
    {
        deck.AddNewCard(card);

        for (int i = 0; i < cards.Count; i++)
        {
            Destroy(cards[i]);
        }
        cards.Clear();

        ReferenceInstance.refInstance.turnManager.StartNextWave();

        chooseNewCardsUI.SetActive(false);

        yield return null;
    }
}

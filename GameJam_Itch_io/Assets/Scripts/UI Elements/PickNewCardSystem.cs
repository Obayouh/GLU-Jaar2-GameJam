using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickNewCardSystem : MonoBehaviour
{
    [SerializeField] private Sprite[] cards;
    [SerializeField] private Image[] cardPos;

    [SerializeField] private GameObject chooseNewCardsUI;

    private void Start()
    {

    }

    public void PickCard()
    {
        chooseNewCardsUI.SetActive(true);

        for (int i = 0; i < cardPos.Length; i++)
        {
            Sprite card = cards[Random.Range(0, cards.Length)];
            cardPos[i].sprite = card;
        }
    }

    public void Card1()
    {
        StartCoroutine(CardChoosen());
    }

    public void Card2()
    {
        StartCoroutine(CardChoosen());
    }

    public void Card3()
    {
        StartCoroutine(CardChoosen());
    }

    private IEnumerator CardChoosen()
    {
        chooseNewCardsUI.SetActive(false);

        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickNewCardSystem : MonoBehaviour
{
    [SerializeField] private Sprite[] cards;
    [SerializeField] private Image[] cardPos;

    private void Start()
    {
        PickCard();
    }

    public void PickCard()
    {
        for (int i = 0; i < cardPos.Length; i++)
        {
            Sprite card = cards[Random.Range(0, cards.Length)];
            cardPos[i].sprite = card;
        }
    }
}

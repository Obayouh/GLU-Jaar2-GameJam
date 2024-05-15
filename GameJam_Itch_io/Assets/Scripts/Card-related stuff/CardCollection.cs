using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCollection : MonoBehaviour
{
    public List<GameObject> cards = new List<GameObject>();
    Vector3 lastPosition;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void AddCards(GameObject card)
    {
        cards.Add(card);
    }

    public void RemoveCard(GameObject card)
    {
        cards.Remove(card);
    }
}

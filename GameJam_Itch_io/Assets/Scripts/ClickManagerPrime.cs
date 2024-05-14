using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManagerPrime : MonoBehaviour
{
    private RaycastHit hit;
    private Transform previousHit;
    private Transform currentHitTransform;

    private GameObject selectedCard;
    private GameObject selectedEnemy;

    private CardStats cardStats;

    void Start()
    {
        currentHitTransform = null;
        selectedCard = null;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 400.0f))
        {

            // Inform the previously hit object that the mouse is no longer hovering over it
            if (previousHit != null && previousHit != hit.transform)
            {
                previousHit.SendMessage("OnMouseExit", SendMessageOptions.DontRequireReceiver);
                previousHit = null;
            }

            // Inform the newly hit object that the mouse is now hovering over it
            currentHitTransform = hit.transform;
            currentHitTransform.SendMessage("OnMouseEnter", SendMessageOptions.DontRequireReceiver);

            // Store the current hit target as the previous hit target
            previousHit = currentHitTransform;

            //select playercard to use if not already defined
            if (Input.GetMouseButtonDown(0) && selectedCard == null && hit.transform.CompareTag("PlayerCard"))
            {
                selectedCard = hit.transform.gameObject;
                cardStats = selectedCard.GetComponentInParent<CardStats>();

                //Check if player has enough mana
                if (ReferenceInstance.refInstance.playerStats.ReturnPlayerMana() < cardStats.ReturnCost())
                {
                    Debug.Log("Not Enough Mana");
                    selectedCard = null;
                    cardStats = null;
                }
                else
                {
                    ReferenceInstance.refInstance.cardManager.SelectedCard(selectedCard);
                }
                //Debug.Log(selectedCard);
            }

            //Select enemy to attack if not already defined and then empty card and enemy selections
            if (Input.GetMouseButtonDown(0) && selectedEnemy == null && selectedCard != null && hit.transform.CompareTag("Enemy"))
            {
                DealDamage();
            }
        }
        else
        {
            if (previousHit != null)
            {
                previousHit.SendMessage("OnMouseExit", SendMessageOptions.DontRequireReceiver);
                previousHit = null;
            }
        }
    }

    private void DealDamage()
    {
        selectedEnemy = hit.transform.gameObject;
        HealthSystem hs = selectedEnemy.GetComponent<HealthSystem>();
        hs.TakeDamage(cardStats.ReturnDamage());
        FinishedAttacking();
    }

    private void FinishedAttacking()
    {
        ReferenceInstance.refInstance.playerStats.LoseMana(cardStats.ReturnCost());
        ReferenceInstance.refInstance.cardManager.RemoveCard(selectedCard.transform.parent.parent.gameObject);
        ReferenceInstance.refInstance.turnManager.ChangeState(TurnState.PickCard);
        selectedCard = null;
        selectedEnemy = null;
    }

    public GameObject ReturnPlayerCard()
    {
        if (selectedCard != null)
        {
            return selectedCard;
        }
        else
        {
            return null;
        }
    }
}

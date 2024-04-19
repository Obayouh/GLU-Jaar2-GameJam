using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    Coroutine currentCoroutine;
    RaycastHit hit;
    public Transform currentHitTransform;
    public Vector3 hitPosition;
    Transform previousHit;
    GameObject selectedCard;
    private GameObject selectedEnemy;

    private CardStats cardStats;
    private PlayerStats playerStats;

    private void Start()
    {
        currentCoroutine = null;
        currentHitTransform = null;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 400.0f))
        {
            currentHitTransform = hit.transform;
            hitPosition = currentHitTransform.position;

            if (Input.GetMouseButtonDown(0) && selectedCard == null && hit.transform.CompareTag("PlayerCard"))
            {
                //If card is too costly, do nothing
                cardStats = GetComponent<CardStats>();
                //playerStats.CheckIfUsable();
                selectedCard = hit.transform.gameObject;
            }

            if (Input.GetMouseButtonDown(0) && selectedCard != null && selectedEnemy == null)
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    selectedEnemy = hit.transform.gameObject;
                    StartCoroutine(ThrowCard());
                }

            }

            if (previousHit != null)
            {
                if (previousHit != hit.transform && previousHit.CompareTag("PlayerCard"))
                {
                    previousHit.GetComponentInParent<Animator>().SetBool("isSelected", false);
                }
            }

            if (currentCoroutine == null)
            {
                currentCoroutine = StartCoroutine(ScaleMe(hit.transform));
                Debug.Log("You selected the " + hit.transform.name);
            }
        }

    }
    IEnumerator ScaleMe(Transform objTr)
    {
        previousHit = objTr;

        //if (objTr.CompareTag("Enemy"))
        //{
        //    objTr.localScale *= 1.2f;
        yield return new WaitForSeconds(0.01f);
        //    objTr.localScale /= 1.2f;
        //}
        if (objTr.CompareTag("PlayerCard"))
        {
            objTr.gameObject.GetComponentInParent<Animator>().SetBool("isSelected", true);
            selectedCard = objTr.gameObject;
        }
        currentCoroutine = null;
    }

    //public GameObject ReturnEnemy()
    //{
    //    return selectedEnemy;
    //}

    //public GameObject ReturnSelectedCard()
    //{
    //    return selectedCard;
    //}

    IEnumerator ThrowCard()
    {
        HealthSystem enemyHealth = selectedEnemy.GetComponent<HealthSystem>();
        yield return new WaitForSeconds(1f);
        enemyHealth.TakeDamage(20);
        Destroy(selectedCard);
    }
}

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

    private void Start()
    {
        currentCoroutine = null;
        currentHitTransform = null;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            currentHitTransform = hit.transform;
            hitPosition = currentHitTransform.position;

            if (Input.GetMouseButtonDown(0) && selectedCard != null)
            {
                // Code voor de selected card te activeren. Je moet ook iets hebben voor de enemy te selecteren.
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

        if (objTr.CompareTag("Enemy"))
        {
            objTr.localScale *= 1.2f;
            yield return new WaitForSeconds(0.5f);
            objTr.localScale /= 1.2f;
        }
        else if (objTr.CompareTag("PlayerCard"))
        {
            objTr.gameObject.GetComponentInParent<Animator>().SetBool("isSelected", true);
            selectedCard = objTr.gameObject;
        }
        currentCoroutine = null;
    }

    private void OnCollisionExit(Collision collision)
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    Coroutine currentCoroutine;
    RaycastHit hit;
    public Transform currentHitTransform;
    public Vector3 hitPosition;
    private void Start()
    {
        currentCoroutine = null;
        currentHitTransform = null;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                currentHitTransform = hit.transform;
                hitPosition = currentHitTransform.position;

                if(currentCoroutine == null)
                {
                    currentCoroutine = StartCoroutine(ScaleMe(hit.transform));
                }
                //Debug.Log("You selected the " + hit.transform.name);
                //Debug.Log(currentHitTransform.tag);
            }
        }

    }
    IEnumerator ScaleMe(Transform objTr)
    {
        if (objTr.CompareTag("Enemy") || objTr.CompareTag("PlayerCard"))
        {
            objTr.localScale *= 1.2f;
            yield return new WaitForSeconds(0.5f);
            objTr.localScale /= 1.2f;
        }
        currentCoroutine = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTestAttackElgin : MonoBehaviour
{
    //[SerializeField] private Transform firingPoint;
    private PlayerStats playerStats;
    private ClickManager clickManagerScript;
    private TestCardProjectile projectileScript; 

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        clickManagerScript = FindObjectOfType<ClickManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ThrowCard();
        }

    }

    public void ThrowCard()
    {
        //    Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        Vector3 targetPoint = hit.point;
        //        Vector3 direction = targetPoint - transform.position;
        //        direction.Normalize();
        
        //Debug.Log(clickManagerScript.currentHitTransform.position);


    }
}

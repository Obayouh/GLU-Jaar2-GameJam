using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTestAttackElgin : MonoBehaviour
{
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject InstantiatedCard;
    private ClickManager clickManagerScript;

    private void Start()
    {
        clickManagerScript = FindObjectOfType<ClickManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (clickManagerScript.currentHitTransform.CompareTag("Enemy"))
            {
                Shoot();
            }
        }

    }

    void Shoot()
    {
        //    Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        Vector3 targetPoint = hit.point;
        //        Vector3 direction = targetPoint - transform.position;
        //        direction.Normalize();

        GameObject projectile = Instantiate(InstantiatedCard, firingPoint.position, Quaternion.identity);
        projectile.transform.LookAt(clickManagerScript.currentHitTransform);
        Debug.Log(clickManagerScript.currentHitTransform.position);
        

    }
}

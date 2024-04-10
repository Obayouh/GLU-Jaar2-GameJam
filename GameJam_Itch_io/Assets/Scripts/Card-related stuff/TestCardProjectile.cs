using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCardProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private LayerMask collisionMask;

    private void Update()
    {
        MoveProjectile();
    }

    private void MoveProjectile()
    {
        //// Calculate the next position of the projectile using raycast
        //RaycastHit hit;
        //if (Physics.Raycast(transform.position, transform.forward, out hit, speed * Time.deltaTime, collisionMask))
        //{
        //    // If the raycast hits something, destroy the projectile
        //    Destroy(gameObject);
        //    return;
        //}

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}

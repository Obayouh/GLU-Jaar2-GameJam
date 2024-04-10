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
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Behavior : MonoBehaviour
{
    private float _speed = 5;

    void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.forward);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}

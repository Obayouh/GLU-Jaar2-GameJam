using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Behavior : MonoBehaviour
{
    private float _speed = 10;

    void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.forward);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats playerHealth = collision.gameObject.GetComponent<PlayerStats>();
            playerHealth.TakeDamage(Random.Range(2, 8));
            Destroy(this.gameObject);
        }
    }
}

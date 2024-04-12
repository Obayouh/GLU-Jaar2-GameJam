using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardStats : AbCards
{
    private TextMeshPro damageText;
    private TextMeshPro costText;
    public override void Start()
    {
        base.Start();
        //loop through card
        foreach (Transform t in transform)
        {
            //loop through text components
            foreach (Transform t2 in t)
            {
                if (t2.name == "DamageText")
                {
                    damageText = t2.GetComponent<TextMeshPro>();
                }
                else
                {
                    costText = t2.GetComponent<TextMeshPro>();
                }
            }
        }
        SetCardStats();
    }

    public override void Update()
    {
        base.Update();
    }
    /// <summary>
    /// Randomizes card damage and cost to use on creation
    /// </summary>
    public override void SetCardStats()
    {
        base.SetCardStats();
        damage = Random.Range(1, 9);
        cost = Random.Range(1, 5);
        damageText.SetText(damage.ToString());
        costText.SetText(cost.ToString());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            HealthSystem enemyHealth = collision.gameObject.GetComponent<HealthSystem>();
            enemyHealth.TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }

    public int ReturnCost()
    {
        return cost;
    }

    public int ReturnDamage()
    {
        return damage;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardStats : MonoBehaviour
{
    private TextMeshPro damageText;
    private TextMeshPro costText;
    private int damage;
    private int cost;
    public void Start()
    {
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

    public  void Update()
    {

    }
    /// <summary>
    /// Randomizes card damage and cost to use on creation
    /// </summary>
    public void SetCardStats()
    {
        damage = Random.Range(1, 9);
        cost = Random.Range(1, 5);
        damageText.SetText(damage.ToString());
        costText.SetText(cost.ToString());
    }

    public int ReturnCost()
    {
        return cost;
    }

    public float ReturnDamage()
    {
        return damage;
    }
}

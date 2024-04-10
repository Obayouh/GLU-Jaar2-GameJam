using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardStats : AbCards
{
    public TextMeshPro damageText;
    public TextMeshPro costText;
    public override void Start()
    {
        base.Start();


        //loop through card
        foreach(Transform t in transform)
        {
            //loop through text components
            foreach(Transform t2 in t)
            {
                if(t2.name == "DamageText")
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

     public override void SetCardStats()
    {
        base.SetCardStats();
        damage = Random.Range(1, 9);
        cost = Random.Range(1, 5);
        damageText.SetText(damage.ToString());
        costText.SetText(cost.ToString());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Drawing;

public class CardStats : MonoBehaviour
{   
    private TextMeshPro damageText;
    private TextMeshPro costText;
    private int baseDamage;
    private int cost;

    private SpawnEnemies spawnEnemiesScript;

    [field: SerializeField] public E_ElementalTyping Typing { get; private set; }
    public void Start()
    {
        GetTextMeshPro();       
    }

    /// <summary>
    /// Randomizes card damage and cost to use on creation
    /// </summary>
    public void SetCardStats()
    {
        
        baseDamage = Random.Range(1, 9);
        cost = Random.Range(1, 5);
        damageText.fontSize = 1;
        costText.fontSize = 1;
        damageText.SetText("Damage:\n " + baseDamage.ToString());
        costText.SetText("ManaCost:\n " + cost.ToString());
    }

    private void GetTextMeshPro()
    {
        TextMeshPro[] textComponents = GetComponentsInChildren<TextMeshPro>();

        if (textComponents.Length == 2)
        {
            damageText = textComponents[0];
            costText = textComponents[1];
        }
        else
        {
            Debug.Log("There are not enough TextMeshPro objects in the parent object.");
        }
        SetCardStats();
    }

    //public void ElementalEffect()
    //{
    //    if (Typing == E_ElementalTyping.Fire)
    //    {

    //    }
    //    else if (Typing == E_ElementalTyping.Lightning)
    //    {
    //        ReturnDoTDamage();                     
    //    }
    //    else if (Typing == E_ElementalTyping.Water)
    //    {

    //    }
    //    else if (Typing == E_ElementalTyping.Rock)
    //    {

    //    }
    //}

    public int ReturnCost()
    {
        return cost;
    }

    public float ReturnDamage()
    {
        return baseDamage;
    }

    public int ReturnHalfDamage()
    {
        int DamageOverTime = Mathf.RoundToInt(baseDamage / 2);
        return DamageOverTime;
    }

    public void DealFireDamage(CardStats firstFireCard, HealthSystem fireTarget)
    {
        if (firstFireCard != null && fireTarget != null)
        { 
            if (fireTarget.CurrentHealth <= 0)
            {
                firstFireCard = null;
                
                fireTarget = null;
                //Unsub right after
            }
        }
    }

    public void DealExtraFireDamage(CardStats secondFireCard, HealthSystem fireTarget)
    {
        fireTarget.TakeDamage(secondFireCard.ReturnDamage() * 1.5f);
        if (fireTarget.CurrentHealth <= 0)
        {
            secondFireCard = null;
            fireTarget = null;
            //Unsub right after
        }
    }

    public void DealLightningDamage(CardStats lightningCard, HealthSystem lightningTarget)
    {
        lightningTarget.TakeDamage(lightningCard.ReturnHalfDamage());

        if (lightningTarget.CurrentHealth <= 0)
        {
            lightningCard = null;
            lightningTarget = null;
            //Unsub right after
        }
    }

    public void DealWaterDamage(CardStats waterCard, HealthSystem waterTarget)
    {
        SpawnEnemies spawnEnemiesList = ReferenceInstance.refInstance.spawnEnemiesScript;

        if (waterTarget.CurrentHealth <= 0)
        {
            waterCard = null;
            waterTarget = null;
            //Unsub right after
        }

        
        if (spawnEnemiesList.spawnedEnemies.Count > 1 && waterTarget != null)
        {
            for (int i = 0; i < spawnEnemiesList.spawnedEnemies.Count; i++)
            {
                HealthSystem enemyHealth = spawnEnemiesList.spawnedEnemies[i].GetComponent<HealthSystem>();

                HealthSystem splashTarget = enemyHealth;

                if (waterTarget != splashTarget)
                {
                    splashTarget.TakeDamage(waterCard.ReturnHalfDamage());
                }
            }
        }
    }
}

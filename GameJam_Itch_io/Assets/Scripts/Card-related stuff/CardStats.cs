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
        #region Test Random Nummers Kaarten
        //float rdm = Random.value;
        //if (rdm < .05f)
        //{
        //    baseDamage = Random.Range(1, 1);
        //    cost = Random.Range(1, 5);
        //}
        //else if (rdm < .4f)
        //{
        //    baseDamage = Random.Range(2, 4);
        //    cost = Random.Range(1, 2);
        //}
        //else if (rdm < .8f)
        //{
        //    baseDamage = Random.Range(4, 6);
        //    cost = Random.Range(2, 4);
        //}
        //else if (rdm < 1f)
        //{
        //    baseDamage = Random.Range(7, 9);
        //    cost = Random.Range(4, 5);
        //}
        //else
        //{
        //baseDamage = Random.Range(1, 9);
        //cost = Random.Range(1, 5);
        //}
        #endregion

        if (Typing == E_ElementalTyping.Fire)
        {
            baseDamage = Random.Range(4, 7);
            cost = Random.Range(3, 5);
        }
        else if (Typing == E_ElementalTyping.Lightning)
        {
            baseDamage = Random.Range(3, 5);
            cost = Random.Range(1, 3);
        }
        else if (Typing == E_ElementalTyping.Water)
        {
            baseDamage = Random.Range(4, 6);
            cost = Random.Range(2, 4);
        }
        else if (Typing == E_ElementalTyping.Rock)
        {
            baseDamage = Random.Range(4, 7);
            cost = Random.Range(2, 4);
        }
        else if (Typing == E_ElementalTyping.Neutral)
        {
            baseDamage = Random.Range(5, 8);
            cost = Random.Range(5, 7);
        }

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

    public void DealRockDamage(CardStats rockCard, HealthSystem rockTarget)
    {
        if (rockTarget.CurrentHealth <= 0)
        {
            rockCard = null;
        }

        if (rockCard != null && rockTarget != null)
        {
            rockTarget.hasShield = false;
        }
    }

    public void DealExplosionDamage(CardStats explosionCard, HealthSystem explosionTarget)
    {
        SpawnEnemies spawnEnemiesList = ReferenceInstance.refInstance.spawnEnemiesScript;

        if (explosionTarget.CurrentHealth <= 0)
        {
            explosionCard = null;
            explosionTarget = null;
            //Unsub right after
        }

        if (spawnEnemiesList.spawnedEnemies.Count > 1 && explosionTarget != null)
        {
            for (int i = 0; i < spawnEnemiesList.spawnedEnemies.Count; i++)
            {
                HealthSystem enemyHealth = spawnEnemiesList.spawnedEnemies[i].GetComponent<HealthSystem>();

                HealthSystem splashTarget = enemyHealth;

                if (splashTarget != explosionTarget)
                {
                    splashTarget.TakeDamage(explosionCard.ReturnDamage());
                }
            }
        }
    }
}

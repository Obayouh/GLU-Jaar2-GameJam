using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardStats : MonoBehaviour
{
    //public enum Typing
    //{
    //    Heat,
    //    Water,
    //    Volt,
    //    Gaia,
    //    Neutral,
    //}

    
    private TextMeshPro damageText;
    private TextMeshPro costText;
    private int damage;
    private int cost;

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
        damage = Random.Range(1, 9);
        cost = Random.Range(1, 5);
        damageText.SetText(damage.ToString());
        costText.SetText(cost.ToString());
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

    public int ReturnCost()
    {
        return cost;
    }

    public float ReturnDamage()
    {
        return damage;
    }
}

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

    public int ReturnDoTDamage()
    {
        int DamageOverTime = Mathf.RoundToInt(baseDamage / 2);
        return DamageOverTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbCards : MonoBehaviour
{
    [SerializeField, Range(0, 20)] protected int damage;
    [SerializeField, Min(1)] protected int cost;
    public virtual void SetCardStats()
    {

    }

    public virtual void Discard()
    {

    }

}

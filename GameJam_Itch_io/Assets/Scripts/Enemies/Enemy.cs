using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private void Start()
    {
        Action();
    }

    protected virtual void Action()
    {
        Debug.Log("Enemy class: Attack method called!");
    }
}

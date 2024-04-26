using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : Enemy
{
    protected override void Action()
    {
        base.Action();
        Debug.Log("Healer class: Action method called!");
    }
}

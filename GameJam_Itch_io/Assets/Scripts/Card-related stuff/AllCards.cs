using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllCards" ,menuName = "ScriptableObjects/Cards")]
public class AllCards : ScriptableObject
{
    public List<GameObject> EveryCard;
}

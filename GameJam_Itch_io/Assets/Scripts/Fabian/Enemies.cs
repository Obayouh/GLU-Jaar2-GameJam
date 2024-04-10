using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesScriptableObject", menuName = "ScriptableObjects/Enemies")]
public class Enemies : ScriptableObject
{
    public List<GameObject> AllEnemies;
}

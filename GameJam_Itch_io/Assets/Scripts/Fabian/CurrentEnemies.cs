using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentEnemies : MonoBehaviour
{
    public List<GameObject> SpawnedEnemies = new();

    public List<GameObject> SpawnSpots = new();

    public Enemies AllEnemies;

    private void Update()
    {
        foreach (GameObject slot in SpawnSpots)
        {
            SpawnSlot spawnSlot = slot.GetComponent<SpawnSlot>();
            if (spawnSlot.HasChild == false)
            {
                GameObject pickEnemy = AllEnemies.AllEnemies[Random.Range(0, AllEnemies.AllEnemies.Count)];
                SpawnedEnemies.Add(pickEnemy);
                Instantiate(pickEnemy, slot.transform);
                spawnSlot.HasChild = true;

            }
        }
    }
}

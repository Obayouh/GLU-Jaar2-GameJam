using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public List<GameObject> spawnedEnemies = new();
    public Enemies Enemies;

    void Start()
    {
        StartCoroutine(InstantiateEnemies());
    }

    public IEnumerator InstantiateEnemies()
    {
        foreach (GameObject slot in spawnPoints)
        {
            SpawnSlot spawnSlot = slot.GetComponent<SpawnSlot>();
            if (spawnSlot.HasChild == false)
            {
                GameObject randoEnemy = Enemies.AllEnemies[Random.Range(0, Enemies.AllEnemies.Count)];
                GameObject spawnEnemy = Instantiate(randoEnemy, slot.transform);
                AddEnemy(spawnEnemy);
                spawnSlot.HasChild = true;
            }
        }
        yield return new WaitForSeconds(0.1f);
    }

    private void AddEnemy(GameObject enemy)
    {
        spawnedEnemies.Add(enemy);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        SpawnSlot spawnSlot = enemy.GetComponentInParent<SpawnSlot>();
        spawnSlot.HasChild = false;
        spawnedEnemies.Remove(enemy);
        Destroy(enemy);
    }
}

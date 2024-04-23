using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public List<GameObject> AllSpawnPoints = new();
    public List<GameObject> CurrentSpawnedEnemies = new();
    public Enemies Enemies;

    [HideInInspector] public float FirstSpawnPointPos;
    [HideInInspector] public float SecondSpawnPointPos;
    [HideInInspector] public float ThirdSpawnPointPos;

    void Start()
    {
        FirstSpawnPointPos = AllSpawnPoints[0].transform.position.x;
        SecondSpawnPointPos = AllSpawnPoints[1].transform.position.x;
        ThirdSpawnPointPos = AllSpawnPoints[2].transform.position.x;
        StartCoroutine(InstantiateEnemies());
    }

    public IEnumerator InstantiateEnemies()
    {
        foreach (GameObject slot in AllSpawnPoints)
        {
            SpawnSlot spawnSlot = slot.GetComponent<SpawnSlot>();
            if (spawnSlot.HasChild == false)
            {
                GameObject randoEnemy = Enemies.AllEnemies[Random.Range(0, Enemies.AllEnemies.Count)];
                GameObject spawnEnemy = Instantiate(randoEnemy, slot.transform);
                CurrentSpawnedEnemies.Add(spawnEnemy);
                spawnSlot.HasChild = true;
            }
        }
        yield return new WaitForSeconds(0.1f);
    }

    public void RemoveCurrentEnemy(GameObject enemy)
    {
        CurrentSpawnedEnemies.Remove(enemy);
    }
}

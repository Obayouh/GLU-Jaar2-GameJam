using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentEnemies : MonoBehaviour
{
    public List<GameObject> SpawnedEnemies = new();

    public List<GameObject> SpawnSpots = new();

    public Enemies AllEnemies;

    public bool SlotsEmpty = false;

    public GameObject Portal;

    private List<SpawnSlot> _allSlots = new();

    private void Start()
    {
        Portal.SetActive(true);
        StartCoroutine(SpawnEnenmies());
        _allSlots.Add(SpawnSpots[0].GetComponent<SpawnSlot>());
        _allSlots.Add(SpawnSpots[1].GetComponent<SpawnSlot>());
        _allSlots.Add(SpawnSpots[2].GetComponent<SpawnSlot>());
    }

    private void Update()
    {
        //for (int i = 0; i < _allSlots.Count; i++)
        //{
        //    if (_allSlots[i].HasChild == false)
        //    {
        //        SlotsEmpty = true;
        //    }
        //}

        if (SpawnedEnemies.Count == 0)
        {
            SlotsEmpty = true;
        }
    }

    public IEnumerator SpawnEnenmies()
    {
        foreach (GameObject slot in SpawnSpots)
        {
            SpawnSlot spawnSlot = slot.GetComponent<SpawnSlot>();
            if (SlotsEmpty == true)
            {
                GameObject pickEnemy = AllEnemies.AllEnemies[Random.Range(0, AllEnemies.AllEnemies.Count)];
                GameObject newEnemy = Instantiate(pickEnemy, slot.transform);
                SpawnedEnemies.Add(newEnemy);
                SlotsEmpty = false; 
                spawnSlot.HasChild = true;
            }
        }
        yield return new WaitForSeconds(0.1f);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        SpawnedEnemies.Remove(enemy);
    }
}

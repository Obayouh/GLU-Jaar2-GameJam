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
        for (int i = 0; i < _allSlots.Count; i++)
        {
            if (_allSlots[i].HasChild == false)
            {
                SlotsEmpty = true;
            }
        }
    }

    public IEnumerator SpawnEnenmies()
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
        yield return new WaitForSeconds(0.1f);
    }
}

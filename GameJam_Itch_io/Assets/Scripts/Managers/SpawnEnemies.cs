using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public List<GameObject> spawnedEnemies = new();
    public Enemies Enemies;

    private EnemyController enemyController;
    private PickNewCardSystem pickNewCardSystem;

    void Start()
    {
        enemyController = GetComponent<EnemyController>();
        pickNewCardSystem = FindFirstObjectByType<PickNewCardSystem>();

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
                enemyController.AddEnemy(spawnEnemy);
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
        enemyController.RemoveEnemy(enemy);

        StartCoroutine(DestroyEnemy(enemy));
    }

    private IEnumerator DestroyEnemy(GameObject enemy)
    {
        yield return new WaitForSeconds(.5f);
        Destroy(enemy);

        //If all enemies are killed, pick new card
        if (spawnedEnemies.Count < 1)
        {
            pickNewCardSystem.PickCard();

            ReferenceInstance.refInstance.turnManager.ChangeState(TurnState.Waiting);
        }
    }
}

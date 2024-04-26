using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Queue<Transform> enemyQueue = new Queue<Transform>();

    // Add enemies to the queue
    public void AddEnemyToQueue(Transform enemy)
    {
        enemyQueue.Enqueue(enemy);
    }

    public void RemoveEnemyFromQueue(Transform enemy)
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            StartEnemyActions();
        }
    }

    // Start processing enemies' actions
    public void StartEnemyActions()
    {
        StartCoroutine(ProcessEnemyActions());
    }

    public void NextEnemyInQueue()
    {
        if (enemyQueue.Count > 0)
        {
            StartCoroutine(ProcessEnemyActions());
        }
        else
        {
            TurnManager turnManager = FindFirstObjectByType<TurnManager>();
            turnManager.ChangeState(TurnState.PlayerTurn);
        }
    }

    // Coroutine to process enemy actions one at a time
    private IEnumerator ProcessEnemyActions()
    {
            Transform currentEnemy = enemyQueue.Dequeue();
            // Perform enemy action here (e.g., move, attack, etc.)
            Debug.Log("Enemy " + currentEnemy.name + " is performing action.");

            // Simulate action delay
            yield return new WaitForSeconds(1f); // Change delay as needed
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Queue<Transform> enemyQueue = new Queue<Transform>();
    private List<GameObject> enemyList = new List<GameObject>();

    // Add enemies to the queue
    public void AddEnemyToQueue(Transform enemy)
    {
        enemyQueue.Enqueue(enemy);
    }

    public void RemoveEnemyFromQueue(Transform enemy)
    {
        if (enemyQueue.Contains(enemy) == true)
        {

        }
    }

    private void Update()
    {

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
        if (currentEnemy == null)
        {
            Debug.Log("WTF");
            yield break;
        }
        Debug.Log("Enemy " + currentEnemy.name + " is performing action.");
        Ab_Enemy enemy = currentEnemy.GetComponent<Ab_Enemy>();
        enemy.OnAction();

        // Simulate action delay
        yield return new WaitForSeconds(1f); // Change delay as needed

        if (enemyQueue.Count <= 0)
        {
            TurnManager turnManager = FindAnyObjectByType<TurnManager>();
            turnManager.ChangeState(TurnState.PlayerTurn);

            //_enemyController.AddEnemyToQueue(this.transform);
        }
        else
        {
            StartEnemyActions();
        }
    }
}

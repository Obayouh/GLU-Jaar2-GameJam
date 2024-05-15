using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public List<GameObject> enemies; // Array of enemy GameObjects
    public float actionInterval = 2f; // Time interval between enemy actions

    private int currentEnemyIndex = 0; // Index of the current enemy taking action
    private bool isPerformingAction = false; // Flag to prevent simultaneous actions

    void Start()
    {

    }

    public void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

    public void StartEnemyActions()
    {
        // Initialize the action sequence by performing the first action
        StartCoroutine(PerformEnemyActions());
    }

    IEnumerator PerformEnemyActions()
    {
        yield return new WaitForSeconds(actionInterval);

        while (currentEnemyIndex < enemies.Count)
        {
                GameObject currentEnemy = enemies[currentEnemyIndex];
                isPerformingAction = true;

                // Perform the action for the current enemy
                Debug.Log("Enemy " + (currentEnemyIndex + 1) + " performs action.");
                //Ab_Enemy enemy = currentEnemy.GetComponent<Ab_Enemy>();
                //enemy.OnAction();

                // Simulate the action (e.g., moving, attacking) - Replace this with your logic

                // Wait for the action interval
                yield return new WaitForSeconds(actionInterval);

                // Move to the next enemy
                currentEnemyIndex++;
        }

        // If all enemies have performed their actions, switch turns
        if (currentEnemyIndex >= enemies.Count)
        {
            // Reset the index for the next round of actions
            currentEnemyIndex = 0;
            // Switch turns
            yield return new WaitForSeconds(actionInterval); // Wait a moment before switching turns

            ReferenceInstance.refInstance.turnManager.ChangeState(TurnState.PlayerTurn);
        }

        yield return null;

    }
}

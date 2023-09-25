using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject
    public GameObject enemyPrefab; // Prefab of the enemy to spawn
    public int totalEnemies = 5; // Total number of enemies in the game
    public GameObject gameOverUI; // Reference to the game over UI panel
    public Transform[] spawnPoints; // Reference to the spawn points

    private int remainingEnemies; // Number of remaining enemies
    
    private void Start()
    {
        remainingEnemies = totalEnemies;

        // Spawn enemies
        for (int i = 0; i < totalEnemies; i++)
        {
            SpawnEnemy();
        }
    }

    private void Update()
    {
        if (remainingEnemies == 0)
        {
            // All enemies are eaten, player wins
            EndGame(true);
        }
    }

    // Method to handle enemy spawn
    private void SpawnEnemy()
{
    // Choose a random spawn point
    int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
    Transform selectedSpawnPoint = spawnPoints[randomSpawnIndex];

    // Instantiate the enemy prefab at the selected spawn point
    Instantiate(enemyPrefab, selectedSpawnPoint.position, Quaternion.identity);
}

    // Method to decrease the remaining enemy count
    public void EnemyEaten()
    {
        remainingEnemies--;
       

        // You can update a UI element to display the remaining enemies here if needed
    }

    // Method to end the game
    private void EndGame(bool playerWins)
    {
        // Display game over UI
        gameOverUI.SetActive(true);

        if (playerWins)
        {
            // Handle win condition
            Debug.Log("You win!");
        }
        else
        {
            // Handle lose condition
            Debug.Log("You lose!");
        }

        // Optionally, restart the scene or load a different scene after a delay
        // For example, you can reload the current scene after a few seconds:
        // Invoke("RestartGame", 3f);
    }

    // Method to restart the game
    private void RestartGame()
    {
        // Reload the current scene (assuming the game is a single scene)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

}


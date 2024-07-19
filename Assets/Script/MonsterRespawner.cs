using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRespawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign the enemy prefab in the inspector
    public Transform[] spawnPoints; // Assign spawn points in the inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RespawnEnemies();
        }
    }

    void RespawnEnemies()
    {
        // Destroy existing enemies in the area
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }

        // Instantiate new enemies
        foreach (Transform spawnPoint in spawnPoints)
        {
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}

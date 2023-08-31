using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
  
    public GameObject enemyPrefab;       // The enemy prefab to instantiate.
    public float spawnRate = 2f;         // Rate at which enemies will spawn.
    private float nextSpawnTime = 0f;    // To control the timing of the spawns.

    public float gameWorldWidth = 20f;   // Width of the game world.
    public float gameWorldHeight = 10f;  // Height of the game world.



    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + 1f / spawnRate;
        }
    }

    void SpawnEnemy()
    {
        Vector2[] spawnPoints = {
            new Vector2(-20, Random.Range(-25, 25)),
            new Vector2(20, Random.Range(-25, 25)),
            new Vector2(Random.Range(-15, 15), 25),
            new Vector2(Random.Range(-15, 15), -25)
        };

        Vector2 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
/*

using UnityEngine;

public class EnemySpawner : MonoBehaviour
{


    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + 1f / spawnRate;
        }
    }

    void SpawnEnemy()
    {
        Vector2[] spawnPoints = {
            new Vector2(-gameWorldWidth/2, Random.Range(-gameWorldHeight/2, gameWorldHeight/2)),
            new Vector2(gameWorldWidth/2, Random.Range(-gameWorldHeight/2, gameWorldHeight/2)),
            new Vector2(Random.Range(-gameWorldWidth/2, gameWorldWidth/2), gameWorldHeight/2),
            new Vector2(Random.Range(-gameWorldWidth/2, gameWorldWidth/2), -gameWorldHeight/2)
        };

        Vector2 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
*/
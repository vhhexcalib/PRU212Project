using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    public Transform spawnPoint;
    public Transform[] pathPoints;
    public int startingEnemies = 2;
    public float timeBetweenWaves = 30f;
    public float spawnDelay = 5f;
    public int maxWaves = 5;

    private int currentWave = 1;
    private int enemiesInWave;
    private int remainingEnemies;

    private void Start()
    {
        enemiesInWave = startingEnemies;
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        Debug.Log($"Starting wave {currentWave} with {enemiesInWave} enemies.");
        yield return new WaitForSeconds(timeBetweenWaves);

        if (currentWave > maxWaves)
        {
            Debug.Log("Max number of waves reached.");
            yield break;
        }

        remainingEnemies = enemiesInWave;

        for (int i = 0; i < enemiesInWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }

        Debug.Log($"All enemies for wave {currentWave} spawned. Waiting for destruction...");

        while (remainingEnemies > 0)
        {
            yield return null;
        }

        Debug.Log($"Wave {currentWave} complete. Moving to next wave...");

        enemiesInWave += 2;
        currentWave++;
        StartCoroutine(SpawnWave());
    }

    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(Enemy, spawnPoint.position, Quaternion.identity);
        enemy.SetActive(true);

        CreepMovement creepMovement = enemy.GetComponent<CreepMovement>();
        if (creepMovement != null && pathPoints.Length > 0)
        {
            creepMovement.pathPoints = pathPoints;
        }

        CreepHealth creepHealth = enemy.GetComponent<CreepHealth>();
        if (creepHealth != null)
        {
            creepHealth.OnDestroyed += OnEnemyDestroyed;
            Debug.Log($"Enemy spawned. Remaining enemies: {remainingEnemies}");
        }
    }

    void OnEnemyDestroyed()
    {
        remainingEnemies--;
        Debug.Log($"Enemy destroyed. Remaining enemies: {remainingEnemies}");
        //DropMoney();
    }

    void DropMoney()
    {
        Debug.Log("Enemy dropped money!");
    }
}

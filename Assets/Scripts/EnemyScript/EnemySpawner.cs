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
        yield return new WaitForSeconds(timeBetweenWaves);

        for (int i = 0; i < enemiesInWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }

        while (remainingEnemies > 0)
        {
            yield return null;
        }

        enemiesInWave += 2;
        StartCoroutine(SpawnWave());
    }

    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(Enemy, spawnPoint.position, Quaternion.identity);

        CreepMovement creepMovement = enemy.GetComponent<CreepMovement>();
        if (creepMovement != null && pathPoints.Length > 0)
        {
            creepMovement.pathPoints = pathPoints;
        }

        CreepHealth creepHealth = enemy.GetComponent<CreepHealth>();
        if (creepHealth != null)
        {
            creepHealth.OnDestroyed += OnEnemyDestroyed;
            remainingEnemies++;
        }
    }

    void OnEnemyDestroyed()
    {
        remainingEnemies--;
        DropMoney();
    }

    void DropMoney()
    {
        Debug.Log("Enemy dropped money!");
    }
}

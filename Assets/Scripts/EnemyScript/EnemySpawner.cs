using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject Boss; 
    public Transform spawnPoint;
    public Transform[] pathPoints;
    public int startingEnemies = 2;
    public float timeBetweenWaves = 30f;
    public float spawnDelay = 5f;
    public int maxWaves = 5;

    public DebugDisplay debugDisplay;

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
        while (currentWave <= maxWaves)
        {
            DisplayMessage($"Starting wave {currentWave} with {enemiesInWave} enemies.");

            remainingEnemies = enemiesInWave;

            for (int i = 0; i < enemiesInWave; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(spawnDelay);
            }
            if (GameManager.instance.isLastLevel && currentWave == maxWaves)
            {
                DisplayMessage("Spawning Boss...");
                SpawnBoss();
            }

            DisplayMessage($"All enemies for wave {currentWave} spawned.");

            while (remainingEnemies > 0)
            {
                yield return null;
            }

            DisplayMessage($"Wave {currentWave} complete. Moving to next wave...");
            GameManager.instance.WaveCompleted();

            enemiesInWave += 2;
            IncreaseEnemyHealth(10);
            currentWave++;

            if (currentWave > maxWaves)
            {
                DisplayMessage("Max number of waves reached.");
                GameManager.instance.SetAllWavesCompleted();
                yield break;
            }

            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(Enemy, spawnPoint.position, Quaternion.identity);
        enemy.SetActive(true);

        GameManager.instance.RegisterEnemy();

        CreepMovement creepMovement = enemy.GetComponent<CreepMovement>();
        if (creepMovement != null && pathPoints.Length > 0)
        {
            creepMovement.pathPoints = pathPoints;
        }

        CreepHealth creepHealth = enemy.GetComponent<CreepHealth>();
        if (creepHealth != null)
        {
            creepHealth.OnDestroyed += OnEnemyDestroyed;
            DisplayMessage($"Enemy spawned. Remaining enemies: {remainingEnemies}");
        }
    }

    void SpawnBoss()
    {
        GameObject boss = Instantiate(Boss, spawnPoint.position, Quaternion.identity);
        boss.SetActive(true);

        GameManager.instance.RegisterEnemy();
        GameManager.instance.bossActive = true;

        CreepMovement creepMovement = boss.GetComponent<CreepMovement>();
        if (creepMovement != null && pathPoints.Length > 0)
        {
            creepMovement.pathPoints = pathPoints;
        }

        CreepHealth creepHealth = boss.GetComponent<CreepHealth>();
        if (creepHealth != null)
        {
            creepHealth.OnDestroyed += OnEnemyDestroyed; 
            DisplayMessage("Boss spawned!");
        }
    }
    void OnBossDestroyed()
    {
        OnEnemyDestroyed();
        GameManager.instance.bossActive = false;
    }

    public void OnEnemyDestroyed()
    {
        remainingEnemies--;

        GameManager.instance.EnemyKilled();

        DisplayMessage($"Enemy destroyed. Remaining enemies: {remainingEnemies}");
    }
    void IncreaseEnemyHealth(int amount)
    {
        CreepHealth[] allEnemies = FindObjectsOfType<CreepHealth>();

        foreach (var enemy in allEnemies)
        {
            enemy.maxHealth += amount;
            enemy.currentHealth += amount;
            DisplayMessage($"Increased {enemy.gameObject.name}'s health by {amount}. New Health: {enemy.maxHealth}");
        }
    }

    void DisplayMessage(string message)
    {
        if (debugDisplay != null)
        {
            debugDisplay.UpdateDebugMessage(message);
        }
    }
}

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
    public int levelIndex;

    public DebugDisplay debugDisplay;

    private int currentWave = 1;
    private int enemiesInWave;
    private int remainingEnemies;
    private int healthIncreasePerWave = 10;

    private void Start()
    {
        GameManager.instance.SetLevel(levelIndex);
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
                SpawnEnemy(healthIncreasePerWave * (currentWave - 1));
                yield return new WaitForSeconds(spawnDelay);
            }

            if (GameManager.instance.isLastLevel && currentWave == maxWaves)
            {
                DisplayMessage("Conditions met for spawning Boss.");
                SpawnBoss();
            }
            else
            {
                DisplayMessage("Boss spawn conditions not met. Either not last level or not the final wave.");
            }

            DisplayMessage($"All enemies for wave {currentWave} spawned.");

            while (remainingEnemies > 0)
            {
                yield return null;
            }

            DisplayMessage($"Wave {currentWave} complete. Moving to next wave...");
            GameManager.instance.WaveCompleted();

            enemiesInWave += 2;
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

    void SpawnEnemy(int extraHealth)
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
            creepHealth.maxHealth += extraHealth;
            creepHealth.currentHealth = creepHealth.maxHealth;
            creepHealth.OnDestroyed += OnEnemyDestroyed;

            DisplayMessage($"Enemy spawned with increased health: {creepHealth.maxHealth}");
        }
    }

    void SpawnBoss()
    {
        if (Boss == null)
        {
            DisplayMessage("Boss prefab is not assigned!");
            return;
        }

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
            creepHealth.OnDestroyed += OnBossDestroyed;
            DisplayMessage("Boss spawned successfully!");
        }
        else
        {
            DisplayMessage("Boss does not have CreepHealth component.");
        }
    }

    void OnBossDestroyed()
    {
        OnEnemyDestroyed();
        GameManager.instance.bossActive = false;
        DisplayMessage("Boss defeated!");
    }

    public void OnEnemyDestroyed()
    {
        remainingEnemies--;

        GameManager.instance.EnemyKilled();

        DisplayMessage($"Enemy destroyed. Remaining enemies: {remainingEnemies}");
    }

    void DisplayMessage(string message)
    {
        if (debugDisplay != null)
        {
            debugDisplay.UpdateDebugMessage(message);
        }
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawners : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform[] pathPoints;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    [SerializeField] private float spawnDelay = 5f;
    [SerializeField] private float baseEnemyHealth = 100f;
    [SerializeField] private float healthIncrementPerWave = 5f; 
    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 1;
    private int maxWaves = 5;
    private float timeSinceLastSpawn;
    private int enemiesAlives;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;

    private void Awake()
    {
        onEnemyDestroy.AddListener(OnEnemyDestroyed);
    }

    private void Start()
    {
        StartCoroutine(SpawnWave());
    }

    private void Update()
    {
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlives++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlives == 0 && enemiesLeftToSpawn == 0 && isSpawning)
        {
            EndWave();
        }
    }

    private IEnumerator SpawnWave()
    {
        while (currentWave <= maxWaves)
        {
            DisplayMessage($"Starting wave {currentWave} with {EnemiesPerWave()} enemies.");

            enemiesLeftToSpawn = EnemiesPerWave();
            enemiesAlives = 0;
            isSpawning = true;

            for (int i = 0; i < enemiesLeftToSpawn; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(spawnDelay);
            }

            DisplayMessage($"All enemies for wave {currentWave} spawned.");

            while (enemiesAlives > 0)
            {
                yield return null;
            }

            DisplayMessage($"Wave {currentWave} complete. Moving to next wave...");
            GameManager.instance.WaveCompleted();

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

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        StartCoroutine(SpawnWave());
    }

    private void SpawnEnemy()
    {
        GameObject prefabToSpawn = enemyPrefabs[0];
        GameObject enemy = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
        enemy.SetActive(true);

        GameManager.instance.RegisterEnemy();

        CreepMovement creepMovement = enemy.GetComponent<CreepMovement>();
        if (creepMovement != null && pathPoints.Length > 0)
        {
            creepMovement.pathPoints = pathPoints;
        }

        EnemyHealth creepHealth = enemy.GetComponent<EnemyHealth>();
        if (creepHealth != null)
        {
            creepHealth.OnDestroyed += OnEnemyDestroyed;
            float newHealth = baseEnemyHealth + (healthIncrementPerWave * (currentWave - 1));
            creepHealth.SetHealth(newHealth);
            DisplayMessage($"Enemy spawned with {newHealth} health. Remaining enemies to spawn: {enemiesLeftToSpawn}");
        }
    }

    private void OnEnemyDestroyed()
    {
        enemiesAlives--;

        GameManager.instance.EnemyKilled();

        DisplayMessage($"Enemy destroyed. Remaining enemies alive: {enemiesAlives}");
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    private void DisplayMessage(string message)
    {
        Debug.Log(message);
    }
}
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public NextLevelScreen nextLevelScreen;
    public BaseHealth baseHealth;

    private int enemyCount = 0;
    private bool allWavesCompleted = false;

    // Tower placement control
    public int availableTowers = 1;
    private const int maxTowers = 11;

    public TowerSpotLeft towerPlacementText;

    private void Start()
    {
        UpdateTowerPlacementText();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterEnemy()
    {
        enemyCount++;
    }

    public void EnemyKilled()
    {
        enemyCount--;

        if (enemyCount <= 0 && allWavesCompleted && baseHealth.health > 0)
        {
            LevelCompleted();
        }
    }

    public void WaveCompleted()
    {
        if (availableTowers < maxTowers)
        {
            availableTowers = Mathf.Min(availableTowers + 2, maxTowers);
            Debug.Log("Towers available to place after this wave: " + availableTowers);
            UpdateTowerPlacementText();
        }
    }

    public void SetAllWavesCompleted()
    {
        allWavesCompleted = true;

        if (enemyCount <= 0 && baseHealth.health > 0)
        {
            LevelCompleted();
        }
    }

    private void LevelCompleted()
    {
        Debug.Log("Level Completed!");
        nextLevelScreen.ShowNextLevelScreen("Victory!");
    }

    public bool CanPlaceTower()
    {
        return availableTowers > 0;
    }

    public void UseTower()
    {
        if (availableTowers > 0)
        {
            availableTowers--;
            UpdateTowerPlacementText();
        }
    }

    private void UpdateTowerPlacementText()
    {
        if (towerPlacementText != null && towerPlacementText.towerplacementText != null)
        {
            towerPlacementText.towerplacementText.text = "Towers Available: " + availableTowers;
        }
        else
        {
            Debug.LogWarning("Tower Placement Text or TextMeshPro component is not assigned in the GameManager.");
        }
    }
}

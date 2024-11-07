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
    private const int maxMines = 8;
    public int availableMines = 0;
    public TowerSpotLeft towerPlacementText;
    public MineSpotLeft mineSpotLeft;

    // Reference to AudioManagerScene
    public AudioManagerScene audioManager;

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
        if (audioManager != null && audioManager.SFXSource != null && audioManager.enemydie != null)
        {
            audioManager.SFXSource.PlayOneShot(audioManager.enemydie);
        }
        else
        {
            Debug.LogWarning("AudioManagerScene or SFXSource or enemydie clip is not assigned.");
        }
        if (enemyCount <= 0 && allWavesCompleted && baseHealth.health > 0)
        {
            LevelCompleted();
        }
    }

    public void WaveCompleted()
    {
        if (availableTowers < maxTowers && availableMines < maxMines)
        {
            availableTowers = Mathf.Min(availableTowers + 2, maxTowers);
            Debug.Log("Towers available to place after this wave: " + availableTowers);
            UpdateTowerPlacementText();
            availableMines = Mathf.Min(availableMines + 1, maxMines);
            Debug.Log("Mines available to place after this wave: " + availableMines);
            UpdateMinePlacementText();
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
    public bool CanPlaceMine()
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
    public void UseMine()
    {
        if (availableMines > 0)
        {
            availableMines--;
            UpdateMinePlacementText();
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
    private void UpdateMinePlacementText()
    {
        if (mineSpotLeft != null && mineSpotLeft.mineplacementText != null)
        {
            mineSpotLeft.mineplacementText.text = "Mines Available: " + availableMines;
        }
        else
        {
            Debug.LogWarning("Mine Placement Text or TextMeshPro component is not assigned in the GameManager.");
        }
    }
}

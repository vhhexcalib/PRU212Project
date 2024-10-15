using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public int totalHealth = 5;
    public GameObject heartPrefab;
    public Transform heartsContainer;
    public TextMeshProUGUI healthText;

    private void Start()
    {
        UpdateHeartsUI();
        UpdateHealth(totalHealth);
    }

    public void UpdateHeartsUI()
    {
        foreach (Transform child in heartsContainer)
        {
            Destroy(child.gameObject);
        }

        // Create hearts based on totalHealth
        for (int i = 0; i < totalHealth; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartsContainer);
            heart.name = "Heart" + (i + 1);
        }
    }

    public void UpdateHealth(int currentHealth)
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, totalHealth);

        for (int i = 0; i < heartsContainer.childCount; i++)
        {
            Transform heart = heartsContainer.GetChild(i);
            heart.gameObject.SetActive(i < currentHealth);
        }

        // Update the health text
        healthText.text = currentHealth + "/" + totalHealth;
    }

    public void SetTotalHealth(int newTotalHealth)
    {
        totalHealth = newTotalHealth;
        UpdateHeartsUI();
        UpdateHealth(totalHealth);
    }
}

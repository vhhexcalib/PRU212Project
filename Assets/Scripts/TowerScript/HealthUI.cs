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
        for (int i = 0; i < totalHealth; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartsContainer);
            heart.name = "Heart" + (i + 1);
        }

        UpdateHealth(totalHealth);
    }

    public void UpdateHealth(int currentHealth)
    {
        for (int i = 0; i < heartsContainer.childCount; i++)
        {
            Transform heart = heartsContainer.GetChild(i);
            heart.gameObject.SetActive(i < currentHealth);
        }

        // Update the health text
        healthText.text = currentHealth + "/" + totalHealth;
    }
}

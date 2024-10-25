using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public TextMeshProUGUI healthText;

    private void Start()
    {
        UpdateHealth(10);
    }

    public void UpdateHealth(int currentHealth)
    {
        healthText.text = currentHealth.ToString();
    }
}

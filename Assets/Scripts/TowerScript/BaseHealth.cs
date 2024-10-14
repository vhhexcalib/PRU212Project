using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public int health = 10;
    public HealthUI healthUI;

    public void TakeDamage(int damage)
    {
        health -= damage;

        // Update the health UI
        healthUI.UpdateHealth(health);

        if (health <= 0)
        {
            Debug.Log("Base destroyed!");
        }
    }
}


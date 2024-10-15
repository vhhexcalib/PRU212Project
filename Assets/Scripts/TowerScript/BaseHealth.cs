using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public int health = 10;
    public HealthUI healthUI;
    public GameOverManager gameOverManager;

    public void TakeDamage(int damage)
    {
        health = Mathf.Max(health - damage, 0);

        healthUI.UpdateHealth(health);

        if (health <= 0)
        {
            DestroyBase();
        }
    }

    void DestroyBase()
    {
        Debug.Log("Base destroyed!");
        gameOverManager.GameOver();
    }
}

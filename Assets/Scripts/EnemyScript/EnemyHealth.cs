using Unity.VisualScripting;
using UnityEngine;

public class CreepHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public delegate void EnemyDestroyed();
    public event EnemyDestroyed OnDestroyed;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Creep took damage: " + damage + ", Current Health: " + currentHealth);

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Creep died.");
        OnDestroyed?.Invoke();
        Destroy(gameObject);
    }
}

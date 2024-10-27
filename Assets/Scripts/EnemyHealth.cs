using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;
    public delegate void EnemyDestroyed();
    public event EnemyDestroyed OnDestroyed;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void SetHealth(float newHealth)
    {
        maxHealth = newHealth;
        currentHealth = newHealth;
        Debug.Log("Enemy health set to: " + newHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy took damage: " + damage + ", Current Health: " + currentHealth);
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died.");
        OnDestroyed?.Invoke();
        EnemySpawners.onEnemyDestroy.Invoke();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Base"))
        {
            Debug.Log("Enemy reached the base and died.");
            OnDestroyed?.Invoke();
            EnemySpawners.onEnemyDestroy.Invoke();
            Destroy(gameObject);
        }
    }
}

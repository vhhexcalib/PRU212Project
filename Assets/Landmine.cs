using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmine : MonoBehaviour
{
    public float explosionRadius = 5f;
    public float explosionDamage = 25f; // Damage set to 25
    public float delayBeforeExplosion = 2f;

    private void Start()
    {
        Invoke("Explode", delayBeforeExplosion);
    }

    private void Explode()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (var enemyCollider in hitEnemies)
        {
            if (enemyCollider.CompareTag("Creep"))
            {
                CreepHealth creepHealth = enemyCollider.GetComponent<CreepHealth>();
                if (creepHealth != null)
                {
                    creepHealth.TakeDamage(explosionDamage);
                }
            }
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Creep"))
        {
            CreepHealth creepHealth = collision.GetComponent<CreepHealth>();
            if (creepHealth != null)
            {
                creepHealth.TakeDamage(explosionDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

}

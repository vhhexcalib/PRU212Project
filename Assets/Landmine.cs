using System.Collections;
using UnityEngine;

public class Landmine : MonoBehaviour
{
    public float explosionRadius = 5f;
    public float explosionDamage = 25f;
    public float delayBeforeExplosion = 2f;

    private bool isArmed = false;
    private Collider2D landmineCollider;

    private void Start()
    {
        // Get the collider reference
        landmineCollider = GetComponent<Collider2D>();

        // Temporarily disable the collider on spawn
        landmineCollider.enabled = false;

        // Arm the mine after a short delay
        StartCoroutine(ArmMineAfterDelay());
    }

    IEnumerator ArmMineAfterDelay()
    {
        // Wait for a small time before arming the landmine
        yield return new WaitForSeconds(5f);

        // Enable the collider after delay
        landmineCollider.enabled = true;
        isArmed = true;

        Invoke("Explode", delayBeforeExplosion);
    }

    private void Explode()
    {
        // Perform explosion logic
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

        // Destroy landmine after explosion
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Trigger explosion only if the mine is armed
        if (isArmed && collision.CompareTag("Creep"))
        {
            Explode();
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a wireframe sphere for the explosion radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}

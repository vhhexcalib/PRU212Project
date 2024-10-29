using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float attackRange = 3f;
    public float fireRate = 1f;
    private float fireCooldown = 0f;

    public GameObject Arrow;
    public Transform firePoint;
    private Transform target;

    // Reference to AudioManagerScene
    private AudioManagerScene audioManager;

    private void Start()
    {
        // Find the AudioManagerScene object in the scene
        audioManager = FindObjectOfType<AudioManagerScene>();
    }

    void Update()
    {
        FindTarget();

        if (target != null && fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / fireRate;
        }

        fireCooldown -= Time.deltaTime;
    }

    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Creep");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= attackRange)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void Shoot()
    {
        if (firePoint == null)
        {
            Debug.LogError("FirePoint is not assigned!");
            return;
        }

        // Instantiate arrow and make it seek the target
        GameObject projectile = Instantiate(Arrow, firePoint.position, firePoint.rotation);
        Projectile projScript = projectile.GetComponent<Projectile>();

        if (projScript != null)
        {
            projScript.Seek(target);
        }

        // Play sound based on tower's tag
        if (audioManager != null && audioManager.SFXSource != null)
        {
            if (CompareTag("Wizard") && audioManager.shooting != null)
            {
                audioManager.SFXSource.PlayOneShot(audioManager.shooting);
            }
            else if (audioManager.archershooting != null)
            {
                audioManager.SFXSource.PlayOneShot(audioManager.archershooting);
            }
        }
        else
        {
            Debug.LogWarning("AudioManagerScene, SFXSource or sound clips are not assigned.");
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

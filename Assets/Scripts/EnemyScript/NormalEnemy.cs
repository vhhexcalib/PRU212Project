using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage = 1;

    void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Base"))
        {
            BaseHealth baseHealth = other.GetComponent<BaseHealth>();

            if (baseHealth != null)
            {
                baseHealth.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}

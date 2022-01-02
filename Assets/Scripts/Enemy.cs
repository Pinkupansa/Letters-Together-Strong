using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth;
    private float currentHealth;
    private bool canTakeDamage = true;
    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        canTakeDamage = false;
        StartCoroutine(InvulnerabilityFrames());
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator InvulnerabilityFrames()
    {
        yield return new WaitForSeconds(0.2f);
        canTakeDamage = true;
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    public bool CanTakeDamage()
    {
        return canTakeDamage;
    }
}

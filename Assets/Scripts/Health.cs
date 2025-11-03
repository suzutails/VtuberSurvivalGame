using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 10f;
    private float currentHealth;
    private DamageFlash damageFlash;

    void Start()
    {
        currentHealth = maxHealth;
        damageFlash = GetComponent<DamageFlash>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage! HP: {currentHealth}");

        if (damageFlash != null)
            damageFlash.Flash();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} has died!");
        Destroy(gameObject); // later replace with death animation or respawn
    }
}

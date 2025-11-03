using UnityEngine;

public class EnemyDamagePlayer : MonoBehaviour
{
    public float damage = 1f;
    public float attackCooldown = 1f;

    private float lastAttackTime;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                Health playerHealth = collision.GetComponent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                    lastAttackTime = Time.time;
                }
            }
        }
    }
}

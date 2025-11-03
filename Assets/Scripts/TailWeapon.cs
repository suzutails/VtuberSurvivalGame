using UnityEngine;

public class TailWeapon : MonoBehaviour
{
    public float swingCooldown = 1f;     // seconds between swings
    public float damage = 1f;            // tail damage
    private float lastSwingTime;

    private PlayerController player;     // reference to player
    private Collider2D col;

    void Start()
    {
        player = GetComponentInParent<PlayerController>();
        col = GetComponent<Collider2D>();
        col.enabled = false; // initially off
    }

    void Update()
    {
        if (player == null || player.moveInput.magnitude == 0) return;

        if (Time.time - lastSwingTime >= swingCooldown)
        {
            SwingTail(player.moveInput);
            lastSwingTime = Time.time;
        }
    }

    void SwingTail(Vector2 direction)
    {
        // Rotate tail toward movement direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Enable collider briefly to hit enemies
        StartCoroutine(DoSwing());
    }

    System.Collections.IEnumerator DoSwing()
    {
        col.enabled = true;
        yield return new WaitForSeconds(0.1f); // tail active for 0.1s
        col.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Health enemyHealth = other.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
    }
}

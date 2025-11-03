using UnityEngine;
using System.Collections;

public class TailWeaponArcTopDownWASD : MonoBehaviour
{
    [Header("Tail Settings")]
    public float swingCooldown = 1f;    // seconds between swings
    public float swingDuration = 0.2f;  // how long the swing lasts
    public float swingAngle = 90f;      // total arc in degrees
    public float damage = 1f;           // damage per hit
    public float spriteOffset = 0f;     // tweak if sprite isn’t exactly facing right

    private float lastSwingTime;
    private Collider2D col;
    private SpriteRenderer spriteRenderer;
    private PlayerController player;

    void Start()
    {
        col = GetComponent<Collider2D>();
        col.enabled = false;

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            spriteRenderer.enabled = false;

        player = GetComponentInParent<PlayerController>();
    }

    void Update()
    {
        // Get WASD input manually if PlayerController doesn't handle it
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (moveInput.magnitude == 0) return;

        if (Time.time - lastSwingTime >= swingCooldown)
        {
            StartCoroutine(Swing(moveInput));
            lastSwingTime = Time.time;
        }
    }

    private IEnumerator Swing(Vector2 facingDir)
    {
        if (spriteRenderer != null) spriteRenderer.enabled = true;
        col.enabled = true;

        float midAngle = Mathf.Atan2(facingDir.y, facingDir.x) * Mathf.Rad2Deg + spriteOffset;
        float halfAngle = swingAngle / 2f;
        float elapsed = 0f;

        Quaternion startRot = Quaternion.Euler(0, 0, midAngle - halfAngle);
        Quaternion endRot = Quaternion.Euler(0, 0, midAngle + halfAngle);

        while (elapsed < swingDuration)
        {
            float t = elapsed / swingDuration;
            transform.localRotation = Quaternion.Lerp(startRot, endRot, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = endRot;
        col.enabled = false;

        if (spriteRenderer != null) spriteRenderer.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Health enemyHealth = other.GetComponent<Health>();
            if (enemyHealth != null)
                enemyHealth.TakeDamage(damage);
        }
    }
}

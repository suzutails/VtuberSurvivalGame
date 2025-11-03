using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float moveSpeed = 2f;       // How fast the enemy moves
    public float stopDistance = 0.3f;  // How close they get before stopping (optional)

    private Transform target;          // The player

    void Start()
    {
        // Find the player by tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            target = player.transform;
    }

    void Update()
    {
        if (target == null) return;

        // Direction towards the player
        Vector2 direction = (target.position - transform.position).normalized;

        // Only move if far enough away
        float distance = Vector2.Distance(transform.position, target.position);
        if (distance > stopDistance)
        {
            transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
        }
    }
}

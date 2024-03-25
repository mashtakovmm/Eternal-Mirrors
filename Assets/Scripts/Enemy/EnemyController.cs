using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private PlayerContorller player;
    private Vector2 playerPosition;
    private Rigidbody2D rb;
    void Start()
    {
        player = FindObjectOfType<PlayerContorller>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        playerPosition = player.transform.position;

        Vector2 direction = (playerPosition - (Vector2)transform.position).normalized;

        rb.velocity = direction * 10;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Bullet>())
        {
            Debug.Log("Enemy hit");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Player")
        {
            Debug.Log("Player hit");
        }
    }
}

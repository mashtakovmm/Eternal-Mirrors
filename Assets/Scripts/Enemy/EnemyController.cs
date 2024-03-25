using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Data SO")]
    [SerializeField] private EnemySO dataSO;
    private PlayerContorller player;
    private Vector2 playerPosition;
    private Rigidbody2D rb;

    // Data SO stuff
    private int health;
    private int score;
    private float speed;
    private int damage;

    private void Awake()
    {
        health = dataSO.Health;
        score = dataSO.Score;
        speed = dataSO.Speed;
        damage = dataSO.Damage;
    }

    void Start()
    {
        player = FindObjectOfType<PlayerContorller>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        playerPosition = player.transform.position;

        Vector2 direction = (playerPosition - (Vector2)transform.position).normalized;

        rb.velocity = direction * speed;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Bullet>())
        {
            health -= other.GetComponent<Bullet>().damage;
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Player")
        {
            if (other.gameObject.GetComponent<PlayerContorller>() != null)
            {
                player.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}

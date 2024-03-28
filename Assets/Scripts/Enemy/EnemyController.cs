using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Data SO")]
    [SerializeField] private EnemySO dataSO;
    private PlayerContorller player;
    private Vector2 playerPosition;
    private Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    EnemySpawner spawner;
    MoneyManager moneyManager;

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

    public void ApplyWaveDiff(int wave)
    {
        health = health * wave / 10;
        score = score * wave / 10;
        speed = speed * wave / 30;
        damage = damage * wave / 10;
    }

    void Start()
    {
        spawner = FindObjectOfType<EnemySpawner>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerContorller>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        playerPosition = player.transform.position;

        Vector2 direction = (playerPosition - (Vector2)transform.position).normalized;

        if (direction != Vector2.zero)
        {
            spriteRenderer.flipX = direction.x < 0;
        }

        rb.velocity = direction * speed;
        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // transform.rotation = Quaternion.Euler(0, 0, angle);
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
        moneyManager.AddMoney(Random.Range(1, 4));
        spawner.kills++;
        Destroy(gameObject);
    }
}

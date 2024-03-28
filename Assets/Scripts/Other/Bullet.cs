using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public float speed;
    [HideInInspector] public int damage;
    [SerializeField] private bool isPlayerBullet;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Start()
    {
        rb.velocity = transform.right * speed;
        Destroy(gameObject, 1.22f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isPlayerBullet && other.transform.tag != "Player" && other.transform.tag != "Plane")
        {
            Destroy(gameObject);
        }
    }
}

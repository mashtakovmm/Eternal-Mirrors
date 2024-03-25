using System;
using UnityEngine;

public class PlayerContorller : MonoBehaviour
{
    [Header("InputReader")]
    [SerializeField] private InputReader inputReader;

    [Header("Controls Variables")]
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;

    private Vector2 move;
    private Vector2 rawMousePos;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        Look();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Look()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(rawMousePos);
        mousePosition.z = 0;

        Vector2 direction = mousePosition - transform.position;

        float angle = Vector2.SignedAngle(Vector2.right, direction);

        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void Move()
    {
        rb.velocity = move * speed;
    }

    private void OnEnable()
    {
        inputReader.MoveEvent += HandleMoveEvent;
        inputReader.LookEvent += HandleLookEvent;
    }

    private void OnDisable()
    {
        inputReader.MoveEvent -= HandleMoveEvent;
        inputReader.LookEvent -= HandleLookEvent;
    }

    private void HandleMoveEvent(Vector2 vector2)
    {
        move = vector2;
    }

    private void HandleLookEvent(Vector2 vector2)
    {
        rawMousePos = vector2;
    }
}

using System;
using UnityEngine;

public class PlayerContorller : MonoBehaviour
{
    [Header("InputReader")]
    [SerializeField] private InputReader inputReader;

    [Header("Controls Variables")]
    [SerializeField] private float speed;


    private Vector2 move;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }


    private void Move()
    {
        rb.velocity = move * speed;
    }

    private void OnEnable()
    {
        inputReader.MoveEvent += HandleMoveEvent;
    }

    private void OnDisable()
    {
        inputReader.MoveEvent -= HandleMoveEvent;
    }

    private void HandleMoveEvent(Vector2 vector2)
    {
        move = vector2;
    }
}

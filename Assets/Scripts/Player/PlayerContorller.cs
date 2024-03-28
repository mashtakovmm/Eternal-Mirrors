using System.Collections.Generic;
using UnityEngine;

public class PlayerContorller : MonoBehaviour
{
    [Header("InputReader")]
    [SerializeField] private InputReader inputReader;

    [Header("Controls Variables")]
    [SerializeField] private float speed;
    [Header("Stats")]
    [SerializeField] private int _health;

    private List<IInteractable> interactables;
    private Animator animator;
    private Vector2 move;
    SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        interactables = new List<IInteractable>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        if (interactables.Count != 0 && interactables != null)
        {
            interactables[0].Highligh();
        }
    }


    private void Move()
    {
        rb.velocity = move * speed;
        animator.SetBool("isRunning", rb.velocity != Vector2.zero);
    }

    private void OnEnable()
    {
        inputReader.MoveEvent += HandleMoveEvent;
        inputReader.InteractEvent += HandleInteract;
    }

    private void OnDisable()
    {
        inputReader.MoveEvent -= HandleMoveEvent;
        inputReader.InteractEvent -= HandleInteract;
    }

    private void HandleInteract()
    {
        if (interactables.Count != 0 && interactables != null)
        {
            interactables[0].Interact(gameObject);
        }
    }

    private void HandleMoveEvent(Vector2 vector2)
    {
        move = vector2;
        if (move != Vector2.zero)
        {
            spriteRenderer.flipX = move.x < 0;
        }
    }

    public void TakeDamage(int value)
    {
        _health -= value;
        if (_health <= 0)
        {
            Die();
        }
        Debug.Log(_health);
    }

    private void Die()
    {
        Debug.Log("PLAYER DEAD");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            Debug.Log("Mirror");
            interactables.Add(interactable);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interactable.RemoveHighligh();
            interactables.Remove(interactable);
        }
    }
}

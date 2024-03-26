using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour, IInteractable
{
    [Header("Connected to:")]
    [SerializeField] GameObject connectedMirror;
    Vector2 connctedMirrorPosition;
    SpriteRenderer spriteRenderer;
    Color defaultColor;

    void Start()
    {
        connctedMirrorPosition = connectedMirror.transform.position;

        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Highligh()
    {
        spriteRenderer.color = Color.blue;
    }

    public void RemoveHighligh()
    {
        spriteRenderer.color = defaultColor;
    }

    public void Interact(GameObject gameObject)
    {
        gameObject.transform.position = connctedMirrorPosition;
    }

}

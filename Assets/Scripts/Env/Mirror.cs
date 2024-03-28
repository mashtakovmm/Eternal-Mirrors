using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour, IInteractable
{
    [Header("Connected to:")]
    [SerializeField] GameObject connectedMirror;
    GameManager gameManager;
    Vector2 connctedMirrorPosition;
    SpriteRenderer spriteRenderer;
    Color defaultColor;
    [SerializeField] private MirrorType type = MirrorType.Shop;
    public string tipText;
    private enum MirrorType
    {
        Shop,
        Arena
    }

    void Start()
    {
        connctedMirrorPosition = connectedMirror.transform.position;
        gameManager = FindObjectOfType<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Highligh()
    {
        spriteRenderer.color = new Color(0.5f, 0.7f, 1f, 1f);
    }

    public void RemoveHighligh()
    {
        spriteRenderer.color = defaultColor;
    }

    public void Interact(GameObject gameObject)
    {
        if (type == MirrorType.Shop)
        {
            gameManager.EnterState(GameManager.GameState.Wave);
            EnableMirrors(false);
        }
        gameObject.transform.position = connctedMirrorPosition;
    }

    public void EnableMirrors(bool value)
    {
        gameObject.SetActive(value);
        connectedMirror.SetActive(value);
    }

    public string GetTip()
    {
        return $"Press E to enter";
    }
}

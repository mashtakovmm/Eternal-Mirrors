using UnityEngine;

public class GameManager : MonoBehaviour
{
    EnemySpawner spawner;
    Shop shop;
    [SerializeField]Mirror mirror;
    public enum GameState
    {
        Wave,
        Shopping
    }

    public GameState gameState;

    private void Start()
    {
        spawner = FindObjectOfType<EnemySpawner>();
        shop = FindObjectOfType<Shop>();
        EnterState(GameState.Shopping);
    }

    public void EnterState(GameState state)
    {
        switch (state)
        {
            case GameState.Shopping:
                gameState = GameState.Shopping;
                shop.SpawnItems();
                Debug.Log("shopping");
                mirror.EnableMirrors(true);
                break;
            case GameState.Wave:
                shop.ClearShop();
                gameState = GameState.Wave;
                Debug.Log("wave");
                spawner.StartSpawn();
                break;
            default:
                Debug.LogWarning("Unknown state type.");
                break;
        }
    }
}

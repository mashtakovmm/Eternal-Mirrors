using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject testEnemyPrefab;

    [Header("Wave Data")]
    [SerializeField] private SpawnSettingsSO waveData;

    [Header("Plane Bounds")]
    [SerializeField] private GameObject plane;
    Tilemap tilemap;
    public int kills = 0;
    private PolygonCollider2D spawnArea;
    [SerializeField] GameManager gameManager;
    float maxSpawnTime;
    float minSpawnTime;
    int waveWeight;
    private Coroutine spawnCoroutine;
    bool isSpawning;


    void Start()
    {
        tilemap = plane.GetComponent<Tilemap>();
        maxSpawnTime = waveData.MaxSpawnTime;
        minSpawnTime = waveData.MinSpawnTime;
        waveWeight = waveData.WaveWeight;
    }

    private void Update()
    {
        if (kills >= 2)
        {
            kills = 0;
            gameManager.EnterState(GameManager.GameState.Shopping);
            StopSpawning();
        }
    }

    public void StartSpawn()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
        isSpawning = true;
        spawnCoroutine = StartCoroutine(SpawnCourutine());
    }

    public void StopSpawning()
    {
        Debug.Log("Stop");
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
        isSpawning = false;

        foreach (Transform child in transform)
        {
            Debug.Log("destroy");
            Destroy(child.gameObject);
        }
    }

    private IEnumerator SpawnCourutine()
    {
        yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

        Vector3 spawnPoint = new Vector3(Random.Range(-tilemap.size.x / 2, tilemap.size.x / 2), Random.Range(-tilemap.size.y / 2, tilemap.size.y / 2), 0);
        spawnPoint += plane.transform.position;

        Instantiate(testEnemyPrefab, spawnPoint, Quaternion.identity, transform);

        if (isSpawning)
        {
            StartCoroutine(SpawnCourutine());
        }
    }
}

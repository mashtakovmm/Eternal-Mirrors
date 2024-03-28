using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject testEnemyPrefab;
    [SerializeField] private GameObject smokePrefab;
    private int smokeCount;

    [Header("Wave Data")]
    [SerializeField] private SpawnSettingsSO waveData;

    [Header("Plane Bounds")]
    [SerializeField] private GameObject plane;
    Tilemap tilemap;
    [SerializeField] int killsToEnd = 10;
    public int kills = 0;
    private PolygonCollider2D spawnArea;
    [SerializeField] GameManager gameManager;
    float maxSpawnTime;
    float minSpawnTime;
    int waveWeight;
    private Coroutine spawnCoroutine;
    bool isSpawning;
    private int wave = 0;
    public int Wave => wave;


    void Start()
    {
        tilemap = plane.GetComponent<Tilemap>();
        maxSpawnTime = waveData.MaxSpawnTime;
        minSpawnTime = waveData.MinSpawnTime;
        waveWeight = waveData.WaveWeight;
    }

    private void Update()
    {
        if (kills >= killsToEnd)
        {
            kills = 0;
            gameManager.EnterState(GameManager.GameState.Shopping);
            StopSpawning();
        }
    }

    public void StartSpawn()
    {
        wave++;
        killsToEnd += wave * 10;
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
        smokeCount = 0;
    }

    private IEnumerator SpawnCourutine()
    {
        yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

        List<Vector3Int> validTiles = new List<Vector3Int>();
        for (int x = tilemap.cellBounds.xMin; x < tilemap.cellBounds.xMax; x++)
        {
            for (int y = tilemap.cellBounds.yMin; y < tilemap.cellBounds.yMax; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                if (tilemap.HasTile(cellPosition))
                {
                    validTiles.Add(cellPosition);
                }
            }
        }

        if (validTiles.Count > 0)
        {
            int randomIndex = Random.Range(0, validTiles.Count);
            Vector3Int selectedTile = validTiles[randomIndex];

            Vector3 spawnPoint = tilemap.CellToWorld(selectedTile);

            Instantiate(testEnemyPrefab, spawnPoint, Quaternion.identity, transform);
            if (smokeCount < 3)
            {
                Instantiate(smokePrefab, spawnPoint, Quaternion.identity, transform);
                smokeCount ++;
            }
            EnemyController enemyController = testEnemyPrefab.GetComponent<EnemyController>();
            enemyController.ApplyWaveDiff(wave);
        }

        if (isSpawning)
        {
            StartCoroutine(SpawnCourutine());
        }
    }
}

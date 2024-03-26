using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject testEnemyPrefab;

    [Header("Wave Data")]
    [SerializeField] private SpawnSettingsSO waveData;

    [Header("Plane Bounds")]
    [SerializeField] private GameObject plane;
    private PolygonCollider2D spawnArea;

    float maxSpawnTime;
    float minSpawnTime;
    int waveWeight;

    void Start()
    {
        spawnArea = plane.GetComponent<PolygonCollider2D>();
        if (spawnArea == null)
        {
            Debug.LogError("No collider on plane!!");
        }

        maxSpawnTime = waveData.MaxSpawnTime;
        minSpawnTime = waveData.MinSpawnTime;
        waveWeight = waveData.WaveWeight;

        StartCoroutine(SpawnCourutine());
    }

    void Update()
    {

    }

    private IEnumerator SpawnCourutine()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(minSpawnTime, maxSpawnTime));

        Vector3 spawnPoint = new Vector3(UnityEngine.Random.Range(-plane.transform.localScale.x / 2,
        plane.transform.localScale.x / 2), UnityEngine.Random.Range(-plane.transform.localScale.y / 2, plane.transform.localScale.y / 2), 0);

        spawnPoint += plane.transform.position;

        Instantiate(testEnemyPrefab, spawnPoint, Quaternion.identity);

        StartCoroutine(SpawnCourutine());
    }
}

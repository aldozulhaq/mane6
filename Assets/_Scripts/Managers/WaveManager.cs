using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Serializable]
    public struct Wave
    {
        public GameObject enemyPrefab;
        public float spawnTiming;
        public int count;
    }

    [SerializeField] List<Wave> waves;
    [SerializeField] Vector3 minBoundary;
    [SerializeField] Vector3 maxBoundary;
    [SerializeField] GameObject testEnemy;

    private Vector3 randomPosition;
    private GameObject player;

    private void OnEnable()
    {
        TimeManager.OnTimeSpentE += InstantiateOnTime;
    }

    private void OnDisable()
    {
        TimeManager.OnTimeSpentE -= InstantiateOnTime;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        RandomizePoint();
    }

    private void InstantiateOnTime(float time)
    {
        foreach (Wave wave in waves)
        {
            if (wave.spawnTiming == time)
                BatchSpawn(wave.enemyPrefab, wave.count);
        }
    }

    [ContextMenu("Test Instantiate")]
    public void TestInstantiate()
    {
        StartCoroutine(InstantiateEnemiesCoroutine(testEnemy));
    }

    private IEnumerator InstantiateEnemiesCoroutine(GameObject enemyPrefab)
    {
        RandomizePoint();
        while (Vector3.Distance(randomPosition, player.transform.position) < 5f)
        {
            RandomizePoint();
        }

        Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        yield return null;
    }

    private void BatchSpawn(GameObject enemy, int spawnCount)
    {
        for (int i = 0; i < spawnCount; i++)
        {
            StartCoroutine(InstantiateEnemiesCoroutine(enemy));
        }
    }

    private void RandomizePoint()
    {
        randomPosition = new Vector3(UnityEngine.Random.Range(minBoundary.x, maxBoundary.x),
                                     0f,
                                     UnityEngine.Random.Range(minBoundary.z, maxBoundary.z));
    }
}

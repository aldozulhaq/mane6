using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class WaveManager : MonoBehaviour
{
    [Serializable]
    public struct Wave
    {
        public Enemy enemyPrefab;
        public float spawnTimeIntervalMin;
        public float spawnTimeIntervalMax;
        
        public int spawnCountMin;
        public int spawnCountMax;
    }

    [SerializeField] List<Wave> waves;
    [SerializeField] Vector3 minBoundary;
    [SerializeField] Vector3 maxBoundary;
    [SerializeField] List<Enemy> enemiesPrefab;     // Input various type of enemies here

    private int currentWaveIndex;
    private Vector3 randomPosition;
    private GameObject player;
    private Dictionary<string, ObjectPool<Enemy>> enemyPool;
    private bool isWaveRunning;

    private void OnEnable()
    {
        GameplayEvents.OnWaveStartE += OnWaveStart;
        GameplayEvents.OnWaveEndE += OnWaveEnd;
    }

    private void OnDisable()
    {
        GameplayEvents.OnWaveStartE -= OnWaveStart;
        GameplayEvents.OnWaveEndE -= OnWaveEnd;
    }

    private void Start()
    {
        enemyPool = new Dictionary<string, ObjectPool<Enemy>>();
        foreach (Enemy enemyPrefab in enemiesPrefab)
        {
            ObjectPool<Enemy> _objectPool = new ObjectPool<Enemy>(() =>
                                               {
                                                   return Instantiate(enemyPrefab);
                                               }, enemy =>
                                               {
                                                   enemy.gameObject.SetActive(true);
                                               }, enemy =>
                                               {
                                                   enemy.gameObject.SetActive(false);
                                               }, enemy =>
                                               {
                                                   Destroy(enemy.gameObject);
                                               }, false, 10, 100);
            enemyPool.Add(enemyPrefab.GetNameTag(), _objectPool);
        }

        player = GameObject.FindGameObjectWithTag("Player");
        currentWaveIndex = 0;
        RandomizePoint();
    }

    private IEnumerator InstantiateOnTime()
    {
        yield return new WaitForSeconds(1f);

        while (isWaveRunning)
        {
            float randomInterval = UnityEngine.Random.Range(waves[currentWaveIndex].spawnTimeIntervalMin, waves[currentWaveIndex].spawnTimeIntervalMax);
            yield return new WaitForSeconds(randomInterval);

            int randomSpawnCount = UnityEngine.Random.Range(waves[currentWaveIndex].spawnCountMin, waves[currentWaveIndex].spawnCountMax);
            BatchSpawn(waves[currentWaveIndex].enemyPrefab, randomSpawnCount);
        }
    }

    private IEnumerator InstantiateEnemiesCoroutine(Enemy enemyPrefab)
    {
        RandomizePoint();
        while (Vector3.Distance(randomPosition, player.transform.position) < 5f)
        {
            RandomizePoint();
        }

        //Instantiate(enemyPrefab);
        var instantiatedEnemy = enemyPool[enemyPrefab.GetNameTag()].Get();
        instantiatedEnemy.transform.position = randomPosition;
        instantiatedEnemy.Init(KillEnemy);
        yield return null;
    }

    private void KillEnemy(Enemy enemyPrefab)
    {
        Debug.Log(this);
        enemyPool[enemyPrefab.GetNameTag()].Release(enemyPrefab);
        //Destroy(enemy.gameObject);
    }

    private void BatchSpawn(Enemy enemy, int spawnCount)
    {
        if (!isWaveRunning)
            return;

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

    public void OnWaveStart()
    {
        isWaveRunning = true;
        StartCoroutine(InstantiateOnTime());
    }

    private void OnWaveEnd()
    {
        isWaveRunning = false;
        StopCoroutine(InstantiateOnTime());
        currentWaveIndex++;
    }
}

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
        public Enemy[] enemyPrefabs;
        public Enemy bossPrefab;
        public int spawnCount;
        //public float spawnTimeIntervalMin;
        //public float spawnTimeIntervalMax;
        
        //public int spawnCountMin;
        //public int spawnCountMax;
    }

    [SerializeField] List<Wave> waves;
    [SerializeField] Vector3 minBoundary;
    [SerializeField] Vector3 maxBoundary;
    [SerializeField] List<Enemy> enemiesPrefab;     // Input various type of enemies here

    [Header("Event Channels")]
    [SerializeField] EventChannel onWaveEnd;
    [SerializeField] IntEventChannel onEnemyDie;

    private int currentWaveIndex;
    private Vector3 randomPosition;
    private GameObject player;
    private Dictionary<string, ObjectPool<Enemy>> enemyPool;
    private bool isWaveRunning;
    private int instantiatedEnemies = 0;
    private int enemiesKilled;

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
        int currentWaveEnemiesCount = waves[currentWaveIndex].spawnCount;
        yield return new WaitForSeconds(1f);

        while (instantiatedEnemies < currentWaveEnemiesCount)
        {
            /*float randomInterval = UnityEngine.Random.Range(waves[currentWaveIndex].spawnTimeIntervalMin, waves[currentWaveIndex].spawnTimeIntervalMax);
            yield return new WaitForSeconds(randomInterval);*/

            //int randomSpawnCount = UnityEngine.Random.Range(waves[currentWaveIndex].spawnCountMin, waves[currentWaveIndex].spawnCountMax);
            //BatchSpawn(waves[currentWaveIndex].enemyPrefab, randomSpawnCount);

            int randomEnemiesIndex = UnityEngine.Random.Range(0, waves[currentWaveIndex].enemyPrefabs.Length);
            Debug.Log(randomEnemiesIndex);
            StartCoroutine(InstantiateEnemiesCoroutine(waves[currentWaveIndex].enemyPrefabs[randomEnemiesIndex]));

            yield return new WaitForSeconds(1f);
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
        instantiatedEnemy.transform.localRotation = Quaternion.Euler(Vector3.zero);
        instantiatedEnemy.Init(KillEnemy);
        instantiatedEnemies++;
        yield return null;
    }

    private void KillEnemy(Enemy enemyPrefab)
    {
        enemyPool[enemyPrefab.GetNameTag()].Release(enemyPrefab);
        enemiesKilled++;
        onEnemyDie.Invoke();

        //Destroy(enemy.gameObject);
        if (enemiesKilled >= waves[currentWaveIndex].spawnCount)
        {
            onWaveEnd.Invoke();
        }
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
        enemiesKilled = 0;
        instantiatedEnemies = 0;
        isWaveRunning = true;
        StartCoroutine(InstantiateOnTime());

        Enemy _boss = waves[currentWaveIndex].bossPrefab;
        if (_boss != null)
            StartCoroutine(InstantiateEnemiesCoroutine(_boss));
    }

    public void OnWaveEnd()
    {
        isWaveRunning = false;
        StopCoroutine(InstantiateOnTime());
        currentWaveIndex++;
    }
}

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
        public float spawnTiming;
        public int count;
    }

    [SerializeField] List<Wave> waves;
    [SerializeField] Vector3 minBoundary;
    [SerializeField] Vector3 maxBoundary;
    [SerializeField] List<Enemy> enemiesPrefab;     // Input various type of enemies here

    private Vector3 randomPosition;
    private GameObject player;
    private Dictionary<string, ObjectPool<Enemy>> enemyPool;

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
            Debug.Log(enemyPrefab.GetNameTag());
        }

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

    private IEnumerator InstantiateEnemiesCoroutine(Enemy enemyPrefab)
    {
        RandomizePoint();
        while (Vector3.Distance(randomPosition, player.transform.position) < 5f)
        {
            RandomizePoint();
        }

        //Instantiate(enemyPrefab);
        Debug.Log(enemyPrefab.GetNameTag());
        var instantiatedEnemy = enemyPool[enemyPrefab.GetNameTag()].Get();
        instantiatedEnemy.transform.position = randomPosition;
        instantiatedEnemy.Init(KillEnemy);
        yield return null;
    }

    private void KillEnemy(Enemy enemyPrefab)
    {
        enemyPool[enemyPrefab.GetNameTag()].Release(enemyPrefab);
        //Destroy(enemy.gameObject);
    }

    private void BatchSpawn(Enemy enemy, int spawnCount)
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

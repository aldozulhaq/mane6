using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] Vector3 minBoundary;
    [SerializeField] Vector3 maxBoundary;
    [SerializeField] GameObject testEnemy;

    private Vector3 randomPosition;
    private GameObject player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        RandomizePoint();
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

    private void RandomizePoint()
    {
        randomPosition = new Vector3(Random.Range(minBoundary.x, maxBoundary.x),
                                     0f,
                                     Random.Range(minBoundary.z, maxBoundary.z));
    }
}

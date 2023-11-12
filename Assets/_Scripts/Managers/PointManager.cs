using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PointManager : MonoBehaviour
{
    int currentPoint;
    UIManager uiManager;

    [SerializeField] Point pointPrefab;
    ObjectPool<Point> pointObjectPool;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        pointObjectPool = new ObjectPool<Point>(() =>
        {
            return Instantiate(pointPrefab);
        }, point =>
        {
            point.gameObject.SetActive(true);
        }, point =>
        {
            point.gameObject.SetActive(false);
        }, point => 
        {
            Destroy(point.gameObject);
        }, false, 10, 100);
    }

    public void AddPoint()
    {
        currentPoint++;
        // UPDATE UI
    }

    public void ReducePoint(int count)
    {
        if(IsPointSufficient(count))
            currentPoint -= count;
    }

    public int GetPoint()
    {
        return currentPoint;
    }

    public bool IsPointSufficient(int count)
    {
        return currentPoint >= count;
    }

    public void InstantiatePoint(Vector3 location, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 spawnLocation = location + Random.insideUnitSphere * 3;
            var instantiatedPoint = pointObjectPool.Get();
            instantiatedPoint.transform.position = spawnLocation;
            instantiatedPoint.Init(DestroyPoint);
        }
    }

    public void DestroyPoint(Point pointPrefab)
    {
        pointObjectPool.Release(pointPrefab);
    }
}
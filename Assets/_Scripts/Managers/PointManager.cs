using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    int currentPoint;

    public void AddPoint()
    {
        currentPoint++;
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
}

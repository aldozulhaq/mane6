using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public delegate void TimeDelegate(float currentTime);
    public static TimeDelegate OnTimeSpentE;

    private float time;
    private bool isRunning;

    private void Start()
    {
        time = 0;
    }

    [ContextMenu("Start Time")]
    private void StartTime()
    {
        isRunning = true;
        StartCoroutine(Stopwatch());
    }

    private IEnumerator Stopwatch()
    {
        while (isRunning)
        {
            time += 0.1f;

            float roundedTime = Mathf.Round(time * 10.0f) * 0.1f;
            OnTimeSpentE(roundedTime);

            yield return new WaitForSeconds(0.1f);
        }
    }

    [ContextMenu("Stop Time")]
    private void StopTimer()
    {
        isRunning = false;
        StopCoroutine(Stopwatch());
    }
}

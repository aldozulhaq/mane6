using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [SerializeField] float maxTimer = 20f;
    bool isGameRunning;
    float currentTime;

    [Header("Event Channels")]
    [SerializeField] EventChannel onWaveEnd;
    [SerializeField] FloatEventChannel onTimerUpdate;

    public void StartCountdown()
    {
        StopAllCoroutines();
        currentTime = maxTimer;
        isGameRunning = true;
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;

            // Update UI
            onTimerUpdate.Invoke(currentTime);

            if (!isGameRunning)
                yield break;

            yield return null;
        }

        // Ensure timer is 0
        onTimerUpdate.Invoke(0);

        onWaveEnd.Invoke();
        Debug.Log("Time's up");
    }

    public void StopCountdown()
    {
        StopCoroutine(Countdown());
        isGameRunning = false;
    }
}

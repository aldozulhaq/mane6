using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public delegate void TimeDelegate(float currentTime);
    public static TimeDelegate OnTimeSpentE;

    [SerializeField] Text timerText;
    [SerializeField] float maxTimer = 180f;
    bool isGameRunning;
    float currentTime;

    private void OnEnable()
    {
        GameplayEvents.OnWaveStartE += StartCountdown;
    }
    private void OnDisable()
    {
        GameplayEvents.OnWaveStartE -= StartCountdown;
    }

    void StartCountdown()
    {
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
            timerText.text = currentTime.ToString("F0");

            if (!isGameRunning)
                yield break;

            yield return null;
        }

        // Ensure timer is 0
        timerText.text = "0";

        GameplayEvents.OnWaveEnd();
        Debug.Log("Time's up");
    }

    private void StopCountdown()
    {
        StopCoroutine(Countdown());
        isGameRunning = false;
    }
}

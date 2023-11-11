using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [SerializeField] Text timerText;
    [SerializeField] float maxTimer = 20f;
    bool isGameRunning;
    float currentTime;

    [Header("Event Channels")]
    [SerializeField] EventChannel onWaveEnd;

    public void StartCountdown()
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

        onWaveEnd.Invoke();
        Debug.Log("Time's up");
    }

    private void StopCountdown()
    {
        StopCoroutine(Countdown());
        isGameRunning = false;
    }
}

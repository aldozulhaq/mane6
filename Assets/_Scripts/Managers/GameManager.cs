using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject clearWavePanel;

    [Header("Event Channels")]
    [SerializeField] EventChannel onWaveStart;

    int enemiesKilled;

    [ContextMenu("Start Wave")]
    public void StartWave()
    {
        onWaveStart.Invoke();
    }

    public void OnWaveClear()
    {
        clearWavePanel.SetActive(true);
    }

    public void AddEnemiesKilled(int count)
    {
        enemiesKilled += count;
    }
}

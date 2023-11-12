using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    UIManager uiManager;

    [SerializeField] GameObject clearWavePanel;

    [Header("Event Channels")]
    [SerializeField] EventChannel onWaveStart;

    private int enemiesKilled;
    private int waveCount;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        Reset();
    }

    [ContextMenu("Start Wave")]
    public void StartWave()
    {
        onWaveStart.Invoke();
    }

    public void OnWaveClear()
    {
        clearWavePanel.SetActive(true);
        uiManager.UpdateWaveText(waveCount);
    }

    public void AddEnemiesKilled()
    {
        enemiesKilled++;
        uiManager.UpdateKillCount(enemiesKilled);
    }

    public void UpdateUI()
    {
        uiManager.UpdateWaveText(waveCount);
        uiManager.UpdateKillCount(enemiesKilled);
    }

    public void Reset()
    {
        enemiesKilled = 0;
        waveCount = 1;
        UpdateUI();
    }
}

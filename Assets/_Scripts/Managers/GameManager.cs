using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    UIManager uiManager;

    [SerializeField] GameObject clearWavePanel;
    [SerializeField] GameObject gameOverPanel;

    [Header("Event Channels")]
    [SerializeField] EventChannel OnWaveStart;
    [SerializeField] EventChannel OnGameStart;

    private int enemiesKilled;
    private int waveCount;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>(true);
    }

    [ContextMenu("Start Wave")]
    public void StartWave()
    {
        OnWaveStart.Invoke();
    }

    public void StartGame()
    {
        Reset();
        PlayerStats.ResetStats();
        OnGameStart.Invoke();

        StartWave();
    }

    public void OnWaveClear()
    {
        clearWavePanel.SetActive(true);
        waveCount++;
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

    public void SetActiveGameOverPanel(bool active)
    {
        gameOverPanel.SetActive(active);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

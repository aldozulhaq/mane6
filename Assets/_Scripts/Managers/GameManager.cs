using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject clearWavePanel;
    int enemiesKilled;
    private void OnEnable()
    {
        GameplayEvents.OnWaveEndE += OnWaveClear;

        enemiesKilled = 0;
    }

    private void OnDisable()
    {
        GameplayEvents.OnWaveEndE -= OnWaveClear;
    }

    private void Start()
    {
        
    }

    [ContextMenu("Start Wave")]
    public void StartWave()
    {
        GameplayEvents.OnWaveStart();
    }

    private void OnWaveClear()
    {
        clearWavePanel.SetActive(true);
    }

    private void AddEnemiesKilled()
    {
        enemiesKilled++;
    }
}
